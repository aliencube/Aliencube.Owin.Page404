using FluentAssertions;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace Aliencube.Owin.Page404.Tests
{
    [TestFixture]
    public class Page404MiddlewareTest
    {
        #region SetUp / TearDown

        private TestServer _server;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._server != null)
                this._server.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase("", @"sub", "/textfile.txt", HttpStatusCode.OK, null)]
        [TestCase("/sub", @".", "sub/textfile.txt", HttpStatusCode.OK, null)]
        [TestCase("/sub", @".\sub", "sub/textfile.txt", HttpStatusCode.NotFound, "text/html")]
        public async void SendingRequest_Should_Return_StatusCode_And_ContentType(string baseUrl, string baseDir, string requestUrl, HttpStatusCode statusCode, string mediaType)
        {
            var contentType = String.IsNullOrWhiteSpace(mediaType) ? null : new MediaTypeHeaderValue(mediaType);

            this._server = TestServer.Create(app => app.UsePage404(new Page404Options()
                                                                   {
                                                                       RequestPath = new PathString(baseUrl),
                                                                       FileSystem = new PhysicalFileSystem(baseDir),
                                                                       IsLastMiddleware = true,
                                                                   }));
            var response = await this._server.CreateRequest(requestUrl).GetAsync();
            response.StatusCode.Should().Be(statusCode);
            response.Content.Headers.ContentType.Should().Be(contentType);
        }

        [Test]
        [TestCase("/sub", @".\sub", "sub/textfile.txt", "/error/404.html", @".", HttpStatusCode.NotFound, "text/html", "<h1>Not Found</h1>")]
        public async void SendingRequest_Should_Return_StatusCode_404_And_ContentType_TextHtml_From_Customised(string baseUrl, string baseDir, string requestUrl, string custom404PageUrl, string custom404PageDir, HttpStatusCode statusCode, string mediaType, string expected)
        {
            var contentType = String.IsNullOrWhiteSpace(mediaType) ? null : new MediaTypeHeaderValue(mediaType);

            this._server = TestServer.Create(app => app.UsePage404(new Page404Options()
                                                                   {
                                                                       RequestPath = new PathString(baseUrl),
                                                                       FileSystem = new PhysicalFileSystem(baseDir),
                                                                       IsLastMiddleware = true,
                                                                       UseCustom404Page = true,
                                                                       Custom404PagePath = new PathString(custom404PageUrl),
                                                                       Custom404PageDir = new PhysicalFileSystem(custom404PageDir),
                                                                   }));
            var response = await this._server.CreateRequest(requestUrl).GetAsync();
            response.StatusCode.Should().Be(statusCode);
            response.Content.Headers.ContentType.Should().Be(contentType);
            response.Content.ReadAsStringAsync().Result.Should().Contain(expected);
        }

        #endregion Tests
    }
}
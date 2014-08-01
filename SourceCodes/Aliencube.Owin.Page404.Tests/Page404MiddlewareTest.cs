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
        public async void Test(string baseUrl, string baseDir, string requestUrl, HttpStatusCode statusCode, string mediaType)
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

        #endregion Tests
    }
}
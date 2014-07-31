using FluentAssertions;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

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
        [TestCase("", @"sub", "/textfile.txt", HttpStatusCode.OK)]
        public void Test(string baseUrl, string baseDir, string requestUrl, HttpStatusCode statusCode)
        {
            this._server = TestServer.Create(app => app.UsePage404(new Page404Options()
                                                                   {
                                                                       RequestPath = new PathString(baseUrl),
                                                                       FileSystem = new PhysicalFileSystem(baseDir),
                                                                       DefaultFileNames = new List<string>() { "index.html" },
                                                                   }));
            var response = this._server.CreateRequest(requestUrl).GetAsync().Result;
            response.StatusCode.Should().Be(statusCode);
        }

        #endregion Tests
    }
}
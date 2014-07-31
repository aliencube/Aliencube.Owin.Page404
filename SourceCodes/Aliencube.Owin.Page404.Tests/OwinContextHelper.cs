using Microsoft.Owin;
using System.IO;
using System.Threading;

namespace Aliencube.Owin.Page404.Tests
{
    /// <summary>
    /// This represents a helper entity for OWIN context.
    /// </summary>
    public static class OwinContextHelper
    {
        /// <summary>
        /// Creates an empty OWIN context instance.
        /// </summary>
        /// <param name="path">Request URL path.</param>
        /// <returns>Returns the empty OWIN context.</returns>
        public static IOwinContext CreateEmptyRequest(string path)
        {
            IOwinContext context = new OwinContext();
            context.Request.PathBase = PathString.Empty;
            context.Request.Path = new PathString(path);
            context.Response.Body = new MemoryStream();
            context.Request.CallCancelled = CancellationToken.None;
            context.Request.Method = "GET";
            return context;
        }
    }
}
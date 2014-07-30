using Microsoft.Owin;
using Owin;
using System;

namespace Aliencube.Owin.Page404
{
    /// <summary>
    /// This is the entity to provide extension methods for the <c>Page404Middleware</c> class.
    /// </summary>
    public static class Page404Extensions
    {
        /// <summary>
        /// Enables to handle the page 404 error from the current directory.
        /// </summary>
        /// <param name="app">OWIN internface.</param>
        /// <returns></returns>
        public static IAppBuilder UsePage404(this IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            return UsePage404(app, new Page404Options());
        }

        /// <summary>
        /// Enables to handle the page 404 error from the directory provided.
        /// </summary>
        /// <param name="app">OWIN internface.</param>
        /// <param name="requestPath">The relative request path and physical path.</param>
        /// <returns></returns>
        public static IAppBuilder UsePage404(this IAppBuilder app, string requestPath)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (requestPath == null)
            {
                throw new ArgumentNullException("requestPath");
            }

            return UsePage404(app, new Page404Options() { RequestPath = new PathString(requestPath) });
        }

        /// <summary>
        /// Enables to handle the page 404 error with options provided.
        /// </summary>
        /// <param name="app">OWIN internface.</param>
        /// <param name="options">The configuration options.</param>
        /// <returns></returns>
        public static IAppBuilder UsePage404(this IAppBuilder app, Page404Options options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            return app.Use<Page404Middleware>(options);
        }
    }
}
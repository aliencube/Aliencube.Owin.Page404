using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aliencube.Owin.Page404
{
    /// <summary>
    /// This provides the entity to handle the page 404 error when a request comes in.
    /// </summary>
    public class Page404Middleware : OwinMiddleware
    {
        private readonly Page404Options _options;

        /// <summary>
        /// Initialises a new instance of the <c>Page404Middleware</c> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">The configuration options.</param>
        public Page404Middleware(OwinMiddleware next, Page404Options options)
            : base(next)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            this._options = options;
        }

        /// <summary>
        /// Processes a request to determine whether it points to an existing file or not.
        /// </summary>
        /// <param name="context">OWIN context instance.</param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;

            IEnumerable<IFileInfo> fis;
            if (this._options.FileSystem.TryGetDirectoryContents(path.Value, out fis))
            {
                var defaultFileExists = fis.Any(fi => this._options.DefaultFileNames.Contains(fi.Name));
                if (defaultFileExists)
                {
                    if (!this._options.IsLastMiddleware)
                    {
                        await this.Next.Invoke(context);
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                IFileInfo fi;
                if (this._options.FileSystem.TryGetFileInfo(path.Value, out fi))
                {
                    if (!this._options.IsLastMiddleware)
                    {
                        await this.Next.Invoke(context);
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
        }
    }
}
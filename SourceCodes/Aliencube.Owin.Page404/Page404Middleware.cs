using System.IO;
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
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (this.IsValidRequestPath(context))
            {
                if (!this._options.IsLastMiddleware)
                {
                    await this.Next.Invoke(context);
                }
            }
            else
            {
                this.LoadPage404(context);
            }
        }

        /// <summary>
        /// Checks whether the request path is valid or not.
        /// </summary>
        /// <param name="context">OWIN context instance.</param>
        /// <returns>Returns <c>True</c>, if the request path is valid; otherwise returns <c>False</c>.</returns>
        private bool IsValidRequestPath(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var path = context.Request.Path;
            return this.IsValidRequestDirectory(path) || this.IsValidRequestFile(path);
        }

        /// <summary>
        /// Checks whether the request path is a valid directory with default file names or not.
        /// </summary>
        /// <param name="requestPath">Request path.</param>
        /// <returns>Returns <c>True</c>, if the request path is a valid directory with default file names; otherwise returns <c>False</c>.</returns>
        private bool IsValidRequestDirectory(PathString requestPath)
        {
            IEnumerable<IFileInfo> fis;
            var validDirectory = this._options.FileSystem.TryGetDirectoryContents(requestPath.Value, out fis);
            var validDefaultFile = fis != null && fis.Any(fi => this._options.DefaultFileNames.Contains(fi.Name));
            return validDirectory && validDefaultFile;
        }

        /// <summary>
        /// Checks whether the request path is a valid file or not.
        /// </summary>
        /// <param name="requestPath">Request path.</param>
        /// <returns>Returns <c>True</c>, if the request path is a valid file; otherwise returns <c>False</c>.</returns>
        private bool IsValidRequestFile(PathString requestPath)
        {
            IFileInfo fi;
            var validFile = this._options.FileSystem.TryGetFileInfo(requestPath.Value, out fi);
            return validFile;
        }

        /// <summary>
        /// Loads the 404 page.
        /// </summary>
        /// <param name="context">OWIN context instance.</param>
        private void LoadPage404(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!this._options.UseCustom404Page)
            {
                var page404 = new Views.Page404();
                page404.Execute(context);

                return;
            }

            context.Response.StatusCode = 404;
            context.Response.ReasonPhrase = Resources.Page404Html_Title;

            IFileInfo fi;
            if (!this._options.Custom404PageDir.TryGetFileInfo(this._options.Custom404PagePath.Value, out fi))
            {
                return;
            }

            using (var reader = new StreamReader(fi.CreateReadStream()))
            using (var writer = new StreamWriter(context.Response.Body))
            {
                writer.WriteAsync(reader.ReadToEndAsync().Result);
                context.Response.ContentType = "text/html";
            }
        }
    }
}

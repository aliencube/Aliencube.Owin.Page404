using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;

namespace Aliencube.Owin.Page404
{
    public class Page404Middleware : OwinMiddleware
    {
        private readonly Page404Options _options;

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

        public override async Task Invoke(IOwinContext context)
        {
            var path = context.Request.Uri.AbsolutePath;

            IEnumerable<IFileInfo> fis;
            if (this._options.FileSystem.TryGetDirectoryContents(path, out fis))
            {
                var defaultFileExists = fis.Any(fi => this._options.DefaultFileNames.Contains(fi.Name));
                if (defaultFileExists)
                {
                    await this.Next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                IFileInfo fi;
                if (this._options.FileSystem.TryGetFileInfo(path, out fi))
                {
                    await this.Next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
        }
    }
}
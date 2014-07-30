using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Aliencube.Owin.Page404
{
    public class Page404Options : DefaultFilesOptions
    {
        public Page404Options()
            : this(new SharedOptions())
        {
        }

        public Page404Options(SharedOptions sharedOptions)
            : base(sharedOptions)
        {
        }
    }
}
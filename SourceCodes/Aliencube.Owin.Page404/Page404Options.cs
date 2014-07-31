using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.Infrastructure;
using System;

namespace Aliencube.Owin.Page404
{
    /// <summary>
    /// This is an entity to provide <c>Page404Middleware</c> with options.
    /// </summary>
    public class Page404Options : DefaultFilesOptions
    {
        /// <summary>
        /// Initialises a new instance of the <c>Page404Options</c> class.
        /// </summary>
        public Page404Options()
            : this(new SharedOptions())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <c>Page404Options</c> class.
        /// </summary>
        /// <param name="sharedOptions"><c>SharedOptions</c> instance.</param>
        public Page404Options(SharedOptions sharedOptions)
            : base(sharedOptions)
        {
            if (sharedOptions == null)
            {
                throw new ArgumentNullException("sharedOptions");
            }

            this.IsLastMiddleware = true;
        }

        /// <summary>
        /// Gets or sets the value that specifies whether <c>Page404Middleware</c> sits at the last of the middleware chain or not.
        /// Default value is <c>True</c>. If <c>Page404Middleware</c> is the only middleware, set this <c>True</c>.
        /// </summary>
        public bool IsLastMiddleware { get; set; }
    }
}
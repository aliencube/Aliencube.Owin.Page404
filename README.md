# Aliencube.Owin.Page404 #

**Aliencube.Owin.Page404** provides an [OWIN](http://owin.org) middleware that handles 404 page in both pre-built and customised.


## Dependencies ##

This library has dependencies on the following [NuGet](http://nuget.org) packages:

* [Microsoft.Owin](http://www.nuget.org/packages/Microsoft.Owin)
* [Microsoft.Owin.FileSystems](http://www.nuget.org/packages/Microsoft.Owin.FileSystems)
* [Microsoft.Owin.StaticFiles](http://www.nuget.org/packages/Microsoft.Owin.StaticFiles)

> **NOTE**: At the time of writing **Aliencube.Owin.Page404**, all [NuGet](http://nuget.org) packages are of version 2.1.0.


## Getting Started ##

This can be the easiest way to use **Aliencube.Owin.Page404**.

```csharp
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.UsePage404();
    }
}
```

If you want more control, try the following:

```csharp
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var options = new Page404Options() { FileSystem = new PhysicalFileSystem(@".") };
        app.Use<Page404Middleware>(options);
    }
}
```


### `Page404Options` ###

* `RequestPath`: The relative request path that maps to static resources. **NOT USED**
* `FileSystem`: The file system used to locate resources.
* `DefaultFileNames`: An ordered list of file names to select by default. List length and ordering may affect performance. List of default pages in order are `default.htm`, `default.html`, `index.htm` and `index.html`.
* `IsLastMiddleware`: Gets or sets the value that specifies whether `Page404Middleware` sits at the last of the middleware chain or not. Default value is `true`. If `Page404Middleware` is the only middleware, set this `true`.


## Contribution ##

Your contribution is always welcome! All your work should be done in the`dev` branch. Once you finish your work, please send us a pull request on `dev` for review. Make sure that all your changes **MUST** be covered with test codes; otherwise yours won't get accepted.


## License ##

**Aliencube.Owin.Page404** is released under [MIT License](http://opensource.org/licenses/MIT).

> The MIT License (MIT)
> 
> Copyright (c) 2014 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

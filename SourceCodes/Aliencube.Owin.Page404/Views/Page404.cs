﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace Aliencube.Owin.Page404.Views {
    
    #line 1 "Page404.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    
    #line 2 "Page404.cshtml"
    using Aliencube.Owin.Page404;
    
    #line default
    #line hidden
    
    
    public class Page404 : Aliencube.Owin.Page404.Views.BaseView {
        
#line hidden
        
        public Page404() {
        }
        
        public override void Execute() {
WriteLiteral("\r\n");

            
            #line 4 "Page404.cshtml"
  
    Response.StatusCode = 404;
    Response.ReasonPhrase = Resources.Page404Html_Title;
    Response.ContentType = "text/html";
    Response.ContentLength = null; // Clear any prior Content-Length

            
            #line default
            #line hidden
WriteLiteral("\r\n<!DOCTYPE html>\r\n<html");

WriteAttribute("lang", Tuple.Create(" lang=\"", 292), Tuple.Create("\"", 353)
            
            #line 11 "Page404.cshtml"
, Tuple.Create(Tuple.Create("", 299), Tuple.Create<System.Object, System.Int32>(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            
            #line default
            #line hidden
, 299), false)
);

WriteLiteral(" xmlns=\"http://www.w3.org/1999/xhtml\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n    <title>");

            
            #line 14 "Page404.cshtml"
      Write(Resources.Page404Html_Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <style>\r\n");

WriteLiteral("        ");

            
            #line 16 "Page404.cshtml"
    WriteLiteral(@"body {
    font-family: 'Segoe UI',Tahoma,Arial,Helvetica,sans-serif;
    font-size: .813em;
    line-height: 1.4em;
    color: #222;
}

h1, h2 {
    font-weight: 100;
}

h1 {
    color: #44525e;
    margin: 15px 0 15px 0;
}

h2 {
    margin: 10px 5px 0 0;
}
");

            
            #line default
            #line hidden
WriteLiteral(" \r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>");

            
            #line 20 "Page404.cshtml"
   Write(Resources.Page404Html_Title);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h2>");

            
            #line 21 "Page404.cshtml"
   Write(Resources.Page404Html_PageNotFound);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n</body>\r\n</html>");

        }
    }
}

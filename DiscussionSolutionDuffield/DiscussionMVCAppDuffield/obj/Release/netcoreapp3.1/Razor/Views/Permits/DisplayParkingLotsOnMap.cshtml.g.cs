#pragma checksum "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Permits\DisplayParkingLotsOnMap.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "09ff80b8700a8bdf45f285e212eaa6ff0d67d9c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Permits_DisplayParkingLotsOnMap), @"mvc.1.0.view", @"/Views/Permits/DisplayParkingLotsOnMap.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\_ViewImports.cshtml"
using DiscussionMVCAppDuffield;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\_ViewImports.cshtml"
using DiscussionMVCAppDuffield.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\_ViewImports.cshtml"
using DiscussionMVCAppDuffield.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09ff80b8700a8bdf45f285e212eaa6ff0d67d9c3", @"/Views/Permits/DisplayParkingLotsOnMap.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5244a626ae5629604003b3761aae05aa675f4ff1", @"/Views/_ViewImports.cshtml")]
    public class Views_Permits_DisplayParkingLotsOnMap : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Permits\DisplayParkingLotsOnMap.cshtml"
  
    ViewData["Title"] = "Display Parking Lots On Map";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h4>");
#nullable restore
#line 6 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Permits\DisplayParkingLotsOnMap.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n\r\n<img");
            BeginWriteAttribute("src", " src=\"", 98, "\"", 142, 1);
#nullable restore
#line 8 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Permits\DisplayParkingLotsOnMap.cshtml"
WriteAttributeValue("", 104, Url.Action("DisplayParkingLotsOnMap"), 104, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48a0dea5bd5d71a8577a4ad00207ed4339f1c31c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Lots_ShowAllLots), @"mvc.1.0.view", @"/Views/Lots/ShowAllLots.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48a0dea5bd5d71a8577a4ad00207ed4339f1c31c", @"/Views/Lots/ShowAllLots.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5244a626ae5629604003b3761aae05aa675f4ff1", @"/Views/_ViewImports.cshtml")]
    public class Views_Lots_ShowAllLots : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Lot>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
  
    ViewData["Title"] = "List All Lots";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Listing of All Lots</h1>

<table class=""table table-bordered table-striped"">
      <thead>
          <tr>
              <th>Lot Number</th>
              <th>Lot Name</th>
              <th>Lot Address</th>
              <th>Lot Type: TypeOfDay - StartTime and EndTime</th>
          </tr>
      </thead>

      <tbody>
");
#nullable restore
#line 19 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
           foreach (Lot eachLot in Model)
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
               Write(eachLot.LotNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
               Write(eachLot.LocationName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
               Write(eachLot.LotAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n");
#nullable restore
#line 26 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
                     foreach(LotStatus eachLotStatus in eachLot.LotStatuses)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
                    Write(eachLotStatus.LotType.LotTypeName + "on" + eachLotStatus.TypeOfDay + "from" + eachLotStatus.StartTime.TimeOfDay + "to" + eachLotStatus.EndTime.TimeOfDay);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br/>\r\n");
#nullable restore
#line 29 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 32 "C:\Users\duffi\source\repos\MIST450Duffield\DiscussionSolutionDuffield\DiscussionMVCAppDuffield\Views\Lots\ShowAllLots.cshtml"
          }

#line default
#line hidden
#nullable disable
            WriteLiteral("      </tbody>\r\n</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Lot>> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a8a6e81c864977f5b9b2a3385145a118cfb7e4da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Property_SellDetails), @"mvc.1.0.view", @"/Views/Property/SellDetails.cshtml")]
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
#line 1 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\_ViewImports.cshtml"
using REMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\_ViewImports.cshtml"
using REMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a8a6e81c864977f5b9b2a3385145a118cfb7e4da", @"/Views/Property/SellDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84b4b380cad193caeebbea464081a7d3496e9225", @"/Views/_ViewImports.cshtml")]
    public class Views_Property_SellDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "BidModels", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Bid", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SComments", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
  
    ViewData["Title"] = "Details";
    var item = Model.property;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Details</h1>

<div>
    <h4>SellPropertyModel</h4>
    <hr />
    <div class=""row"">
        <dl class=""col"">
            <div class=""row"">
                <dt class=""col-sm-2"">
                    Name
                </dt>
                <dd class=""col-sm-10"">
                    ");
#nullable restore
#line 20 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(item.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Property Type\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 26 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(item.propertyType);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Address\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 32 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(item.address);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Description\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 38 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(item.description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    Start Price\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 44 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(item.startprice);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </div>\r\n        </dl>\r\n        <div class=\"col\">\r\n            <div class=\"card\">\r\n                <div class=\"bg-image hover-overlay ripple\" data-mdb-ripple-color=\"light\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 1420, "\"", 1472, 2);
            WriteAttributeValue("", 1426, "https://localhost:44324/app-images/", 1426, 35, true);
#nullable restore
#line 51 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
WriteAttributeValue("", 1461, item.image, 1461, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"
                         class=""img-fluid"" />
                    <a href=""#!"">
                        <div class=""mask"" style=""background-color: rgba(251, 251, 251, 0.15)""></div>
                    </a>
                </div>
            </div>
        </div>
");
#nullable restore
#line 59 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
         if (ViewBag.isApplied)
        {
            var bid = Model.bids;
            if (ViewBag.won)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row\">\r\n                    <hr class=\"w-50\" />\r\n                    <h4>You Own This Property</h4>\r\n                </div>\r\n");
#nullable restore
#line 68 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row\">\r\n                    <hr class=\"w-50\" />\r\n                    <h4>You have Applied for this property ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a8a6e81c864977f5b9b2a3385145a118cfb7e4da8879", async() => {
                WriteLiteral("Your Bid");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 73 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
                                                                                                                WriteLiteral(bid.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h4>\r\n                </div>\r\n");
#nullable restore
#line 75 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
            }
            
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <hr class=\"w-50\" />\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a8a6e81c864977f5b9b2a3385145a118cfb7e4da11725", async() => {
                WriteLiteral("\r\n                        <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 2582, "\"", 2601, 1);
#nullable restore
#line 84 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
WriteAttributeValue("", 2590, ViewBag.id, 2590, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"id\" id=\"id\">\r\n                        <input type=\"number\"");
                BeginWriteAttribute("min", " min=\"", 2667, "\"", 2692, 1);
#nullable restore
#line 85 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
WriteAttributeValue("", 2673, ViewBag.startprice, 2673, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" name=\"bid\" id=\"bid\" required>\r\n                        <input type=\"submit\" value=\"Place Bid\" class=\"btn btn-primary\">\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 90 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <hr class=\"w-50\" />\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 94 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
         if (ViewBag.show)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h2>Comments:</h2>\r\n");
#nullable restore
#line 97 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 98 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
         foreach (var comment in Model.comments)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <dl class=\"row\">\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 102 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(comment.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 105 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
               Write(comment.comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n");
#nullable restore
#line 108 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a8a6e81c864977f5b9b2a3385145a118cfb7e4da16109", async() => {
                WriteLiteral("\r\n        Add Comment\r\n        <div class=\"form-actions no-color\">\r\n            <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 3839, "\"", 3858, 1);
#nullable restore
#line 118 "C:\Users\Dev's PC\source\repos\REMS\REMS\Views\Property\SellDetails.cshtml"
WriteAttributeValue("", 3847, ViewBag.id, 3847, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" name=""id"" id=""id"" />
            <div class=""input-group"">
                <div class=""form-outline"">
                    <input type=""text"" class=""form-control"" name=""comment"" id=""comment"">
                </div>
                <button type=""submit"" class=""btn btn-primary"">
                    <i class=""fas fa-arrow-right""></i>
                </button>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
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

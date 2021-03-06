#pragma checksum "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e795a7dd2141c8c97a4729612714528ab0caf4b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_Index), @"mvc.1.0.view", @"/Views/Customer/Index.cshtml")]
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
#line 1 "D:\Prashant\Funda Clear\FundaClearApp\Views\_ViewImports.cshtml"
using FundaClearApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Prashant\Funda Clear\FundaClearApp\Views\_ViewImports.cshtml"
using FundaClearApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e795a7dd2141c8c97a4729612714528ab0caf4b0", @"/Views/Customer/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3e8cc2a1141cc9567a3ec1b3fc839b06bf5475af", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Loyalty.DTO.CustomerDTO>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
  
    ViewBag.Title = "Course";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div style=\"padding: 30px;\">\r\n    <h2>Customers</h2>\r\n    <hr />\r\n    <div style=\"width:100%; height:30px\">\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 244, "\"", 281, 1);
#nullable restore
#line 11 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
WriteAttributeValue("", 251, Url.Action("Add", "Customer"), 251, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""float:right;  background-color: #2e4b90;"" class=""btn btn-primary btn-sm active"" role=""button"">
            + Add New Customer
        </a>
    </div>
    <hr />

    <div>
        <table class=""table table-bordered"">
            <thead>
                <tr>
                    <th>Customer Name</th>
                    <th>Mobile Number</th>
                    <th>Email </th>
                    <th>Address</th>
                    <th>Balance Point</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 30 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <th scope=\"row\">");
#nullable restore
#line 33 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                               Write(item.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <td>");
#nullable restore
#line 34 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                   Write(item.MobileNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 35 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                   Write(item.EmailId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                   Write(item.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 37 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                   Write(item.BalancePoint);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        <a id=\"btnEdit\"");
            BeginWriteAttribute("href", " href=\"", 1267, "\"", 1308, 2);
            WriteAttributeValue("", 1274, "/customer/edit?id=", 1274, 18, true);
#nullable restore
#line 39 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
WriteAttributeValue("", 1292, item.CustomerId, 1292, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n\r\n                        &nbsp;\r\n                        <a href=\"javascript:void(0)\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1406, "\"", 1450, 3);
            WriteAttributeValue("", 1416, "DeleteCustomer(\'", 1416, 16, true);
#nullable restore
#line 42 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
WriteAttributeValue("", 1432, item.CustomerId, 1432, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1448, "\')", 1448, 2, true);
            EndWriteAttribute();
            WriteLiteral(" id=\"btnDelete\" class=\"btnDelete\">Delete</a>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 45 "D:\Prashant\Funda Clear\FundaClearApp\Views\Customer\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>
<script>
   
    function DeleteCustomer(id)
    {
        $.ajax({
            type: ""POST"",
            url: ""/Customer/Delete?id="" + id,
            success: function (data) {
                if (data == true) {
                    window.location.reload();
                }
                else {
                    alert(""Something failed"");
                }
            }
        });
    }

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Loyalty.DTO.CustomerDTO>> Html { get; private set; }
    }
}
#pragma warning restore 1591

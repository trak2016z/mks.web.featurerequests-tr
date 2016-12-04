using Microsoft.AspNetCore.Razor.TagHelpers;
using MKS.Web.Common.ResourceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.TagHelpers
{
    /// <summary>
    /// Tag helper for scripts which registers contents of the tag in the resource manager.
    /// Useful for view components which want to output scripts at the end of page's body.
    /// Usage:
    ///     <![CDATA[
    ///         <script resource>
    ///             function test() {....}
    ///         </script>
    ///     ]]>
    /// </summary>
    [HtmlTargetElement(Attributes = "resource")]
    public class ResourceTagHelper : TagHelper
    {
        private readonly IResourceManager _resourceManager;

        public ResourceTagHelper(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var contents = (await output.GetChildContentAsync())
                .GetContent();

            output.SuppressOutput();
            _resourceManager.RegisterFootScriptContent(contents);
        }
    }
}

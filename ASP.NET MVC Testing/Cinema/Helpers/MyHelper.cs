using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cinema.Helpers
{
   public class Caps : TagHelper
    {
        private string _divData = string.Empty;
        public string DivData
        {
            get
            {
                return _divData.ToUpper();
            }
            set { _divData = value; }
        }
       
        public override void Process(
          TagHelperContext context,
          TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent(DivData);
        }
    }
}
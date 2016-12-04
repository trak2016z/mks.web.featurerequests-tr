using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Common.ResourceManager
{
    public class Script
    {
        public string Path { get; set; }
        public string Content { get; set; }

        public override int GetHashCode()
        {
            return Path != null 
                ? Path.GetHashCode()
                : Content.GetHashCode();  
        }
    }
}

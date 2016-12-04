using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Common.ResourceManager
{
    public interface IResourceManager
    {
        void RegisterFootScriptUrl(string url);
        void RegisterFootScriptContent(string content);
        List<Script> GetAllFootScripts();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Common.ResourceManager
{
    public class ResourceManager : IResourceManager
    {
        private readonly HashSet<Script> _footScripts = new HashSet<Script>(); 

        public List<Script> GetAllFootScripts()
        {
            return _footScripts.ToList();
        }

        public void RegisterFootScriptContent(string content)
        {
            var obj = new Script() { Content = content };

            //does not add obj if obj.Content is present in the set because
            //hash is calculated on path/content, so same content == same object
            _footScripts.Add(obj);
        }

        public void RegisterFootScriptUrl(string url)
        {
            var obj = new Script() { Path = url };

            //does not add obj if obj.Content is present in the set because
            //hash is calculated on path/content, so same content == same object
            _footScripts.Add(obj);
        }
    }
}

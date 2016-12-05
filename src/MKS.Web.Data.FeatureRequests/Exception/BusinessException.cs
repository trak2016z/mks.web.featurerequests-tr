using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Exception
{
    public class BusinessException : System.Exception
    {
        public string Property { get; set; }

        public BusinessException(string msg)
            : base(msg)
        {

        }

        public BusinessException(string msg, string propertyOrField)
            : base(msg)
        {
            Property = propertyOrField;
        }

        public BusinessException(string msg, System.Exception innerException)
            : base(msg, innerException)
        {

        }
    }
}

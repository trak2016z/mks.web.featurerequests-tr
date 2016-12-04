using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.FeatureRequests.Model.Common
{
    public class DataGridModel<T>
    {
        public DataRequestModel Request { get; set; } = new DataRequestModel();
        public List<T> Data { get; set; } = new List<T>();

        public DataGridModel()
        {

        }

        public DataGridModel(DataRequestModel request, List<T> data)
        {
            Request = request;
            data = data;
        }
    }
}

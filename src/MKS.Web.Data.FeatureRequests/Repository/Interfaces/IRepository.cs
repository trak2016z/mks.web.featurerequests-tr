using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Repository.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(long id);
        List<T> GetList(IDataRequest<T> request);
        List<T> GetList();
        void Add(T entity);
        void Update(T entity);
        void Update(long id, Action<T> updateFunc);
        void Delete(long id);
    }
}

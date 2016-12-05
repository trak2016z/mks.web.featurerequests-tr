﻿using MKS.Web.Data.FeatureRequests.Model.Interfaces;
using MKS.Web.Data.FeatureRequests.Query;
using MKS.Web.Data.FeatureRequests.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected readonly FeatureRequestsDbContext _db;

        public BaseRepository(FeatureRequestsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets a single object by unique id.
        /// </summary>
        public virtual T GetById(long id)
        {
            return _db.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets a specified page of objects matching specific criteria.
        /// </summary>
        public virtual List<T> GetList(IDataRequest<T> request)
        {
            return GetListQueryable(request).ToList();
        }

        /// <summary>
        /// Gets a specified page of objects matching specific criteria
        /// as queryable for further processing in other repository methods.
        /// </summary>
        protected virtual IQueryable<T> GetListQueryable(IDataRequest<T> request)
        {
            IQueryable<T> q = _db.Set<T>();

            if (request.Where != null)
                q = q.Where(request.Where);

            if (request.OrderBy != null)
            {
                if (request.Direction == SortDirection.ASC)
                    q = q.OrderBy(request.OrderBy);
                else
                    q = q.OrderByDescending(request.OrderBy);
            }
            else
            {
                //Id ASC by default
                q = q.OrderBy(e => e.Id);
            }

            return q.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        /// <summary>
        /// Gets ALL entities stored.
        /// </summary>
        public virtual List<T> GetList()
        {
            return _db.Set<T>().ToList();
        }

        public virtual void Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _db.Update(entity);
            _db.SaveChanges();
        }

        public void Update(long id, Action<T> updateFunc)
        {
            var entity = _db.Set<T>().SingleOrDefault(e => e.Id == id);
            if(entity != null)
                updateFunc(entity);
            _db.SaveChanges();
        }

        public virtual void Delete(long id)
        {
            var entity = _db.Set<T>().SingleOrDefault(e => e.Id == id);
            if(entity != null)
            {
                _db.Remove(entity);
                _db.SaveChanges();
            }
        }
    }
}

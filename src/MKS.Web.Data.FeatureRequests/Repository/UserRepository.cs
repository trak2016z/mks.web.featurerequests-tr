using MKS.Web.Data.FeatureRequests.Model;
using MKS.Web.Data.FeatureRequests.Model.Query;
using MKS.Web.Data.FeatureRequests.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MKS.Web.Data.FeatureRequests.Repository
{
    public class UserRepository
    {
        private readonly FeatureRequestsDbContext _db;

        public UserRepository(FeatureRequestsDbContext db)
        {
            _db = db;
        }

        public async Task EnsureRegisteredAsync(string id)
        {
            using (var scope = await _db.Database.BeginTransactionAsync())
            {
                if(!await _db.Users.AnyAsync(u => u.Id == id))
                {
                    _db.Users.Add(new User() { Id = id });
                    await _db.SaveChangesAsync();
                    scope.Commit();
                }
            }
        }
    }
}

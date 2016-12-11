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

        public async Task AddOrUpdateAsync(User user)
        {
            using (var scope = await _db.Database.BeginTransactionAsync())
            {
                var oldUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                if (oldUser == null)
                {
                    _db.Users.Add(user);
                }
                else
                {
                    oldUser.GivenName = user.GivenName;
                }

                await _db.SaveChangesAsync();
                scope.Commit();
            }
        }
    }
}

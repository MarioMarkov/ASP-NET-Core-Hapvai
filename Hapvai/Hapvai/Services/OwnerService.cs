using Hapvai.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext context;
        public OwnerService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int IdByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool IsDealer(string userId)
        {
            return this.context.Owners.Any(o => o.UserId == userId);
        }
    }
}

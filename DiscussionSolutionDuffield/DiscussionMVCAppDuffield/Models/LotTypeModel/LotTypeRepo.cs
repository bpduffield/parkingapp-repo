using DiscussionMVCAppDuffield.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public class LotTypeRepo : ILotTypeRepo
    {
        private ApplicationDbContext database;

        public LotTypeRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public List<LotType> ListAllLotTypes()
        {
            List<LotType> lotTypes = database.LotTypes.ToList();

            return lotTypes;
        }
    }
}

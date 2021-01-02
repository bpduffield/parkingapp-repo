using DiscussionMVCAppDuffield.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    //LotRepo implements ILotRepo to go to database
    //dependent on the database

    public class LotRepo : ILotRepo
    {
        private ApplicationDbContext database;

        public LotRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Task AddLot(Lot lot)
        {
            database.Lots.AddAsync(lot);
            return database.SaveChangesAsync();

            
        }

        public Task DeleteLot(Lot lot)
        {
            database.Lots.Remove(lot);
            return database.SaveChangesAsync();
        }

        public Task EditLot(Lot lot)
        {
            database.Lots.Update(lot);
            return database.SaveChangesAsync();
        }

        public Lot FindLot(int? lotID)
        {
            Lot lot = database.Lots.Find(lotID);
            return lot;
        }

        public string GetSpotsForAllLots()
        {
            var resultList = database.Lots.ToList();
            string jsonData = JsonConvert.SerializeObject(resultList);
            return jsonData;
        }

        public bool IsChosenLotAvailable(int chosenLotID)
        {
            bool lotAvailable;

            Lot lot = database.Lots.Find(chosenLotID);
            
            if(lot.CurrentOccupancy < lot.MaxCapacity)
            {
                lotAvailable = true;
            }
            else
            {
                lotAvailable = false;
            }

            return lotAvailable;
        }

        public List<Lot> ListAllLots()
        {          

            List<Lot> lotList = database.Lots.Include(l => l.LotStatuses).ThenInclude(ls => ls.LotType).ToList<Lot>();
            //.Where(l => l.LotStatuses.Any(l => l.LotType.LotTypeName == "Permit"))
            return lotList;

        }



        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.Models
{
    public interface ILotRepo
    {
        //promises the actions that will be done
        //but does not implement
        string GetSpotsForAllLots();

        List<Lot> ListAllLots();

        //Async methods [Add, Delete, Update, Save to database]
        Task AddLot(Lot lot);

        Task EditLot(Lot lot);
        Lot FindLot(int? lotID);

        Task DeleteLot(Lot lot);
        bool IsChosenLotAvailable(int chosenLotID);

        

        //Not here, but in controller
        //List<Lot> SearchLots();
    }
}

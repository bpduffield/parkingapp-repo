using DiscussionMVCAppDuffield.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;

namespace DiscussionMVCAppDuffield.ViewModels
{
    public class LotSearchViewModel
    {
        //Usually, view models only have properties, no methods

        //User Inputs
        public bool IsLotCurrentlyAvailable { get; set; }

        //DropDownList (user sees name, system receives ID)
        public int? LotTypeID {get;set;}

        public string TypeOfDay { get; set; }//Weekday, Weekend, Gameday

        public int InputPageSize { get; set; }
        
        //Output(Search result)
        //public List<Lot> LotSearchResult { get; set; }

        public PagedResult<Lot> LotSearchResult { get; set; }

        public LotSearchViewModel()
        {
            this.LotSearchResult = new PagedResult<Lot>();
        }

    }
}

using DiscussionMVCAppDuffield.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppDuffield.ViewModels
{
    public class AppUserSearchViewModel
    {
        //input
        public string appUserType { get; set; }



        //output
        public List<ApplicationUser> AppUserSearchResult { get; set; }
    }
}

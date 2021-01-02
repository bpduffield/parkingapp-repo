using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppDuffield.Controllers
{
    public class AppUsersController : Controller
    {
        private IApplicationUserRepo iApplicationUserRepo;

        public AppUsersController(IApplicationUserRepo applicationUserRepo)
        {
            iApplicationUserRepo = applicationUserRepo;
        }

        [HttpGet]
        public IActionResult SearchAppUsers()
        {
            AppUserSearchViewModel viewModel = new AppUserSearchViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchAppUsers(AppUserSearchViewModel appUserSearchViewModel)
        {
            List<ApplicationUser> applicationUserList = iApplicationUserRepo.ListAllAppUsers();

            if(appUserSearchViewModel.appUserType != null)
            {
                applicationUserList = applicationUserList.Where(a => a.GetType().Name == appUserSearchViewModel.appUserType).ToList();
                
            }

            appUserSearchViewModel.AppUserSearchResult = applicationUserList;

            return View(appUserSearchViewModel);
        }
    }
}
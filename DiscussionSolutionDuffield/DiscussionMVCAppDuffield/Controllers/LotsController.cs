using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppDuffield.Data;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiscussionMVCAppDuffield.Controllers
{
    public class LotsController : Controller
    {
        //Access to database
        //class attribute or instance variable 
        //private ApplicationDbContext database;

        private ILotRepo iLotRepo;
        private ILotTypeRepo iLotTypeRepo;

        //Dependency Inversion
        //Instead of being dependent on the database
        //Change the dependency direction
        //dependency injection
        public LotsController(//ApplicationDbContext dbContext
            ILotRepo lotRepo, ILotTypeRepo lotTypeRepo)
        {
            //this.database = dbContext;
            this.iLotRepo = lotRepo;
            this.iLotTypeRepo = lotTypeRepo;
        }

        [Authorize(Roles = "WVUEmployee, ParkingEmployee")]
        public IActionResult ShowAllLots() //tests logic of controller methods
        {
            //Two options:
            //1.Get data from database
            //2.Inject mock data from test class/method
            List<Lot> lotList = iLotRepo.ListAllLots(); //database.Lots.Include(l => l.LotStatuses).ThenInclude(ls => ls.LotType).ToList<Lot>();

            //Send the data to the UI layer
            return View(lotList);
        }

        [HttpGet]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AddLot()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AddLot(Lot lot)
        {
            if (ModelState.IsValid)
            {
                iLotRepo.AddLot(lot).Wait();
                return RedirectToAction("ShowAllLots");
            }
            else
            {
                return View(lot);
            }
            
        }

        //Client (browser) <-> Web Server communication
        [HttpGet]
        public IActionResult SearchLots()
        {
            ViewData["LotTypes"] = new SelectList(iLotTypeRepo.ListAllLotTypes(), "LotTypeID", "LotTypeName");

            LotSearchViewModel searchViewModel = new LotSearchViewModel();

            return View(searchViewModel);
        }


        //For user inputs, we will use a ViewModel
        //For Search, inputs from user, output (List)
        //[HttpPost]
        public IActionResult SearchLotsResult(string searchButton, string sortOrder, bool IsLotCurrentlyAvailable, string TypeOfDay, int? LotTypeID,  int pageNumber, LotSearchViewModel searchViewModel, int InputPageSize = 2)
        {
            ViewData["LotNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "lotname_desc" : "";

            ViewData["CurrentSortOrder"] = sortOrder;

            ViewData["LotTypes"] = new SelectList(iLotTypeRepo.ListAllLotTypes(), "LotTypeID", "LotTypeName");

            if(searchButton == "Active")
            {
                IsLotCurrentlyAvailable = searchViewModel.IsLotCurrentlyAvailable;
                ViewData["IsLotCurrentlyAvailable"] = IsLotCurrentlyAvailable;

                TypeOfDay = searchViewModel.TypeOfDay;
                ViewData["TypeOfDay"] = TypeOfDay;

                LotTypeID = searchViewModel.LotTypeID;
                ViewData["LotTypeID"] = LotTypeID;

                //InputPageSize = searchViewModel.InputPageSize;
                ViewData["InputPageSize"] = InputPageSize;
            }
            else
            {
                ViewData["IsLotCurrentlyAvailable"] = IsLotCurrentlyAvailable;
                ViewData["TypeOfDay"] = TypeOfDay;
                ViewData["LotTypeID"] = LotTypeID;
                ViewData["InputPageSize"] = InputPageSize;
            }

            //For test, this data will be injected with the mock data
            List<Lot> listOfLots = iLotRepo.ListAllLots();

            //Do the searching based on user input criteria
            if (IsLotCurrentlyAvailable)
            {
                listOfLots = listOfLots.Where
                    (
                        l => l.MaxCapacity > l.CurrentOccupancy
                    ).ToList();
            }

            if (TypeOfDay != null)
            {
                listOfLots = listOfLots.Where
                    (
                        l => l.LotStatuses.Any(ls => ls.TypeOfDay == searchViewModel.TypeOfDay)
                    ).ToList();
            }

            if (LotTypeID != null)
            {
                listOfLots = listOfLots.Where
                    (
                        l => l.LotStatuses.Any(ls => ls.LotTypeID == searchViewModel.LotTypeID)
                    ).ToList();
            }

            switch (sortOrder)
            {
                case "lotname_desc":
                    listOfLots = listOfLots.OrderByDescending(l => l.LocationName).ToList();
                    ViewData["LotNameImage"] = "descending";
                    break;

                default:
                    listOfLots = listOfLots.OrderBy(l => l.LocationName).ToList();
                    break;
            }

            int totalItems = listOfLots.Count();
            int pageSize = InputPageSize; // from user (selecting value)
            //int pageNumber = 2; // from user (clicking)
            int excludeRows = (pageSize * pageNumber) - pageSize;

            listOfLots = listOfLots.Skip(excludeRows).Take(pageSize).ToList();

            //Get search result (list of lots)
            searchViewModel.LotSearchResult.Data = listOfLots;
            searchViewModel.LotSearchResult.PageNumber = pageNumber;
            searchViewModel.LotSearchResult.PageSize = pageSize;
            searchViewModel.LotSearchResult.TotalItems = totalItems;

            return View("SearchLots", searchViewModel);
        }


        [HttpGet]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult EditLot(int? lotID)
        {
            Lot lot = iLotRepo.FindLot(lotID);

            return View(lot);
        }

        [HttpPost]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult EditLot(Lot lot)
        {
            if (ModelState.IsValid)
            {
                iLotRepo.EditLot(lot).Wait();
                return RedirectToAction("ShowAllLots");
            }
            else
            {
                return View(lot);
            }
            
        }

        
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult DeleteLot(Lot lot)
        {
            iLotRepo.DeleteLot(lot).Wait();
            return RedirectToAction("ShowAllLots");
        }

        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult ConfirmDeleteLot(int? lotID)
        {
            Lot lot = iLotRepo.FindLot(lotID);
            return View(lot);
        }
    }
}

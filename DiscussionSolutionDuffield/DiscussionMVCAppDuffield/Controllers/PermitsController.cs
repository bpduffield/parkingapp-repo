using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.Services;
using DiscussionMVCAppDuffield.ViewModels;

using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using GoogleApi.Entities.Maps.StaticMaps.Response;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiscussionMVCAppDuffield.Controllers
{
    public class PermitsController : Controller
    {
        private IPermitRepo iPermitRepo;
        private ILotRepo iLotRepo;
        private ILotStatusRepo iLotStatusRepo;
        private IApplicationUserRepo iApplicationUserRepo;
        
        //ctor <tab> <tab>
        public PermitsController(IPermitRepo permitRepo, ILotRepo lotRepo, ILotStatusRepo lotStatusRepo, IApplicationUserRepo applicationUserRepo)
        {
            this.iPermitRepo = permitRepo;
            this.iLotRepo = lotRepo;
            this.iLotStatusRepo = lotStatusRepo;
            this.iApplicationUserRepo = applicationUserRepo;
        }

        public FileResult DisplayParkingLotsOnMap()
        {
            List<Lot> allLots = iLotRepo.ListAllLots();
            List<MapMarker> mapMarkers = new List<MapMarker>();

            foreach(Lot eachLot in allLots)
            {
                MapMarker eachMapMarker = new MapMarker();
                List<Location> locations = new List<Location>();
                Location eachLocation = new Location(eachLot.LotAddress);
                locations.Add(eachLocation);
                eachMapMarker.Locations = locations;

                eachMapMarker.Label = eachLot.LotNumber; //can only be 0-9 or A-Z

                eachMapMarker.Color = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapColor.Blue;

                mapMarkers.Add(eachMapMarker);
            }

            StaticMapsRequest staticMapsRequest = new StaticMapsRequest();

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("WVUEmployeeID")))
            {
                string WVUEmployeeID = HttpContext.Session.GetString("WVUEmployeeID");
                WVUEmployee employee = iApplicationUserRepo.FindWvuEmployee(WVUEmployeeID);
                string origin = employee.Department.DepartmentAddress;

                MapMarker eachMapMarker = new MapMarker();
                List<Location> locations = new List<Location>();
                Location employeeLocation = new Location(origin);
                locations.Add(employeeLocation);
                eachMapMarker.Locations = locations;

                eachMapMarker.Label = "E"; //can only be 0-9 or A-Z

                eachMapMarker.Color = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapColor.Purple;

                mapMarkers.Add(eachMapMarker);

                staticMapsRequest.Center = employeeLocation;
            }


            staticMapsRequest.Key = "AIzaSyBRT65jDLR_mhb4yzGbMBMaIZALz57028A";

            staticMapsRequest.Markers = mapMarkers;

            staticMapsRequest.Type = GoogleApi.Entities.Maps.StaticMaps.Request.Enums.MapType.Roadmap;

            StaticMapsResponse response = GoogleApi.GoogleMaps.StaticMaps.Query(staticMapsRequest);

            var file = response.Buffer;

            return File(file, "image/jpeg");
        }

        public void DropDownListForEmployees()
        {
            ViewData["WVUEmployees"] = new SelectList(iApplicationUserRepo.ListAllWVUEmployees(), "Id", "EmployeeNameAndDepartment");
        }

        [HttpGet]
        public IActionResult DetermineDistanceMatrix()
        {
            DropDownListForEmployees();
            DistanceMatrixViewModel viewModel = new DistanceMatrixViewModel();
            return View(viewModel);
        }

        //[HttpPost]
        public IActionResult DetermineDistanceMatrixResult(string sortOrder, DistanceMatrixViewModel inputViewModel)
        {
            //Ternary conditional operator
            //if()? true : false
            ViewData["DistanceSortParam"] = String.IsNullOrEmpty(sortOrder) ? "distance_desc" : "";

            ViewData["DurationSortParam"] = sortOrder == "duration" ? "duration_desc" : "duration";

            string WVUEmployeeID = inputViewModel.WvuEmployeeID;

            //To use session objects, need to set statements in Startup.cs
            if(WVUEmployeeID != null)
            {
                HttpContext.Session.SetString("WVUEmployeeID", WVUEmployeeID);
            }

            if(!String.IsNullOrEmpty(HttpContext.Session.GetString("WVUEmployeeID")))
            {
                WVUEmployeeID = HttpContext.Session.GetString("WVUEmployeeID");
            }

            WVUEmployee employee = iApplicationUserRepo.FindWvuEmployee(WVUEmployeeID);
            string origin = employee.Department.DepartmentAddress;

            if(inputViewModel.WvuEmployeeID != null)
            {
                inputViewModel = CreateDistanceMatrix(origin);
                HttpContext.Session.SetComplexData("inputViewModel", inputViewModel);
            }
            else
            {
                inputViewModel = HttpContext.Session.GetComplexData<DistanceMatrixViewModel>("inputViewModel");
            }
            

            switch (sortOrder)
            {
                case "distance_desc": inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderByDescending(d => d.DistanceInMiles).ToList();
                    ViewData["DistanceImage"] = "descending";
                    break;

                case "duration": inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderBy(d => d.DurationInMinutes).ToList();
                    break;

                case "duration_desc": inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderByDescending(d => d.DurationInMinutes).ToList();
                    ViewData["DurationImage"] = "descending";
                    break;

                default: inputViewModel.DistanceMatrix = inputViewModel.DistanceMatrix.OrderBy(d => d.DistanceInMiles).ToList();
                    break;
            }
          

            DropDownListForEmployees();

            return View("DetermineDistanceMatrix",inputViewModel);
        }

        public DistanceMatrixViewModel CreateDistanceMatrix(string origin)
        {
            DistanceMatrixViewModel inputViewModel = new DistanceMatrixViewModel();
            inputViewModel.DistanceMatrix = new List<DistanceMatrixViewModel>();

            List<Location> destinationLocations = new List<Location>();

            List<Lot> allLots = iLotRepo.ListAllLots();

            foreach (Lot eachLot in allLots)
            {
                DistanceMatrixViewModel processingViewModel = new DistanceMatrixViewModel();
                processingViewModel.LotID = eachLot.LotID;
                processingViewModel.LotAddress = eachLot.LotAddress;
                processingViewModel.LotNumber = eachLot.LotNumber;
                processingViewModel.AvailableSpots = eachLot.MaxCapacity - eachLot.CurrentOccupancy;
                processingViewModel.LotName = eachLot.LocationName;

                inputViewModel.DistanceMatrix.Add(processingViewModel);

                destinationLocations.Add(new Location(eachLot.LotAddress));
            }

            DistanceMatrixResponse response = FindDistanceAndDurationBetweenOriginAndDestinations(origin, destinationLocations);

            Row row = response.Rows.FirstOrDefault();
            List<Element> elements = row.Elements.ToList();
            int distanceInMeters;
            double distanceInMiles;
            const double metersToMileConverter = 0.00062137;
            int durationInSeconds;
            int durationInMinutes;
            int index = 0;

            foreach (Element eachElement in elements)// need to do this for all 4
            {
                distanceInMeters = eachElement.Distance.Value;
                distanceInMiles = Math.Round((distanceInMeters * metersToMileConverter), 2);
                durationInSeconds = eachElement.Duration.Value;
                durationInMinutes = durationInSeconds / 60;
                inputViewModel.DistanceMatrix[index].DistanceInMiles = distanceInMiles;
                inputViewModel.DistanceMatrix[index].DurationInMinutes = durationInMinutes;
                index++;
            }

            return (inputViewModel);
        }

        public DistanceMatrixResponse FindDistanceAndDurationBetweenOriginAndDestinations(string origin, List<Location> destinationLocations)
        {
            //string origin = "1601 University Ave, Morgantown, WV 26506";
            //string destination = "2001 Rec Center Dr, Morgantown, WV 26506";

            DistanceMatrixRequest request = new DistanceMatrixRequest();
            request.Key = "AIzaSyBRT65jDLR_mhb4yzGbMBMaIZALz57028A";

            List<Location> originLocations = new List<Location>();
            Location originLocation = new Location(origin);
            originLocations.Add(originLocation);

            request.Origins = originLocations;
            request.Destinations = destinationLocations;

            DistanceMatrixResponse response = GoogleApi.GoogleMaps.DistanceMatrix.Query(request);
            return response;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListAllPermits()
        {
            List<Permit> permitList = iPermitRepo.ListAllPermits();
            return View(permitList);
        }

        [HttpGet]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AssignPermit()
        {
            ViewData["Lots"] = new SelectList(iLotRepo.ListAllLots(), "LotID", "LocationName");
            ViewData["Employees"] = new SelectList(iApplicationUserRepo.ListAllWVUEmployees(), "Id", "UserName");
            //Do the same for all WVUEmployees
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "ParkingEmployee")]
        public IActionResult AssignPermit(AssignPermitViewModel viewModel)
        {
            string parkingEmployeeID = iApplicationUserRepo.FindUserID();
                //= User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            //bool assignPermit;

            string errorMessage = "None";

            bool employeeHasPermit = iPermitRepo.DoesWVUEmployeeHavePermit(viewModel.WVUEmployeeID);

            if (employeeHasPermit)
            {
                errorMessage = "Employee already has a permit";
                ModelState.AddModelError("EmployeePermit", errorMessage);
            }

            bool lotAvailable = iLotRepo.IsChosenLotAvailable(viewModel.LotID);

            if (!lotAvailable)
            {
                errorMessage = "Lot is not available";
                ModelState.AddModelError("LotAvailable", errorMessage);
            }

            if (!employeeHasPermit && lotAvailable)
            {
                int lotTypeID = 3;
                double permitAmount = iLotStatusRepo.FindPermitAmount(viewModel.LotID, lotTypeID);

                //DateTime startDate = DateTime.Today.Date;
                DateTime endDate = viewModel.StartDate.AddYears(1);
                
                Permit permit = new Permit(permitAmount, viewModel.StartDate, endDate, viewModel.WVUEmployeeID, parkingEmployeeID);
                iPermitRepo.AddPermit(permit).Wait();

                //lot CurrentOccupancy to +1
                Lot lot = iLotRepo.FindLot(viewModel.LotID);
                lot.CurrentOccupancy += 1;
                iLotRepo.EditLot(lot).Wait();
                return RedirectToAction("ListAllPermits");

                
            }
            else
            {
                ViewData["Lots"] = new SelectList(iLotRepo.ListAllLots(), "LotID", "LocationName");
                ViewData["WVUEmployees"] = new SelectList(iApplicationUserRepo.ListAllWVUEmployees(), "Id", "Fullname");
                //This has to be mocked and injected into method
                //ViewData["Lots"] = new SelectList(iLotRepo.ListAllLots(), "LotID", "LocationName");
                return View(viewModel);   
            }

            
        }
    }
}
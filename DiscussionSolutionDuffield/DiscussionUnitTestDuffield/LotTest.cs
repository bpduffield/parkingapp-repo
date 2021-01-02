using DiscussionMVCAppDuffield.Controllers;
using DiscussionMVCAppDuffield.Data;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace DiscussionUnitTestDuffield
{
    public class LotTest
    {
        //When you access the database then this is no longer a unit test
        //Youre dependent on the database which is outside application
        private Mock<ILotRepo> mockLotRepo;
        private Mock<ILotTypeRepo> mockLotTypeRepo;
        private LotsController controller;
        //Dependency Inversion
        //Instead of being dependent on the database
        //Change the dependency direction

        public LotTest()
        {
            this.mockLotRepo = new Mock<ILotRepo>();
            this.mockLotTypeRepo = new Mock<ILotTypeRepo>();
            controller = new LotsController(mockLotRepo.Object, mockLotTypeRepo.Object);
        }

        [Fact]//Happy Path - Everything goes well
        public void ShouldAddLot()
        {
            //Arrange
            Lot lot = new Lot("0001", "Test Lot", "Test Address", 10);

            mockLotRepo.Setup(m => m.AddLot(It.IsAny<Lot>())); //Fluent notation

            //Act
           
            controller.AddLot(lot);


            //Assert
            mockLotRepo.Verify(m => m.AddLot(It.IsAny<Lot>()), Times.Once);
        }

        [Fact]//Failing path
        public void ShouldNotAddLot()
        {
            Lot addedLot = new Lot();
            addedLot.LocationName = "B&E Parking Lot";

            //string expectedErrorMessage = "The LotNumber field is required";

            var validationResult = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(addedLot, new ValidationContext(addedLot), validationResult);

            //string actualErrorMessage = validationResult[0].ErrorMessage;

            Assert.False(isValid);
            //Assert.Equal(expectedErrorMessage, actualErrorMessage);
        }

        [Fact]//Happy Path - Everything goes well
        public void ShouldEditLot()
        {
            //Arrange
            Lot lot = new Lot("0001", "Test Lot", "Test Address", 10);

            mockLotRepo.Setup(m => m.EditLot(It.IsAny<Lot>())); //Fluent notation

            //Act

            controller.EditLot(lot);


            //Assert
            mockLotRepo.Verify(m => m.EditLot(It.IsAny<Lot>()), Times.Once);
        }

        [Fact]
        public void ShouldDeleteLot()
        {
            //Arrange
            Lot lot = new Lot("0001", "Test Lot", "Test Address", 10);

            mockLotRepo.Setup(m => m.DeleteLot(It.IsAny<Lot>())); //Fluent notation

            //Act

            controller.DeleteLot(lot);


            //Assert
            mockLotRepo.Verify(m => m.DeleteLot(It.IsAny<Lot>()), Times.Once);
        }

        [Fact]
        public void ShouldNotDeleteLot()
        {
           Lot lot = new Lot();
           lot.LotID = 100;
            mockLotRepo.Setup(m => m.DeleteLot(It.IsAny<Lot>())); //Fluent notation

            //Act

            //controller.DeleteLot(lot);
            controller.ConfirmDeleteLot(lot.LotID);


            //Assert
            mockLotRepo.Verify(m => m.DeleteLot(It.IsAny<Lot>()), Times.Never);
        }


        //health of an app -> tests that validate logic 
        //test coverage: how much of your code is tested?
        //Red/Green testing
        [Fact]//test first development //test driven development (TDD)
        public void ShouldSearchAllLots()
        {
            //AAA test template
            //1) Arrange
            List<Lot> mockLotList = CreateMockLotData();
            List<LotType> mockLotTypeList = CreateMockLotTypeData();


            //logic not on implementation of interface but in controller
            mockLotRepo.Setup(m => m.ListAllLots()).Returns(mockLotList);
            mockLotTypeRepo.Setup(lt => lt.ListAllLotTypes()).Returns(mockLotTypeList);

            //Mock data injected into SearchLots controller method
            //How many lot objects/rows will be injected into that method? 4
            

            LotSearchViewModel searchViewModel = new LotSearchViewModel();
            searchViewModel.IsLotCurrentlyAvailable = true;

            int expectedNumberOfLots = 4;



            //2) Act
            //Casting(to match left and right sides of assignment)
            //testing logic of controller methods
            //Test post methods
            //SearchLots
            string sortOrder = null;
            string searchButton = null;
            bool isLotCurrentlyAvailable = false;
            string typeOfDay = null;
            int? lotTypeID = null;
            int InputPageSize = 4;
            int pageNumber = 1;

            ViewResult result = controller.SearchLotsResult(searchButton, sortOrder, isLotCurrentlyAvailable, typeOfDay, lotTypeID, pageNumber, searchViewModel, InputPageSize) as ViewResult;
            LotSearchViewModel resultModel = result.Model as LotSearchViewModel;
            List<Lot> lotList = resultModel.LotSearchResult.Data;
            int actualNumberOfLots = lotList.Count;

            //3) Assert
            //What is expected compared with the actual
            Assert.Equal(expectedNumberOfLots, actualNumberOfLots);
            
        }

        [Fact]
        public void ShouldSearchAllWeekdayLots()
        {
            //AAA test template
            //1) Arrange
            List<Lot> mockLotList = CreateMockLotData();
            List<LotType> mockLotTypeList = CreateMockLotTypeData();

            //logic not on implementation of interface but in controller
            mockLotRepo.Setup(m => m.ListAllLots()).Returns(mockLotList);
            mockLotTypeRepo.Setup(lt => lt.ListAllLotTypes()).Returns(mockLotTypeList);

            //Mock data injected into SearchLots controller method
            //How many lot objects/rows will be injected into that method? 4
            

            LotSearchViewModel searchViewModel = new LotSearchViewModel();
            searchViewModel.IsLotCurrentlyAvailable = true;
            searchViewModel.TypeOfDay = "Weekday";

            int expectedNumberOfLots = 3;



            //2) Act
            //Casting(to match left and right sides of assignment)
            //testing logic of controller methods
            //Test post methods
            //SearchLots
            string sortOrder = null;
            string searchButton = null;
            bool isLotCurrentlyAvailable = false;
            string typeOfDay = null;
            int? lotTypeID = null;
            int InputPageSize = 3;
            int pageNumber = 1;

            ViewResult result = controller.SearchLotsResult(searchButton, sortOrder, isLotCurrentlyAvailable, typeOfDay, lotTypeID, pageNumber, searchViewModel, InputPageSize) as ViewResult;
            LotSearchViewModel resultModel = result.Model as LotSearchViewModel;
            List<Lot> lotList = resultModel.LotSearchResult.Data;
            int actualNumberOfLots = lotList.Count;

            //3) Assert
            //What is expected compared with the actual
            Assert.Equal(expectedNumberOfLots, actualNumberOfLots);

        }

        [Fact]
        public void ShouldListAvailableLots()
        {
            //AAA test template
            //1) Arrange
            List<Lot> mockLotList = CreateMockLotData();

            mockLotRepo.Setup(m => m.ListAllLots()).Returns(mockLotList);

            
            int expectedNumberOfLots = mockLotList.Count;

            //2) Act
            //Casting(to match left and right sides of assignment)
            //testing logic of controller methods
            //ListAllLots
            ViewResult result = controller.ShowAllLots() as ViewResult;
            List<Lot> lotList = result.Model as List<Lot>;
            int actualNumberOfLots = lotList.Count;

            //3) Assert
            //What is expected compared with the actual
            Assert.Equal(expectedNumberOfLots, actualNumberOfLots);

        }

        //helper method: generate mock data
        public List<Lot> CreateMockLotData()
        {
            List<Lot> mockLotData = new List<Lot>();

            Lot lot = new Lot("100", "Test Lot 1", "Test Address 1", 100);
            lot.LotID = 1;
            lot.CurrentOccupancy = 50;
            mockLotData.Add(lot);

            DateTime startDateTime = DateTime.Parse("12:00 AM");
            DateTime endDateTime = DateTime.Parse("5:00 AM");

            LotStatus lotStatus = new LotStatus("Weekday", startDateTime, endDateTime, 0, 1, 1);
            lot.LotStatuses.Add(lotStatus);



            lot = new Lot("200", "Test Lot 2", "Test Address 2", 200);
            lot.LotID = 2;
            lot.CurrentOccupancy = 100;

            lotStatus = new LotStatus("Weekend", startDateTime, endDateTime, 0, 1, 1);
            lot.LotStatuses.Add(lotStatus);

            mockLotData.Add(lot);

            lot = new Lot("300", "Test Lot 3", "Test Address 3", 300);
            lot.LotID = 3;
            lot.CurrentOccupancy = 100;

            lotStatus = new LotStatus("Weekday", startDateTime, endDateTime, 0, 1, 1);
            lot.LotStatuses.Add(lotStatus);

            lotStatus = new LotStatus("Weekend", startDateTime, endDateTime, 0, 1, 1);
            lot.LotStatuses.Add(lotStatus);

            mockLotData.Add(lot);

            lot = new Lot("400", "Test Lot 4", "Test Address 4", 400);
            lot.LotID = 4;
            lot.CurrentOccupancy = 300;

            lotStatus = new LotStatus("Weekday", startDateTime, endDateTime, 0, 1, 1);
            lot.LotStatuses.Add(lotStatus);

            mockLotData.Add(lot);

            return mockLotData;
        }

        public List<LotType> CreateMockLotTypeData()
        {
            List<LotType> mockLotTypeData = new List<LotType>(); 
            LotType lotType = new LotType("TestLotTypeName1"); 
            lotType.LotTypeID = 1; 
            mockLotTypeData.Add(lotType); 
            
            new LotType("TestLotTypeName2"); 
            lotType.LotTypeID = 2; 
            mockLotTypeData.Add(lotType); 
            
            return mockLotTypeData;
        }
    
    
    }

}

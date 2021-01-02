using DiscussionMVCAppDuffield.Controllers;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiscussionUnitTestDuffield
{
    public class PermitTest
    {
        private PermitsController controller;
        private Mock<IPermitRepo> mockPermitRepo;
        private Mock<ILotRepo> mockLotRepo;
        private Mock<ILotStatusRepo> mockLotStatusRepo;
        private Mock<IApplicationUserRepo> mockApplicationUserRepo;

        public PermitTest()
        {          
            this.mockPermitRepo = new Mock<IPermitRepo>();
            this.mockLotRepo = new Mock<ILotRepo>();
            this.mockLotStatusRepo = new Mock<ILotStatusRepo>();
            this.mockApplicationUserRepo = new Mock<IApplicationUserRepo>();
            controller = new PermitsController(mockPermitRepo.Object, mockLotRepo.Object, mockLotStatusRepo.Object, mockApplicationUserRepo.Object);
        }

        [Fact]
        public void ShouldAssignPermit()
        {
            string parkingEmployeeID = "999999";

            mockApplicationUserRepo.Setup(m => m.FindUserID()).Returns(parkingEmployeeID);

            //Arrange
            string wvuEmployeeID = "001";

            Lot lot = new Lot("999", "Test Name", "Test Address", 10);
            lot.LotID = 100;
            lot.CurrentOccupancy = 8;

            int lotTypeID = 3;

            DateTime startDate = new DateTime(2020, 10, 5);

            int expectedCurrentlyOccupiedSpotsAfterAssignment = 9;

            //Does chosen employee already have permit? False
            mockPermitRepo.Setup(m => m.DoesWVUEmployeeHavePermit(wvuEmployeeID)).Returns(false);

            //Is the chosen lot available?
            mockLotRepo.Setup(l => l.IsChosenLotAvailable(lot.LotID)).Returns(true);

            //If both conditions are met then assign permit
            mockLotStatusRepo.Setup(s => s.FindPermitAmount(lot.LotID, lotTypeID)).Returns(500.00);

            //IF an object is created in controller method we can get access to that object using Callback on the mock setup
            Permit permit = null;
            mockPermitRepo.Setup(p => p.AddPermit(It.IsAny<Permit>())).Returns(Task.CompletedTask).Callback<Permit>(p => permit = p);


            mockLotRepo.Setup(l => l.FindLot(lot.LotID)).Returns(lot);

            AssignPermitViewModel viewModel = new AssignPermitViewModel();
            viewModel.WVUEmployeeID = wvuEmployeeID;
            viewModel.LotID = lot.LotID;
            viewModel.StartDate = startDate;

            double expectedPermitAmount = 500;

            //Act
            controller.AssignPermit(viewModel);

            //Assert
            
            Assert.Equal(expectedCurrentlyOccupiedSpotsAfterAssignment, lot.CurrentOccupancy);
            Assert.Equal(expectedPermitAmount, permit.PermitAmount);
        }

        [Fact]
        public void ShouldNotAssignPermitAsEmployeeAlreadyHasPermit()
        {

            //Arrange
            string wvuEmployeeID = "001";

            Lot lot = new Lot("999", "Test Name", "Test Address", 10);
            lot.LotID = 100;
            lot.CurrentOccupancy = 8;

            int lotTypeID = 3;

            DateTime startDate = new DateTime(2020, 10, 5);

            int expectedCurrentlyOccupiedSpotsAfterAssignment = 8;

            //Does chosen employee already have permit? True
            mockPermitRepo.Setup(m => m.DoesWVUEmployeeHavePermit(wvuEmployeeID)).Returns(true);

            //Is the chosen lot available?
            mockLotRepo.Setup(l => l.IsChosenLotAvailable(lot.LotID)).Returns(true);

            //If both conditions are met then assign permit
            mockLotStatusRepo.Setup(s => s.FindPermitAmount(lot.LotID, lotTypeID)).Returns(500.00);

            //IF an object is created in controller method we can get access to that object using Callback on the mock setup
            Permit permit = null;
            mockPermitRepo.Setup(p => p.AddPermit(It.IsAny<Permit>())).Returns(Task.CompletedTask).Callback<Permit>(p => permit = p);


            mockLotRepo.Setup(l => l.FindLot(lot.LotID)).Returns(lot);

            AssignPermitViewModel viewModel = new AssignPermitViewModel();
            viewModel.WVUEmployeeID = wvuEmployeeID;
            viewModel.LotID = lot.LotID;
            viewModel.StartDate = startDate;

            string expectedErrorMessage = "Employee already has a permit";
            // double expectedPermitAmount = 500;

            mockLotRepo.Setup(l => l.ListAllLots()).Returns(new List<Lot>());
            mockApplicationUserRepo.Setup(a => a.ListAllWVUEmployees()).Returns(new List<WVUEmployee>());            
            
            //Act
            controller.AssignPermit(viewModel);

            //Assert

            Assert.Equal(expectedCurrentlyOccupiedSpotsAfterAssignment, lot.CurrentOccupancy);
            Assert.Equal(expectedErrorMessage, controller.ModelState["EmployeePermit"].Errors[0].ErrorMessage);
           // Assert.Equal(expectedPermitAmount, permit.PermitAmount);
        }

        [Fact]
        public void ShouldNotAssignPermitAsLotIsUnavailable()
        {

            //Arrange
            string wvuEmployeeID = "001";

            Lot lot = new Lot("999", "Test Name", "Test Address", 10);
            lot.LotID = 100;
            lot.CurrentOccupancy = 8;

            int lotTypeID = 3;

            DateTime startDate = new DateTime(2020, 10, 5);

            int expectedCurrentlyOccupiedSpotsAfterAssignment = 8;

            //Does chosen employee already have permit? True
            mockPermitRepo.Setup(m => m.DoesWVUEmployeeHavePermit(wvuEmployeeID)).Returns(true);

            //Is the chosen lot available?
            mockLotRepo.Setup(l => l.IsChosenLotAvailable(lot.LotID)).Returns(false);

            //If both conditions are met then assign permit
            mockLotStatusRepo.Setup(s => s.FindPermitAmount(lot.LotID, lotTypeID)).Returns(500.00);

            //IF an object is created in controller method we can get access to that object using Callback on the mock setup
            Permit permit = null;
            mockPermitRepo.Setup(p => p.AddPermit(It.IsAny<Permit>())).Returns(Task.CompletedTask).Callback<Permit>(p => permit = p);


            mockLotRepo.Setup(l => l.FindLot(lot.LotID)).Returns(lot);

            AssignPermitViewModel viewModel = new AssignPermitViewModel();
            viewModel.WVUEmployeeID = wvuEmployeeID;
            viewModel.LotID = lot.LotID;
            viewModel.StartDate = startDate;

            string expectedErrorMessage = "Lot is not available";
            // double expectedPermitAmount = 500;

            mockLotRepo.Setup(l => l.ListAllLots()).Returns(new List<Lot>());
            mockApplicationUserRepo.Setup(a => a.ListAllWVUEmployees()).Returns(new List<WVUEmployee>())
;            //Act
            controller.AssignPermit(viewModel);

            //Assert

            Assert.Equal(expectedCurrentlyOccupiedSpotsAfterAssignment, lot.CurrentOccupancy);
            Assert.Equal(expectedErrorMessage, controller.ModelState["LotAvailable"].Errors[0].ErrorMessage);
        }

        public List<Lot> CreateMockPermitData()
        {
            List<Lot> mockPermitData = new List<Lot>();

            Lot lot = new Lot("100", "Test Lot 1", "Test Address 1", 100);
            lot.LotID = 1;
            lot.CurrentOccupancy = 50;
            mockPermitData.Add(lot);

            lot = new Lot("200", "Test Lot 2", "Test Address 2", 200);
            lot.LotID = 2;
            lot.CurrentOccupancy = 100;
            mockPermitData.Add(lot);

            lot = new Lot("300", "Test Lot 3", "Test Address 3", 200);
            lot.LotID = 3;
            lot.CurrentOccupancy = 200;
            mockPermitData.Add(lot);

            lot = new Lot("400", "Test Lot 4", "Test Address 4", 200);
            lot.LotID = 4;
            lot.CurrentOccupancy = 100;
            mockPermitData.Add(lot);

            return mockPermitData;
        }

        public List<WVUEmployee> CreateMockWVUEmployeeData()
        {
            List<WVUEmployee> mockWVUEmployeeData = new List<WVUEmployee>();

            WVUEmployee employee = new WVUEmployee("John", "Doe", "jdoe@mix.wvu.edu", "3041112222", "jdoe", 1);
            employee.PermitID = 1;
            mockWVUEmployeeData.Add(employee);

            employee = new WVUEmployee("Jane", "Doe", "jadoe@mix.wvu.edu", "3042222222", "jadoe", 2);
            mockWVUEmployeeData.Add(employee);

            employee = new WVUEmployee("Dan", "Johnson", "djohnson@mix.wvu.edu", "3042223333", "djohnson", 1);
            mockWVUEmployeeData.Add(employee);

            employee = new WVUEmployee("Jennifer", "Smith", "jsmith@mix.wvu.edu", "3043332222", "jsmith", 2);
            mockWVUEmployeeData.Add(employee);

            return mockWVUEmployeeData;
        }
    }
}

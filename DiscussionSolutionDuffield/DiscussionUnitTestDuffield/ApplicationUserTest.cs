using DiscussionMVCAppDuffield.Controllers;
using DiscussionMVCAppDuffield.Models;
using DiscussionMVCAppDuffield.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DiscussionUnitTestDuffield
{
    public class ApplicationUserTest
    {
        private readonly Mock<IApplicationUserRepo> mockApplicationUserRepo;
        

        public ApplicationUserTest()
        {
            this.mockApplicationUserRepo = new Mock<IApplicationUserRepo>();           
        }

        [Fact]
        public void ShouldSearchAllAppUsersByUserType()
        {
            List<ApplicationUser> mockAppUsersList = CreateMockAppUsersData();

            mockApplicationUserRepo.Setup(m => m.ListAllAppUsers()).Returns(mockAppUsersList);

            int expectedAppUsers = 1;

            AppUsersController controller = new AppUsersController(mockApplicationUserRepo.Object);

            AppUserSearchViewModel viewModel = new AppUserSearchViewModel();
            viewModel.appUserType = "Visitor";

            ViewResult result = controller.SearchAppUsers(viewModel) as ViewResult;

            AppUserSearchViewModel resultModel = result.Model as AppUserSearchViewModel;

            List<ApplicationUser> actualApplicationUserList = resultModel.AppUserSearchResult;

            int actualAppUsers = actualApplicationUserList.Count;

            Assert.Equal(expectedAppUsers, actualAppUsers);
        }

        public List<ApplicationUser> CreateMockAppUsersData()
        {
            List<ApplicationUser> mockAppUserData = new List<ApplicationUser>();

            Visitor visitor = new Visitor("Test","Visitor1","Test.Visitor1@test.local","3040001111", "Test.Visitor1", "TestVisitorOrganization");
            visitor.Id = "1";            
            mockAppUserData.Add(visitor);
                       
            WVUEmployee employee = new WVUEmployee("Test", "Employee1", "Test.Employee1@test.com","3041110000", "Test.Employee1", 1);
            employee.Id = "2";                     
            mockAppUserData.Add(employee);         

            return mockAppUserData;
        }
    }
}

using System.Threading.Tasks;
using Dashboard.Common.Utilities;
using Dashboard.Facade.BorrowingProfile;
using Dashboard.Models;
using Dashboard.Models.BorrowingProfile.Models;
using Dashboard.Models.BorrowingProfile.Settings;
using Dashboard.Models.BorrowingProfile.ViewModels;
using Dashboard.Web.WebAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Dashboard.Tests.Web
{
    public class BorrowingProfileControllerTests
    {
        private readonly BorrowingProfileFilter _filter;
        private readonly Mock<IOptions<BorrowingProfileClientSettings>> _settingsMock;

        public BorrowingProfileControllerTests()
        {            
            AutoMapperConfiguration.Initialize();
            _filter = BorrowingProfileFilter.Default;

            var clientSettingsMock = new Mock<IBorrowingProfileClientSettings>();
            clientSettingsMock.Setup(x => x.GetApplicationName()).Returns("App Test");
            _settingsMock = new Mock<IOptions<BorrowingProfileClientSettings>>();
            _settingsMock.Setup(x => x.Value).Returns(clientSettingsMock.Object as BorrowingProfileClientSettings);
        }

        [Fact]
        public async Task GetBorrowingProfileSettings_test()
        {
            //Arrange
            var facadeMock = new Mock<IBorrowingProfileFacade>();
            var controller = new BorrowingProfileController(facadeMock.Object, _settingsMock.Object);

            //Act
            var result = await controller.GetBorrowingProfileSettings();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task Post_BorrowingProfileFilter_test()
        {
            //Arrange
            var facadeMock = new Mock<IBorrowingProfileFacade>();
            facadeMock.Setup(x => x.GetTargetAsync(FiscalYear.Now)).ReturnsAsync(
                new ProfileDataModel
                    {
                        ProfileName = "Test",
                        ConsolidatedAmount = 100.00,
                        ProvinceAmount = 100.00,
                        OefcAmount = 100.00,
                        PercentBorrowed=10.00,
                        Deals=10
                });

            facadeMock.Setup(x => x.GetProfilesDataAsync(It.IsAny<BorrowingProfileFilter>())).ReturnsAsync(
                new[]
                {
                    new ProfileDataModel
                    {
                        ProfileName = "Test",
                        ConsolidatedAmount = 100.00,
                        ProvinceAmount = 100.00,
                        OefcAmount = 100.00,
                        PercentBorrowed=10.00,
                        Deals=10
                    }
                });

            facadeMock.Setup(x => x.GetHedgesAsync(It.IsAny<BorrowingProfileFilter>())).ReturnsAsync(
                new[] {new ProfileDataModel{ProfileName="Test"}});
            var controller = new BorrowingProfileController(facadeMock.Object, _settingsMock.Object);

            //Act
            var result1 = await controller.Post(null);
            var result2 = await controller.Post(_filter);

            //Assert
            var okResult = result2.Should().BeOfType<OkObjectResult>().Subject;
            var valueResult = okResult.Value.Should().BeAssignableTo<BorrowingProfileViewModel>().Subject;
            valueResult.ElapsedTime.Equals(100);
            valueResult.Fiscals.Length.Should().Be(10);

            var okResult1 = result1.Should().BeOfType<OkObjectResult>().Subject;
            okResult1.Value.Should().BeEquivalentTo(valueResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Post_Profile_Trades_DealId0_test(int dealId)
        {
            //Arrange
            var facadeMock = new Mock<IBorrowingProfileFacade>();
            facadeMock.Setup(x => x.GetDetailsAsync(_filter, It.IsAny<int>())).ReturnsAsync(new ProfileDetailsModel());
            var controller = new BorrowingProfileController(facadeMock.Object, _settingsMock.Object);

            //Act
            var result1 = await controller.PostByDealId(_filter,dealId);
            var result2 = await controller.PostByDealId(null,null);

            //Assert
            var okResult1 = result1.Should().BeOfType<OkObjectResult>().Subject;
             okResult1.Value.Should().BeAssignableTo<ProfileDetailsModel>();
            var okResult2 = result2.Should().BeOfType<OkObjectResult>().Subject;
            okResult2.Value.Should().BeNull();
        }

        [Fact]
        public async Task PostByProfileName_test()
        {
            //Arrange
            var facadeMock = new Mock<IBorrowingProfileFacade>();
            facadeMock.Setup(x => x.GetDealsDataAsync(_filter)).ReturnsAsync(new ProfileDealsModel());
            var controller = new BorrowingProfileController(facadeMock.Object, _settingsMock.Object);

            //Act
            var result1 = await controller.PostByProfileName(null);
            var result2 = await controller.PostByProfileName(_filter);

            //Assert
            var okResult = result2.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeAssignableTo<ProfileDealsModel>();
            var okResult1 = result1.Should().BeOfType<OkObjectResult>().Subject;
            okResult1.Value.Should().BeNull();
        }
    }
}
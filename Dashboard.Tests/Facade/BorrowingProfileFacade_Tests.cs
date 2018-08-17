using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dashboard.Common.Utilities;
using Dashboard.Facade.BorrowingProfile;
using Dashboard.Models;
using Dashboard.Models.BorrowingProfile.ClientModels;
using Dashboard.Models.BorrowingProfile.Models;
using Dashboard.Models.BorrowingProfile.Settings;
using Dashboard.Models.BorrowingProfile.ViewModels;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using Xunit;

namespace Dashboard.Tests.Facade
{
    public class BorrowingProfileFacadeTests
    {
        private readonly BorrowingProfileFilter _filter;
        private readonly Mock<IBorrowingProfileSettings> _settings;

        public BorrowingProfileFacadeTests()
        {
            AutoMapperConfiguration.Initialize();
            _filter = BorrowingProfileFilter.Default;
            _settings = new Mock<IBorrowingProfileSettings>();
            _settings.Setup(x => x.GetBorrowingProfileServiceUrl()).Returns("http://url");
        }

        [Fact]
        public async Task GetDealsData_test()
        {
            //Arrange
            _settings.Setup(x => x.GetDealsUrl()).Returns("http://dealsurl");
            var restClient = MockRestClient<List<BorrowingProfileDeal>>(
                JsonConvert.SerializeObject(new List<ProfileDealsModel>
                {
                    new ProfileDealsModel
                    {
                        ProfileDetailsTotal = new ProfileDealInfoModel(),
                        Deals = new List<ProfileDealInfoModel>()
                    }
                }));

            var facade = new BorrowingProfileFacade(_settings.Object,restClient);

            //Act
            var result = await facade.GetDealsDataAsync(_filter);

            //Assert
            result.Should().BeOfType<ProfileDealsModel>();
        }

        [Fact]
        public async Task GetTarget_test()
        {
            //Arrange
            _settings.Setup(x => x.GetTargetsUrl()).Returns("http://targeturl");
            var restClient = MockRestClient<BorrowingProfile>(
                JsonConvert.SerializeObject(
                    new BorrowingProfile
                    {
                        Profile="Test",
                        Province=100.00,
                        Consolidated=100.00,
                        OEFC= 100.00,
                        Deals=10,
                        PercentBorrowed=10
                    }
                ));

            var facade = new BorrowingProfileFacade(_settings.Object,restClient);

            //Act
            var result = await facade.GetTargetAsync(new FiscalYear(DateTime.Now));

            //Assert
            result.Should().BeOfType<ProfileDataModel>();
        }

        [Fact]
        public async Task GetProfileDetails_test()
        {
            //Arrange
            _settings.Setup(x => x.GetTargetsUrl()).Returns("http://targeturl");
            var restClient = MockRestClient<List<BorrowingProfile>>(
                JsonConvert.SerializeObject(new[]
                {
                    new ProfileDataModel
                    {
                        ProfileName="Test",
                        ProvinceAmount=100.00,
                        ConsolidatedAmount=100.00,
                        OefcAmount=100.00,
                        Deals=10,
                        PercentBorrowed=10
                    }
                }));

            var facade = new BorrowingProfileFacade(_settings.Object,restClient);

            //Act
            var result = await facade.GetProfilesDataAsync(_filter);

            //Assert
            result.Should().BeOfType<ProfileDataModel[]>();
        }

        [Fact]
        public async Task GetHedgesData_test()
        {
            //Arrange
            _settings.Setup(x => x.GetDealsUrl()).Returns("http://hedgesurl");
            var restClient = MockRestClient<List<BorrowingProfileHedges>>(
                JsonConvert.SerializeObject(new []
                {
                    new ProfileDataModel
                    {
                        ProfileName="Test",
                        ProvinceAmount=100.00,
                        ConsolidatedAmount=100.00,
                        OefcAmount=100.00,
                        Deals=10,
                        PercentBorrowed=10
                    }
                }));

            var facade = new BorrowingProfileFacade(_settings.Object,restClient);

            //Act
            var result = await facade.GetHedgesAsync(_filter);

            //Assert
            result.Should().BeOfType<ProfileDataModel[]>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetDetailsData_test(int dealId)
        {
            //Arrange
            _settings.Setup(x => x.GetBorrowingProfileServiceUrl()).Returns("http://detailsurl");
            var restClient = MockRestClient<List<BorrowingTradeDetails>>(
                JsonConvert.SerializeObject(
                    new List<BorrowingTradeDetails>
                    {
                        new BorrowingTradeDetails()
                    }
               ));

            var facade = new BorrowingProfileFacade(_settings.Object,restClient);

            //Act
            var result = await facade.GetDetailsAsync(_filter, dealId);

            //Assert
            result.Should().BeOfType<ProfileDetailsModel>();
        }

        private static IRestClient MockRestClient<T>(string json) 
            where T : new()
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            var response =  new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            response.Setup(_ => _.Data).Returns(data);

            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient.Setup(x => x.ExecuteTaskAsync<T>(It.IsAny<IRestRequest>())).ReturnsAsync(response.Object);
            return mockIRestClient.Object;
        }
    }
}
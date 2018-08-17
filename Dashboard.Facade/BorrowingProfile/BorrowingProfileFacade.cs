using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dashboard.Common.Utilities;
using Dashboard.Facade.BorrowingProfile.Utilities;
using Dashboard.Models.BorrowingProfile.ClientModels;
using Dashboard.Models.BorrowingProfile.Models;
using Dashboard.Models.BorrowingProfile.Settings;
using Dashboard.Models.BorrowingProfile.ViewModels;
using RestSharp;

namespace Dashboard.Facade.BorrowingProfile
{
    public class BorrowingProfileFacade : FacadeBase, IBorrowingProfileFacade
    {
        private readonly IRestClient _client;
        private readonly IBorrowingProfileSettings _settings;

        public BorrowingProfileFacade(IBorrowingProfileSettings settings, IRestClient client)
        {
           _settings = settings;
            _client = client;
            var url = _settings.GetBorrowingProfileServiceUrl();
            _client.BaseUrl = new Uri(url);
        }

        public async Task<ProfileDealsModel> GetDealsDataAsync(BorrowingProfileFilter filter)
        {
            var request = new RestRequest(_settings.GetDealsUrl(), Method.POST){RequestFormat = DataFormat.Json};
            request.AddBody(Mapper.Map<BorrowingProfileFilter, BorrowingProfileDealParameter>(filter));
            var response = await ExecuteAsync<List<BorrowingProfileDeal>>(_client,request);

            var dealVm = new ProfileDealsModel
            {
                Deals = Mapper.Map<List<BorrowingProfileDeal>, List<ProfileDealInfoModel>>(response)
            };
            dealVm.ProfileDetailsTotal = DealsCalculator.Sum(dealVm.Deals.ToArray());
            return dealVm;
        }

        public async Task<ProfileDetailsModel> GetDetailsAsync(BorrowingProfileFilter filter, int dealId)
        {
            var param = Mapper.Map<BorrowingProfileFilter, BorrowingTradeDetailsParameter>(filter);
            param.DealID = dealId;
            var details = new ProfileDetailsModel {ProfileName = filter.ProfileName};

            var request = new RestRequest(_settings.GetDetailsUrl() + param.DealID,
                Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(param);
            var response = await ExecuteAsync<List<BorrowingTradeDetails>>(_client,request);
            details.Trades = Mapper.Map<List<BorrowingTradeDetails>, TradeDetailsModel[]>(response);
            return details;
        }


        public async Task<ProfileDataModel[]> GetHedgesAsync(BorrowingProfileFilter filter)
        {
            var param = Mapper.Map<BorrowingProfileFilter, BorrowingProfileParameter>(filter);
            var request =
                new RestRequest(_settings.GetHedgesUrl(), Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(param);
            var response = await ExecuteAsync<List<BorrowingProfileHedges>>(_client,request);
            var mappedResult = Mapper.Map<List<BorrowingProfileHedges>, List<ProfileDataModel>>(response);
            return mappedResult.ToArray();
        }

        public async Task<ProfileDataModel> GetTargetAsync(FiscalYear filterFiscal)
        {
            var request =
                new RestRequest(_settings.GetTargetsUrl(), Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(filterFiscal.StartCalendarYear);
            var response = await ExecuteAsync<Models.BorrowingProfile.ClientModels.BorrowingProfile>(_client,request);
            return Mapper.Map<Models.BorrowingProfile.ClientModels.BorrowingProfile, ProfileDataModel>(response);
        }

        public async Task<ProfileDataModel[]> GetProfilesDataAsync(BorrowingProfileFilter filter)
        {
            var param = Mapper.Map<BorrowingProfileFilter, BorrowingProfileParameter>(filter);
            var request = new RestRequest(_settings.GetProfileUrl(),
                Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(param);
            var response = await ExecuteAsync<List<Models.BorrowingProfile.ClientModels.BorrowingProfile>>(_client,request);
            return Mapper.Map<List<Models.BorrowingProfile.ClientModels.BorrowingProfile>, ProfileDataModel[]>(response);
        }
    }
}
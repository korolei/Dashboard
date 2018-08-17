using System;
using AutoMapper;
using Dashboard.Common.Utilities;
using Dashboard.Models.BorrowingProfile.ClientModels;
using Dashboard.Models.BorrowingProfile.Models;
using Dashboard.Models.BorrowingProfile.ViewModels;

namespace Dashboard.Models.BorrowingProfile
{
    public class BorrowingProfileMappingProfile : Profile
    {
        public BorrowingProfileMappingProfile()
        {
            CreateMap<BorrowingProfileFilter, BorrowingProfileParameter>()
                .ForMember(x=> x.EndDate, opt=>{opt.MapFrom(o=> GetFiscalYear(o.StartFiscalYear).EndDate);})
                .ForMember(x => x.StartDate, opt => { opt.MapFrom(o => GetFiscalYear(o.StartFiscalYear).StartDate); })
                .ForMember(x => x.IncludeCPP, opt => { opt.MapFrom(o => o.IncludeCpp); })
                .ForMember(x => x.IncludeOIIC, opt => { opt.MapFrom(o => o.IncludeOiic); });

            CreateMap<BorrowingProfileFilter, BorrowingProfileDealParameter>()
                .ForMember(x => x.EndDate, opt => { opt.MapFrom(o => GetFiscalYear(o.StartFiscalYear).EndDate); })
                .ForMember(x => x.StartDate, opt => { opt.MapFrom(o => GetFiscalYear(o.StartFiscalYear).StartDate); })
                .ForMember(x => x.IncludeCpp, opt => { opt.MapFrom(o => o.IncludeCpp); })
                .ForMember(x => x.IncludeOiic, opt => { opt.MapFrom(o => o.IncludeOiic); })
                .ForMember(x => x.Profile, opt => { opt.MapFrom(o => o.ProfileName ?? string.Empty); });

            CreateMap<BorrowingProfileFilter, BorrowingTradeDetailsParameter>()
                .ForMember(x => x.EndDate, opt => { opt.MapFrom(o => GetFiscalYear(o.StartFiscalYear).EndDate); })
                .ForMember(x => x.StartDate, opt => { opt.MapFrom(o => GetFiscalYear(o.StartFiscalYear).StartDate); })
                .ForMember(x => x.EnableCpp, opt => { opt.MapFrom(o => o.IncludeCpp); })
                .ForMember(x => x.EnableOiic, opt => { opt.MapFrom(o => o.IncludeOiic); })
                .ForMember(x => x.DealID, opt => { opt.Ignore(); })
                .ForMember(x => x.ProfileName, opt => { opt.MapFrom(o => o.ProfileName ?? string.Empty); });

            CreateMap<BorrowingProfileHedges, ProfileDataModel>()
                .ForMember(x => x.ProfileName, opt => { opt.MapFrom(o => o.SubProgram); })
                .ForMember(x => x.ConsolidatedAmount, opt => { opt.MapFrom(o => o.Consolidated); })
                .ForMember(x => x.Deals, opt => { opt.MapFrom(o => ParseToInt(o.Deals)); })
                .ForMember(x => x.OefcAmount, opt => { opt.MapFrom(o => o.OEFC); })
                .ForMember(x => x.PercentBorrowed, opt => { opt.Ignore(); })
                .ForMember(x => x.ProvinceAmount, opt => { opt.MapFrom(o => o.Province); });

            CreateMap<ClientModels.BorrowingProfile, ProfileDataModel>()
                .ForMember(x => x.ProfileName, opt => { opt.MapFrom(o => o.Profile); })
                .ForMember(x => x.ConsolidatedAmount, opt => { opt.MapFrom(o => o.Consolidated); })
                .ForMember(x => x.OefcAmount, opt => { opt.MapFrom(o => o.OEFC); })
                .ForMember(x => x.ProvinceAmount, opt => { opt.MapFrom(o => o.Province); });

            CreateMap<BorrowingProfileDeal, ProfileDealInfoModel>()
                .ForMember(x => x.ConsolidatedAmount, opt => { opt.MapFrom(o => o.Consolidated); })
                .ForMember(x => x.OefcAmount, opt => { opt.MapFrom(o => o.OEFC); })
                .ForMember(x => x.TradesNum, opt => { opt.MapFrom(o => o.Trades); })
                .ForMember(x => x.ProfileName, opt => { opt.MapFrom(o => o.Profile); })
                .ForMember(x => x.ProvinceAmount, opt => { opt.MapFrom(o => o.Province); })
                .ForMember(x => x.Description, opt => { opt.MapFrom(o => o.Desc); });

            CreateMap<BorrowingTradeDetails, TradeDetailsModel>()
                .ForMember(x=> x.Description, opt =>
                    {
                        opt.MapFrom(o => GetTradeDescription(o.FaceVal, o.Coupon, o.MDate));
                    })
                .ForMember(x=> x.Series, opt=>{opt.MapFrom(o=>o.SN);})
                .ForMember(x=> x.MaturityDate, opt=>{opt.MapFrom(o=>o.MDate ?? DateTime.MinValue);})
                .ForMember(x=> x.TradeDate, opt=>{opt.MapFrom(o=>o.TDate ?? DateTime.MinValue);});

        }

        private static FiscalYear GetFiscalYear(int startFiscalYear)
        {
            return new FiscalYear(startFiscalYear);
        }

        private static string GetTradeDescription(string faceVal, double coupon, DateTime? mDate)
        {
            return mDate != null ? $"{faceVal} {coupon}% {mDate.Value:MMM-dd-yyyy}" : $"{faceVal} {coupon}%";
        }

        private static int ParseToInt(string s)
        {
            int i = int.TryParse(s, out i) ? i : 0;
            return i;
        }
    }
}
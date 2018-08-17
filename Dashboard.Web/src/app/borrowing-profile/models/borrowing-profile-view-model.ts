export interface BorrowingProfileViewModel {
    profiles: ProfileDataModel[];
    profilesTotals: ProfileDataModel[];
    hedges: ProfileDataModel[];
    target: ProfileDataModel;
    profileDetails: ProfileDetailsModel;
    profileDeals: ProfileDealsModel;
    filter: BorrowingProfileFilter;
    fiscals: FiscalYear[];
    elapsedTime:number;
}

export interface ProfileDealsModel{
    deals: ProfileDealInfoModel[];
    profileDetailsTotal: ProfileDealInfoModel;    
}

export interface ProfileDataModel {
    consolidatedAmount: number;
    deals: number;
    oefcAmount: number;
    percentBorrowed: number;
    profileName: string;
    provinceAmount: number;
}

export interface ProfileDealInfoModel {
    consolidatedAmount: number;
    dealId: number;
    description: string;
    market: string;
    oefcAmount: number;
    percentBorrowed: number;
    profileName: string;
    provinceAmount: number;
    tradeDate: Date;
    tradeNum: number;
}

export interface ProfileDetailsModel {
    profileName: string;
    trades: TradeDetailsModel[];
}

export interface TradeDetailsModel {
    coupon: number;
    faceVal: string;
    maturityDate: Date;
    series: string;
    tradeDate: Date;
    tradeNum: string;
    description: string;
}

export interface BorrowingProfileFilter {
    display: DisplayType;
    includeOiic: boolean;
    includeCpp: boolean;
    startFiscalYear: number;
    viewMode: DataViewMode;
    profileName: string;
}

export interface FiscalYear {
    startCalendarYear: number;
    endCalendarYear: number;
    startDate: string;
    endDate: string;
    displayYear: string;
}

export enum DisplayType {
    Grid = "Grid", 
    PieChart = "PieChart", 
}

export enum DataViewMode {
    Profile = "Profile", 
    Deals = "Deals", 
}

export function enumSelector(definition) {
    return Object.keys(definition)
      .map(key => ({ value: definition[key], title: key }));
  }
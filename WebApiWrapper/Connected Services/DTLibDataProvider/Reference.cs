﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace WebApiWrapper.Connected_Services.DTLibDataProvider {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="TimePeriodInterval", Namespace="http://schemas.datacontract.org/2004/07/Ofa.Moss.ITSService")]
    public enum TimePeriodInterval : int {
        
        [EnumMember()]
        IndividualUnit = 0,
        
        [EnumMember()]
        Day = 1,
        
        [EnumMember()]
        Week = 2,
        
        [EnumMember()]
        Month = 3,
        
        [EnumMember()]
        Quarter = 4,
        
        [EnumMember()]
        FiscalYear = 5,
        
        [EnumMember()]
        CalendarYear = 6,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DTLibDataProvider.DTLibProvider")]
    public interface DTLibProvider {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/IsBusinessDate", ReplyAction="http://tempuri.org/DTLibProvider/IsBusinessDateResponse")]
        bool IsBusinessDate(System.DateTime baseDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/IsBusinessDate", ReplyAction="http://tempuri.org/DTLibProvider/IsBusinessDateResponse")]
        System.Threading.Tasks.Task<bool> IsBusinessDateAsync(System.DateTime baseDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDate", ReplyAction="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateResponse")]
        System.DateTime ExtrapolateBusinessDate(System.DateTime baseDate, int numberOfBusinessDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDate", ReplyAction="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateResponse")]
        System.Threading.Tasks.Task<System.DateTime> ExtrapolateBusinessDateAsync(System.DateTime baseDate, int numberOfBusinessDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateInterval", ReplyAction="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateIntervalResponse")]
        System.DateTime ExtrapolateBusinessDateInterval(System.DateTime startDate, TimePeriodInterval interval, double numberOfIntervals);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateInterval", ReplyAction="http://tempuri.org/DTLibProvider/ExtrapolateBusinessDateIntervalResponse")]
        System.Threading.Tasks.Task<System.DateTime> ExtrapolateBusinessDateIntervalAsync(System.DateTime startDate, TimePeriodInterval interval, double numberOfIntervals);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/GetBusinessDates", ReplyAction="http://tempuri.org/DTLibProvider/GetBusinessDatesResponse")]
        System.DateTime[] GetBusinessDates(System.DateTime baseDate, int numberOfBusinessDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/GetBusinessDates", ReplyAction="http://tempuri.org/DTLibProvider/GetBusinessDatesResponse")]
        System.Threading.Tasks.Task<System.DateTime[]> GetBusinessDatesAsync(System.DateTime baseDate, int numberOfBusinessDays);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/GetBusinessDatesDifference", ReplyAction="http://tempuri.org/DTLibProvider/GetBusinessDatesDifferenceResponse")]
        int GetBusinessDatesDifference(System.DateTime dateFrom, System.DateTime dateTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DTLibProvider/GetBusinessDatesDifference", ReplyAction="http://tempuri.org/DTLibProvider/GetBusinessDatesDifferenceResponse")]
        System.Threading.Tasks.Task<int> GetBusinessDatesDifferenceAsync(System.DateTime dateFrom, System.DateTime dateTo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DTLibProviderChannel : DTLibProvider, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DTLibProviderClient : System.ServiceModel.ClientBase<DTLibProvider>, DTLibProvider {
        
        public DTLibProviderClient() {
        }
        
        public DTLibProviderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DTLibProviderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DTLibProviderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DTLibProviderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsBusinessDate(System.DateTime baseDate) {
            return base.Channel.IsBusinessDate(baseDate);
        }
        
        public System.Threading.Tasks.Task<bool> IsBusinessDateAsync(System.DateTime baseDate) {
            return base.Channel.IsBusinessDateAsync(baseDate);
        }
        
        public System.DateTime ExtrapolateBusinessDate(System.DateTime baseDate, int numberOfBusinessDays) {
            return base.Channel.ExtrapolateBusinessDate(baseDate, numberOfBusinessDays);
        }
        
        public System.Threading.Tasks.Task<System.DateTime> ExtrapolateBusinessDateAsync(System.DateTime baseDate, int numberOfBusinessDays) {
            return base.Channel.ExtrapolateBusinessDateAsync(baseDate, numberOfBusinessDays);
        }
        
        public System.DateTime ExtrapolateBusinessDateInterval(System.DateTime startDate, TimePeriodInterval interval, double numberOfIntervals) {
            return base.Channel.ExtrapolateBusinessDateInterval(startDate, interval, numberOfIntervals);
        }
        
        public System.Threading.Tasks.Task<System.DateTime> ExtrapolateBusinessDateIntervalAsync(System.DateTime startDate, TimePeriodInterval interval, double numberOfIntervals) {
            return base.Channel.ExtrapolateBusinessDateIntervalAsync(startDate, interval, numberOfIntervals);
        }
        
        public System.DateTime[] GetBusinessDates(System.DateTime baseDate, int numberOfBusinessDays) {
            return base.Channel.GetBusinessDates(baseDate, numberOfBusinessDays);
        }
        
        public System.Threading.Tasks.Task<System.DateTime[]> GetBusinessDatesAsync(System.DateTime baseDate, int numberOfBusinessDays) {
            return base.Channel.GetBusinessDatesAsync(baseDate, numberOfBusinessDays);
        }
        
        public int GetBusinessDatesDifference(System.DateTime dateFrom, System.DateTime dateTo) {
            return base.Channel.GetBusinessDatesDifference(dateFrom, dateTo);
        }
        
        public System.Threading.Tasks.Task<int> GetBusinessDatesDifferenceAsync(System.DateTime dateFrom, System.DateTime dateTo) {
            return base.Channel.GetBusinessDatesDifferenceAsync(dateFrom, dateTo);
        }
    }
}

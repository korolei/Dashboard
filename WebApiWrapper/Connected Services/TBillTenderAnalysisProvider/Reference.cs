﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace WebApiWrapper.Connected_Services.TBillTenderAnalysisProvider {
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="DealerRankingsParameter", Namespace="http://schemas.datacontract.org/2004/07/Ofa.Moss.ITSService")]
    [Serializable()]
    public partial class DealerRankingsParameter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [NonSerialized()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [OptionalField()]
        private System.DateTime EndDateField;
        
        [OptionalField()]
        private System.DateTime StartDateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [DataMember()]
        public System.DateTime EndDate {
            get {
                return this.EndDateField;
            }
            set {
                if ((this.EndDateField.Equals(value) != true)) {
                    this.EndDateField = value;
                    this.RaisePropertyChanged("EndDate");
                }
            }
        }
        
        [DataMember()]
        public System.DateTime StartDate {
            get {
                return this.StartDateField;
            }
            set {
                if ((this.StartDateField.Equals(value) != true)) {
                    this.StartDateField = value;
                    this.RaisePropertyChanged("StartDate");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="DealerRanking", Namespace="http://schemas.datacontract.org/2004/07/Ofa.Moss.ITSService")]
    [Serializable()]
    public partial class DealerRanking : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [NonSerialized()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [OptionalField()]
        private double AmtBidField;
        
        [OptionalField()]
        private double AmtWonField;
        
        [OptionalField()]
        private string CPField;
        
        [OptionalField()]
        private string CPShortNameField;
        
        [OptionalField()]
        private int NumBidField;
        
        [OptionalField()]
        private int NumWonField;
        
        [OptionalField()]
        private double RankValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [DataMember()]
        public double AmtBid {
            get {
                return this.AmtBidField;
            }
            set {
                if ((this.AmtBidField.Equals(value) != true)) {
                    this.AmtBidField = value;
                    this.RaisePropertyChanged("AmtBid");
                }
            }
        }
        
        [DataMember()]
        public double AmtWon {
            get {
                return this.AmtWonField;
            }
            set {
                if ((this.AmtWonField.Equals(value) != true)) {
                    this.AmtWonField = value;
                    this.RaisePropertyChanged("AmtWon");
                }
            }
        }
        
        [DataMember()]
        public string CP {
            get {
                return this.CPField;
            }
            set {
                if ((object.ReferenceEquals(this.CPField, value) != true)) {
                    this.CPField = value;
                    this.RaisePropertyChanged("CP");
                }
            }
        }
        
        [DataMember()]
        public string CPShortName {
            get {
                return this.CPShortNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CPShortNameField, value) != true)) {
                    this.CPShortNameField = value;
                    this.RaisePropertyChanged("CPShortName");
                }
            }
        }
        
        [DataMember()]
        public int NumBid {
            get {
                return this.NumBidField;
            }
            set {
                if ((this.NumBidField.Equals(value) != true)) {
                    this.NumBidField = value;
                    this.RaisePropertyChanged("NumBid");
                }
            }
        }
        
        [DataMember()]
        public int NumWon {
            get {
                return this.NumWonField;
            }
            set {
                if ((this.NumWonField.Equals(value) != true)) {
                    this.NumWonField = value;
                    this.RaisePropertyChanged("NumWon");
                }
            }
        }
        
        [DataMember()]
        public double RankValue {
            get {
                return this.RankValueField;
            }
            set {
                if ((this.RankValueField.Equals(value) != true)) {
                    this.RankValueField = value;
                    this.RaisePropertyChanged("RankValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TBillTenderAnalysisProvider.ITBillTenderAnalysisProvider")]
    public interface ITBillTenderAnalysisProvider {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITBillTenderAnalysisProvider/GetDealerRankings", ReplyAction="http://tempuri.org/ITBillTenderAnalysisProvider/GetDealerRankingsResponse")]
        DealerRanking[] GetDealerRankings(DealerRankingsParameter parameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITBillTenderAnalysisProvider/GetDealerRankings", ReplyAction="http://tempuri.org/ITBillTenderAnalysisProvider/GetDealerRankingsResponse")]
        System.Threading.Tasks.Task<DealerRanking[]> GetDealerRankingsAsync(DealerRankingsParameter parameters);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITBillTenderAnalysisProviderChannel : ITBillTenderAnalysisProvider, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TBillTenderAnalysisProviderClient : System.ServiceModel.ClientBase<ITBillTenderAnalysisProvider>, ITBillTenderAnalysisProvider {
        
        public TBillTenderAnalysisProviderClient() {
        }
        
        public TBillTenderAnalysisProviderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TBillTenderAnalysisProviderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TBillTenderAnalysisProviderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TBillTenderAnalysisProviderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public DealerRanking[] GetDealerRankings(DealerRankingsParameter parameters) {
            return base.Channel.GetDealerRankings(parameters);
        }
        
        public System.Threading.Tasks.Task<DealerRanking[]> GetDealerRankingsAsync(DealerRankingsParameter parameters) {
            return base.Channel.GetDealerRankingsAsync(parameters);
        }
    }
}

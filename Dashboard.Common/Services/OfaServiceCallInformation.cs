using System;

namespace Dashboard.Common.Services
{
    public class OfaServiceCallInformation
    {
        public bool IsAnonymous { get; set; }
        public string RemotePrincipal { get; set; }
        public string SessionId { get; set; }
        public Uri RequestedUri { get; set; }
        public Uri RemoteUri { get; set; }
        public string RemoteIP { get; set; }
        public int RemotePort { get; set; }
        public string RequestedContract { get; set; }
        public string RequestedAction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public object[] Arguments { get; set; }
        public DateTime CallTime { get; set; }
    }
}
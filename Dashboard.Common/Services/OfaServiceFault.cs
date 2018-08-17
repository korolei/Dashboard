using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Services
{
    [DataContract]
    [Serializable]
	public class OfaServiceFault
	{
		#region Properties

		[DataMember]
		public Guid CorrelationId { get; set; }

		[DataMember]
		public string Message { get; set; }

		#endregion
	}
}

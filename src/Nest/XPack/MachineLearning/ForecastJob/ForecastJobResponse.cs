using System.Runtime.Serialization;

namespace Nest
{
	public class ForecastJobResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="forecast_id")]
		public string ForecastId { get; internal set; }
	}
}

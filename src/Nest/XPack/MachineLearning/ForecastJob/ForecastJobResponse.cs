using System.Runtime.Serialization;

namespace Nest
{
	public interface IForecastJobResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="forecast_id")]
		string ForecastId { get; }
	}

	public class ForecastJobResponse : AcknowledgedResponseBase, IForecastJobResponse
	{
		public string ForecastId { get; internal set; }
	}
}

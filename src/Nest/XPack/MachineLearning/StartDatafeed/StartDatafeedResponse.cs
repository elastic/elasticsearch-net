using Newtonsoft.Json;

namespace Nest
{
	public interface IStartDatafeedResponse : IResponse
	{
		bool Started { get; }
	}

	public class StartDatafeedResponse : ResponseBase, IStartDatafeedResponse
	{
		[JsonProperty("started")]
		public bool Started { get; internal set; }
	}
}

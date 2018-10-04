using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IStartRollupJobResponse : IResponse
	{
		[JsonProperty("started")]
		bool Started { get; set; }
	}

	public class StartRollupJobResponse : ResponseBase, IStartRollupJobResponse
	{
		public bool Started { get; set; }
	}
}

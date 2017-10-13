using Newtonsoft.Json;

namespace Nest
{
	public interface IOpenJobResponse : IResponse
	{
		[JsonProperty("opened")]
		bool Opened { get; }
	}

	public class OpenJobResponse : ResponseBase, IOpenJobResponse
	{
		public bool Opened { get; internal set; }
	}
}

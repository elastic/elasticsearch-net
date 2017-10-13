using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetDatafeedsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("datafeeds")]
		IReadOnlyCollection<DatafeedConfig> Datafeeds { get; }
	}

	public class GetDatafeedsResponse : ResponseBase, IGetDatafeedsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<DatafeedConfig> Datafeeds { get; internal set; } = EmptyReadOnly<DatafeedConfig>.Collection;
	}
}

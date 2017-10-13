using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetDatafeedStatsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("datafeeds")]
		IReadOnlyCollection<DatafeedStats> Datafeeds { get; }
	}

	public class GetDatafeedStatsResponse : ResponseBase, IGetDatafeedStatsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<DatafeedStats> Datafeeds { get; internal set; } = EmptyReadOnly<DatafeedStats>.Collection;
	}
}

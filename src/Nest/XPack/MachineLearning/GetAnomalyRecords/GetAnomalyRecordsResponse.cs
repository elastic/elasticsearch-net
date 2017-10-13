using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAnomalyRecordsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("records")]
		IReadOnlyCollection<AnomalyRecord> Records { get; }
	}

	public class GetAnomalyRecordsResponse : ResponseBase, IGetAnomalyRecordsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<AnomalyRecord> Records { get; internal set; } = EmptyReadOnly<AnomalyRecord>.Collection;
	}
}

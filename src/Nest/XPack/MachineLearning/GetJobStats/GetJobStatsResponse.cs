using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetJobStatsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("jobs")]
		IReadOnlyCollection<JobStats> Jobs { get; }
	}

	public class GetJobStatsResponse : ResponseBase, IGetJobStatsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<JobStats> Jobs { get; internal set; } = EmptyReadOnly<JobStats>.Collection;
	}
}

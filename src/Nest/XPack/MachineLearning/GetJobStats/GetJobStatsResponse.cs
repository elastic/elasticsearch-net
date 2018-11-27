using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetJobStatsResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="jobs")]
		IReadOnlyCollection<JobStats> Jobs { get; }
	}

	public class GetJobStatsResponse : ResponseBase, IGetJobStatsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<JobStats> Jobs { get; internal set; } = EmptyReadOnly<JobStats>.Collection;
	}
}

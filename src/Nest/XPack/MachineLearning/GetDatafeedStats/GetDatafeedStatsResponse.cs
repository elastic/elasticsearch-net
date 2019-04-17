using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetDatafeedStatsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="datafeeds")]
		public IReadOnlyCollection<DatafeedStats> Datafeeds { get; internal set; } = EmptyReadOnly<DatafeedStats>.Collection;
	}
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetAnomalyRecordsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="records")]
		public IReadOnlyCollection<AnomalyRecord> Records { get; internal set; } = EmptyReadOnly<AnomalyRecord>.Collection;
	}
}

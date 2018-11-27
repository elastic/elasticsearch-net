using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetAnomalyRecordsResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="records")]
		IReadOnlyCollection<AnomalyRecord> Records { get; }
	}

	public class GetAnomalyRecordsResponse : ResponseBase, IGetAnomalyRecordsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<AnomalyRecord> Records { get; internal set; } = EmptyReadOnly<AnomalyRecord>.Collection;
	}
}

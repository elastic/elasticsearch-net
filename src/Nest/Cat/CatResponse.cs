using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class CatResponse<TCatRecord> : ResponseBase
		where TCatRecord : ICatRecord
	{
		[IgnoreDataMember]
		public IReadOnlyCollection<TCatRecord> Records { get; internal set; } = EmptyReadOnly<TCatRecord>.Collection;
	}
}

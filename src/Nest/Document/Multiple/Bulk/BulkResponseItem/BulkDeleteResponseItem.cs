using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(ConcreteBulkIndexResponseItemFormatter<BulkDeleteResponseItem>))]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "delete";
	}
}

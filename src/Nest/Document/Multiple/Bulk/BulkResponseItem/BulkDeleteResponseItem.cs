using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(ConcreteBulkIndexResponseItemFormatter<BulkDeleteResponseItem>))]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "delete";
	}
}

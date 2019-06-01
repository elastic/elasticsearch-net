using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(ConcreteBulkIndexResponseItemFormatter<BulkCreateResponseItem>))]
	public class BulkCreateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "create";
	}
}

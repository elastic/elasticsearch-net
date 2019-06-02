using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(ConcreteBulkIndexResponseItemFormatter<BulkUpdateResponseItem>))]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "update";
	}
}

using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(BulkResponseItemJsonConverter))]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }
	}
}

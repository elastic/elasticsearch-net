using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "update";
	}
}

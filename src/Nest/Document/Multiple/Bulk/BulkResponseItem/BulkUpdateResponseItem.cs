using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class BulkUpdateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "update";
	}
}

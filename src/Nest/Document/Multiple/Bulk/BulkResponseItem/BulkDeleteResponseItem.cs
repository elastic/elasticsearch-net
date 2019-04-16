using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "delete";
	}
}

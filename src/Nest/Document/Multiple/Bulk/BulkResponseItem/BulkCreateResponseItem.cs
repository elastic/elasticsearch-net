using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class BulkCreateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "create";
	}
}

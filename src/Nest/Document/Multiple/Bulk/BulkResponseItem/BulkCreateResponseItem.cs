using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class BulkCreateResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "create";
	}
}

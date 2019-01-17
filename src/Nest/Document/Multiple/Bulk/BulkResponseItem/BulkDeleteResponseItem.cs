using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class BulkDeleteResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; } = "delete";
	}
}

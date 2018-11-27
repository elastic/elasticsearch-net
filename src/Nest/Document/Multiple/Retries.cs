using System.Runtime.Serialization;

namespace Nest
{
	public class Retries
	{
		[DataMember(Name ="bulk")]
		public long Bulk { get; internal set; }

		[DataMember(Name ="search")]
		public long Search { get; internal set; }
	}
}

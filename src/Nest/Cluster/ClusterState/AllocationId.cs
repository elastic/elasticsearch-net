using System.Runtime.Serialization;

namespace Nest
{
	public class AllocationId
	{
		[DataMember(Name ="id")]
		public string Id { get; internal set; }
	}
}

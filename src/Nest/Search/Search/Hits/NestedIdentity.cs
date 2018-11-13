using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	public class NestedIdentity
	{
		[DataMember(Name ="field")]
		public Field Field { get; internal set; }

		[DataMember(Name ="_nested")]
		public NestedIdentity Nested { get; internal set; }

		[DataMember(Name ="offset")]
		public int Offset { get; internal set; }
	}
}

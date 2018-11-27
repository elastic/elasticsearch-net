using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CompactNodeInfo
	{
		[DataMember(Name ="name")]
		public string Name { get; internal set; }
	}
}

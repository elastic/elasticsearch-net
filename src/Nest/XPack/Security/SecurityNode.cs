using System.Runtime.Serialization;

namespace Nest
{
	public class SecurityNode
	{
		[DataMember(Name ="name")]
		public string Name { get; set; }
	}
}

using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum ClusterStatus
	{
		[EnumMember(Value = "green")]
		Green,

		[EnumMember(Value = "yellow")]
		Yellow,

		[EnumMember(Value = "red")]
		Red
	}
}

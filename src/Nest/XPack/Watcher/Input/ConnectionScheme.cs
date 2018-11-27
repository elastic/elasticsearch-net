using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum ConnectionScheme
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "https")]
		Https
	}
}

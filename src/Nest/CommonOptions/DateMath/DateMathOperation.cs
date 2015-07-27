using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DateMathOperation
	{
		[EnumMember(Value = "+")]
		Add,
		[EnumMember(Value = "-")]
		Subtract
	}
}
using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum Operator
	{
		[EnumMember(Value = "and")]
		And,

		[EnumMember(Value = "or")]
		Or
	}
}

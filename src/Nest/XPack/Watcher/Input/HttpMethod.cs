using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum HttpInputMethod
	{
		[EnumMember(Value = "head")]
		Head,

		[EnumMember(Value = "get")]
		Get,

		[EnumMember(Value = "post")]
		Post,

		[EnumMember(Value = "put")]
		Put,

		[EnumMember(Value = "delete")]
		Delete
	}
}

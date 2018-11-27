using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum FielddataLoading
	{
		[EnumMember(Value = "eager")]
		Eager,

		[EnumMember(Value = "eager_global_ordinals")]
		EagerGlobalOrdinals
	}
}

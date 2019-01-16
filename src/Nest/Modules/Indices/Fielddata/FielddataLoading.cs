using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum FielddataLoading
	{
		[EnumMember(Value = "eager")]
		Eager,

		[EnumMember(Value = "eager_global_ordinals")]
		EagerGlobalOrdinals
	}
}

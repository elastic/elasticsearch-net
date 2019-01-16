using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum RangeRelation
	{
		[EnumMember(Value = "within")] Within,
		[EnumMember(Value = "contains")] Contains,
		[EnumMember(Value = "intersects")] Intersects
	}
}

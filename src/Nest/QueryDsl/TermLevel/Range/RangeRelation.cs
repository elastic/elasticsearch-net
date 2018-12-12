using System.Runtime.Serialization;


namespace Nest
{

	public enum RangeRelation
	{
		[EnumMember(Value = "within")] Within,
		[EnumMember(Value = "contains")] Contains,
		[EnumMember(Value = "intersects")] Intersects
	}
}

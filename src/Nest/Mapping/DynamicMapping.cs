using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum DynamicMapping
	{
		/// <summary>
		/// If new unmapped fields are passed, the whole document will not be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}
}

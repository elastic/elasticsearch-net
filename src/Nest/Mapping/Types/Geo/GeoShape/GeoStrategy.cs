using System.Runtime.Serialization;


namespace Nest
{

	public enum GeoStrategy
	{
		/// <summary>
		/// supports all shape types
		/// </summary>
		[EnumMember(Value = "recursive")]
		Recursive,

		/// <summary>
		/// supports point types only
		/// </summary>
		[EnumMember(Value = "term")]
		Term
	}
}

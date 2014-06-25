using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Controls how elasticsearch handles dynamic mapping changes when a new document present new fields
	/// </summary>
	public enum DynamicMappingOption
	{
		/// <summary>
		/// Default value, allows unmapped fields to be cause a mapping update 
		/// </summary>
		[EnumMember(Value = "allow")]
		Allow,
		/// <summary>
		/// New unmapped fields will be silently ignored
		/// </summary>
		[EnumMember(Value = "ignore")]
		Ignore,
		/// <summary>
		/// If new unmapped fields are passed, the whole document WON'T be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}
}

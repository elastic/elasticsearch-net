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
		allow,
		/// <summary>
		/// New unmapped fields will be silently ignored
		/// </summary>
		ignore,
		/// <summary>
		/// If new unmapped fields are passed, the whole document WON'T be added/updated
		/// </summary>
		strict
	}
}

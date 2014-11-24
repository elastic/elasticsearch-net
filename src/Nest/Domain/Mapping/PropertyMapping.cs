namespace Nest
{
	/// <summary>
	/// This class allows you to map aspects of a Type's property
	/// that influences how NEST treats it. 
	/// </summary>
	public interface IPropertyMapping
	{
		/// <summary>
		/// Override the json property name of a type
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using MapFromAttributes()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		bool OptOut { get; set; } //TODO Rename to Ignore when we get rid of IElasticPropertyAttribute
	}

	/// <summary>
	/// This class allows you to map aspects of a Type's property
	/// that influences how NEST treats it. 
	/// </summary>
	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

		/// <summary>
		/// Override the json property name of a type
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using MapFromAttributes()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		public bool Ignore { get; set; }

		bool IPropertyMapping.OptOut
		{
			get { return this.Ignore; }
			set { this.Ignore = value; }
		}
		
		public static implicit operator PropertyMapping(string propertyName)
		{
			return propertyName == null ? null : new PropertyMapping() { Name = propertyName };
		}
	}
}
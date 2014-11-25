using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Nest.Resolvers;

namespace Nest
{
	
	public class PropertyMappingDescriptor<TDocument>
	{
		
		private readonly IList<KeyValuePair<Expression<Func<TDocument, object>>, PropertyMapping>> _mappings = new List<KeyValuePair<Expression<Func<TDocument, object>>, PropertyMapping>>();
		
		internal IList<KeyValuePair<Expression<Func<TDocument, object>>, PropertyMapping>> Mappings { get { return _mappings; } }

		public PropertyMappingDescriptor<TDocument> Rename(Expression<Func<TDocument, object>> property, string propertyName)
		{
			property.ThrowIfNull("property");
			propertyName.ThrowIfNullOrEmpty("propertyName");
			this._mappings.Add(new KeyValuePair<Expression<Func<TDocument, object>>, PropertyMapping>(property, propertyName));
			return this;
		}

		public PropertyMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property)
		{
			property.ThrowIfNull("property");
			this._mappings.Add(new KeyValuePair<Expression<Func<TDocument, object>>, PropertyMapping>(property, PropertyMapping.Ignored));
			return this;
		}
	}




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
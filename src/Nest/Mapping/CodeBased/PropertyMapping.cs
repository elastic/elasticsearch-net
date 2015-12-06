using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Nest.CommonAbstractions.ConnectionSettings;
using Nest.Resolvers;

namespace Nest
{
	
	public class PropertyMappingDescriptor<TDocument> : DescriptorBase<PropertyMappingDescriptor<TDocument>, IDescriptor>
		where TDocument : class
	{
		internal IList<IClrTypePropertyMapping<TDocument>> Mappings { get; } = new List<IClrTypePropertyMapping<TDocument>>();

		public PropertyMappingDescriptor<TDocument> Rename(Expression<Func<TDocument, object>> property, string field)
		{
			property.ThrowIfNull(nameof(property));
			field.ThrowIfNullOrEmpty(nameof(field));
			this.Mappings.Add(new RenamePropertyMapping<TDocument>(property, field));
			return this;
		}

		public PropertyMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property)
		{
			property.ThrowIfNull(nameof(property));
			this.Mappings.Add(new IgnorePropertyMapping<TDocument>(property));
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
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		bool Ignore { get; set; }
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
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		public bool Ignore { get; set; }

		bool IPropertyMapping.Ignore
		{
			get { return this.Ignore; }
			set { this.Ignore = value; }
		}
		
		public static implicit operator PropertyMapping(string field)
		{
			return field == null ? null : new PropertyMapping() { Name = field };
		}
	}
}
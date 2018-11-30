using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A mapping for a property type to a document field in Elasticsearch
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(PropertyFormatter))]
	public interface IProperty : IFieldMapping
	{
		/// <summary>
		/// Local property metadata that will not be stored in Elasticsearch with the mappings
		/// </summary>
		[IgnoreDataMember]
		IDictionary<string, object> LocalMetadata { get; set; }

		/// <summary>
		/// The name of the property
		/// </summary>
		[DataMember(Name = "name")]
		PropertyName Name { get; set; }

		/// <summary>
		/// The datatype of the property
		/// </summary>
		[DataMember(Name ="type")]
		string Type { get; set; }
	}

	/// <summary>
	/// A mapping for a property from a CLR type
	/// </summary>
	public interface IPropertyWithClrOrigin
	{
		/// <summary>
		/// The CLR property to which the mapping relates
		/// </summary>
		PropertyInfo ClrOrigin { get; set; }
	}

	/// <inheritdoc cref="IProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		protected PropertyBase(FieldType type) => ((IProperty)this).Type = type.GetStringValue();

		/// <inheritdoc />
		public IDictionary<string, object> LocalMetadata { get; set; }

		/// <inheritdoc />
		public PropertyName Name { get; set; }

		protected string DebugDisplay => $"Type: {((IProperty)this).Type ?? "<empty>"}, Name: {Name.DebugDisplay} ";

		/// <summary>
		/// Override for the property type, used for custom mappings
		/// </summary>
		protected string TypeOverride { get; set; }

		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }

		string IProperty.Type
		{
			get => TypeOverride;
			set => TypeOverride = value;
		}
	}
}

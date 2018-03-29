using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IProperty : IFieldMapping
	{
		/// <summary>
		/// The name of the property
		/// </summary>
		PropertyName Name { get; set; }

		/// <summary>
		/// The datatype of the property
		/// </summary>
		[JsonProperty("type")]
		string Type { get; set; }

		/// <summary>
		/// Local property metadata that will not be stored in Elasticsearch with the mappings
		/// </summary>
		[JsonIgnore]
		IDictionary<string, object> LocalMetadata { get; set; }
	}

	public interface IPropertyWithClrOrigin
	{
		PropertyInfo ClrOrigin { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		private string _type;
		protected string TypeOverride { get => _type; set => _type = value; }

		string IProperty.Type { get => _type; set => _type = value; }

		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }

		protected PropertyBase(FieldType type)
		{
			((IProperty)this).Type = type.GetStringValue();
		}

		protected string DebugDisplay => $"Type: {((IProperty)this).Type ?? "<empty>"}, Name: {Name.DebugDisplay} ";

		/// <inheritdoc />
		public PropertyName Name { get; set; }

		/// <inheritdoc />
		public IDictionary<string, object> LocalMetadata { get; set; }
	}
}

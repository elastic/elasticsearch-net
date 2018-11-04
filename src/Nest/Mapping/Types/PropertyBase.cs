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
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
		/// </summary>
		[JsonIgnore]
		IDictionary<string, object> LocalMetadata { get; set; }

		PropertyName Name { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

	public interface IPropertyWithClrOrigin
	{
		PropertyInfo ClrOrigin { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		[Obsolete("Please use overload taking FieldType")]
		protected PropertyBase(TypeName typeName) => Type = typeName;

#pragma warning disable 618
		protected PropertyBase(FieldType type) : this(type.GetStringValue()) { }
#pragma warning restore 618

		/// <summary>
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
		/// </summary>
		public IDictionary<string, object> LocalMetadata { get; set; }

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }

		protected string DebugDisplay => $"Type: {Type?.DebugDisplay}, Name: {Name?.DebugDisplay} ";
		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }
	}
}

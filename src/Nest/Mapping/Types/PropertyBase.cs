using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IProperty : IFieldMapping
	{
		PropertyName Name { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		/// <summary>
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
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
		protected PropertyBase(TypeName typeName)
		{
			Type = typeName;
		}

		protected string DebugDisplay => $"Type: {Type.DebugDisplay}, Name: {Name.DebugDisplay} ";

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }
		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }

		/// <summary>
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
		/// </summary>
		public IDictionary<string, object> LocalMetadata { get; set; }
	}
}

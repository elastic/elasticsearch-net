using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IProperty : IFieldMapping, IPropertyWithLocalMetadata
	{
		PropertyName Name { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }
    }

    public interface IPropertyWithClrOrigin
	{
		PropertyInfo ClrOrigin { get; set; }
	}

    public interface IPropertyWithLocalMetadata {
        [JsonIgnore]
        IDictionary<string, object> LocalMetadata { get; set; }
    }

    public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		protected PropertyBase(TypeName typeName)
		{
			Type = typeName;
		}

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }
		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }
        IDictionary<string, object> IPropertyWithLocalMetadata.LocalMetadata { get; set; }
    }
}

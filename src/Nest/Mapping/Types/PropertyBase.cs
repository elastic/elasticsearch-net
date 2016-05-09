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

		[JsonProperty("index_name")]
		string IndexName { get; set; }
	}

	public interface IPropertyWithClrOrigin
	{
		PropertyInfo ClrOrigin { get; set; }
	}

	public abstract class PropertyBase : IProperty, IPropertyWithClrOrigin
	{
		protected PropertyBase(TypeName typeName)
		{
			Type = typeName;
		}

		public PropertyName Name { get; set; }
		public virtual TypeName Type { get; set; }
		public string IndexName { get; set; }
		PropertyInfo IPropertyWithClrOrigin.ClrOrigin { get; set; }
	}
}

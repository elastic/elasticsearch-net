using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>Determines how a POCO property maps to the property on a JSON object when serialized</summary>
	public interface IPropertyMapping
	{
		/// <summary> Override the property name serialized to JSON for this property</summary>
		string Name { get; set; }
		/// <summary>
		/// Ignore this property completely
		/// <para>- When mapping automatically using <see cref="TypeMappingDescriptor{T}.AutoMap{TDocument}"/></para>
		/// <para>- When Indexing this type do not serialize this property and its value</para>
		/// </summary>
		bool Ignore { get; set; }
	}

	/// <inheritdoc/>
	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public bool Ignore { get; set; }
	}

	/// <summary>
	/// Provides mappings for POCO properties
	/// </summary>
	public interface IPropertyMappingProvider
	{
		/// <summary>
		/// Creates an <see cref="IPropertyMapping"/> for a <see cref="MemberInfo"/>
		/// </summary>
		IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
	}

	/// <inheritdoc/>
	public class PropertyMappingProvider : IPropertyMappingProvider
	{
		protected readonly ConcurrentDictionary<string, IPropertyMapping> Properties = new ConcurrentDictionary<string, IPropertyMapping>();

		/// <inheritdoc/>
		public virtual IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out var mapping)) return mapping;

			mapping = PropertyMappingFromAttributes(memberInfo);
			this.Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		private static IPropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
		{
			var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>(true);
			var dataMemberProperty = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);
			var propertyName = memberInfo.GetCustomAttribute<PropertyNameAttribute>(true);
			var ignore = memberInfo.GetCustomAttribute<IgnoreAttribute>(true);
			if (jsonProperty == null && ignore == null && propertyName == null && dataMemberProperty == null) return null;
			return new PropertyMapping {Name = propertyName?.Name ?? jsonProperty?.PropertyName ?? dataMemberProperty?.Name, Ignore = ignore != null};
		}
	}


}

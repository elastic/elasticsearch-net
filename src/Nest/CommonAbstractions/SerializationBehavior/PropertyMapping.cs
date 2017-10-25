using System.Collections.Concurrent;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary> This class allows a serializer to report back on a properties behavior </summary>
	public interface IPropertyMapping
	{
		/// <summary> Override the json property name of a type </summary>
		string Name { get; set; }
		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		bool Ignore { get; set; }
	}

	/// <summary> This class allows a serializer to report back on a properties behavior </summary>
	public class PropertyMapping : IPropertyMapping
	{
		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

		/// <summary> Override the json property name of a type </summary>
		public string Name { get; set; }

		/// <summary>
		/// Ignore this property completely
		/// <pre>- When mapping automatically using AutoMap()</pre>
		/// <pre>- When Indexing this type do not serialize whatever this value hold</pre>
		/// </summary>
		public bool Ignore { get; set; }
	}

	public interface IPropertyMappingProvider
	{
		IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
	}

	public class PropertyMappingProvider : IPropertyMappingProvider
	{
		private readonly IConnectionSettingsValues _settings;

		protected readonly ConcurrentDictionary<string, IPropertyMapping> Properties = new ConcurrentDictionary<string, IPropertyMapping>();

		public PropertyMappingProvider(IConnectionSettingsValues settings)
		{
			this._settings = settings;
		}
		public virtual IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			IPropertyMapping mapping;
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out mapping))
				return mapping;
			mapping =  PropertyMappingFromAttributes(memberInfo);
			this.Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		private static IPropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
		{
			var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>(true);
			var ignoreProperty = memberInfo.GetCustomAttribute<JsonIgnoreAttribute>(true);
			if (jsonProperty == null && ignoreProperty == null) return null;
			return new PropertyMapping {Name = jsonProperty?.PropertyName, Ignore = ignoreProperty != null};
		}
	}


}

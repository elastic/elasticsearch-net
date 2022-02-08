//// Licensed to Elasticsearch B.V under one or more agreements.
//// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
//// See the LICENSE file in the project root for more information

//using System.Collections.Concurrent;
//using System.Reflection;
//using System.Runtime.Serialization;

//namespace Elastic.Clients.Elasticsearch
//{
//	/// <summary>Determines how a POCO property maps to the property on a JSON object when serialized</summary>
//	public interface IPropertyMapping
//	{
//		/// <summary>
//		/// Ignores this property.
//		/// <para>- When mapping automatically using <see cref="TypeMappingDescriptor{T}.AutoMap{TDocument}" /></para>
//		/// <para>- When Indexing this type do not serialize this property and its value</para>
//		/// </summary>
//		bool Ignore { get; set; }

//		/// <summary> Overrides the property name serialized to JSON for this property</summary>
//		string Name { get; set; }
//	}

//	/// <inheritdoc />
//	public class PropertyMapping : IPropertyMapping
//	{
//		public static PropertyMapping Ignored = new PropertyMapping { Ignore = true };

//		/// <inheritdoc />
//		public bool Ignore { get; set; }

//		/// <inheritdoc />
//		public string Name { get; set; }
//	}

//	/// <summary>
//	/// Provides mappings for CLR types
//	/// </summary>
//	public interface IPropertyMappingProvider
//	{
//		/// <summary>
//		/// Creates an <see cref="IPropertyMapping" /> for a <see cref="MemberInfo" />
//		/// </summary>
//		IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
//	}

//	/// <inheritdoc />
//	public class PropertyMappingProvider : IPropertyMappingProvider
//	{
//		protected readonly ConcurrentDictionary<string, IPropertyMapping> Properties = new ConcurrentDictionary<string, IPropertyMapping>();

//		/// <inheritdoc />
//		public virtual IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
//		{
//			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
//			if (Properties.TryGetValue(memberInfoString, out var mapping)) return mapping;

//			mapping = PropertyMappingFromAttributes(memberInfo);
//			Properties.TryAdd(memberInfoString, mapping);
//			return mapping;
//		}

//		private static IPropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
//		{
//			var dataMemberProperty = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);
//			var propertyName = memberInfo.GetCustomAttribute<PropertyNameAttribute>(true);
//			var ignore = memberInfo.GetCustomAttribute<IgnoreAttribute>(true);
//			if (ignore == null && propertyName == null && dataMemberProperty == null) return null;

//			return new PropertyMapping
//			{
//				Name = propertyName?.Name ?? dataMemberProperty?.Name,
//				Ignore = ignore != null || propertyName != null && propertyName.Ignore
//			};
//		}
//	}
//}

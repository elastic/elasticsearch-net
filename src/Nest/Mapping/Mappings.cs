// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Types are gone from Elasticsearch 7.x this class solely exist to help you move your complex mappings over
	/// to the new way of writing the mappings. Use TypeMapping directly instead.
	/// <pre>
	/// This class won't receive updates after the 7.0.0 release please be advised to move over if you need to utilize
	/// new features in the future.
	/// </pre>
	/// </summary>
	[Obsolete("Mappings are no longer type dependent, please use TypeMapping directly")]
	public class Mappings : ObsoleteMappingsBase, ITypeMapping, IEnumerable<ITypeMapping>
	{
		private IEnumerable<ITypeMapping> AsEnumerable => new[] { new TypeMapping() };

		public void Add(object _, ITypeMapping mapping) => Wrapped = mapping ?? Wrapped;
		public ITypeMapping this[object key] => Wrapped;


		public IEnumerator<ITypeMapping> GetEnumerator() => AsEnumerable.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public abstract class ObsoleteMappingsBase : ITypeMapping
	{
		[IgnoreDataMember]
		[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
		public IAllField AllField { get => Wrapped.AllField; set => Wrapped.AllField = value; }
		[DataMember(Name = "date_detection")]
		bool? ITypeMapping.DateDetection { get => Wrapped.DateDetection; set => Wrapped.DateDetection = value; }
		[DataMember(Name = "dynamic")]
		Union<bool, DynamicMapping> ITypeMapping.Dynamic { get => Wrapped.Dynamic; set => Wrapped.Dynamic = value; }
		[DataMember(Name = "dynamic_date_formats")]
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get => Wrapped.DynamicDateFormats; set => Wrapped.DynamicDateFormats = value; }
		[DataMember(Name = "dynamic_templates")]
		IDynamicTemplateContainer ITypeMapping.DynamicTemplates { get => Wrapped.DynamicTemplates; set => Wrapped.DynamicTemplates = value; }
		[DataMember(Name = "_field_names")]
		IFieldNamesField ITypeMapping.FieldNamesField { get => Wrapped.FieldNamesField; set => Wrapped.FieldNamesField = value; }
		[IgnoreDataMember]
		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		IIndexField ITypeMapping.IndexField { get => Wrapped.IndexField; set => Wrapped.IndexField = value; }
		[DataMember(Name = "_meta")]
		IDictionary<string, object> ITypeMapping.Meta { get => Wrapped.Meta; set => Wrapped.Meta = value; }
		[DataMember(Name = "numeric_detection")]
		bool? ITypeMapping.NumericDetection { get => Wrapped.NumericDetection; set => Wrapped.NumericDetection = value; }
		[DataMember(Name = "properties")]
		IProperties ITypeMapping.Properties { get => Wrapped.Properties; set => Wrapped.Properties = value; }
		[DataMember(Name = "_routing")]
		IRoutingField ITypeMapping.RoutingField { get => Wrapped.RoutingField; set => Wrapped.RoutingField = value; }
		[DataMember(Name = "_size")]
		ISizeField ITypeMapping.SizeField { get => Wrapped.SizeField; set => Wrapped.SizeField = value; }
		[DataMember(Name = "_source")]
		ISourceField ITypeMapping.SourceField { get => Wrapped.SourceField; set => Wrapped.SourceField = value; }
		protected ITypeMapping Wrapped { get; set; } = new TypeMapping();

	}

	/// <summary>
	/// The common pattern in NEST is that you can call fluent methods multiple types overriding what was previously set.
	/// This type prevents a user to call Map() multiple times with different types making it crystal clear not only can you
	/// no longer have multiple types in an index <see cref="MappingsDescriptor"/> makes the overloads that take type obsolete
	/// as well. Both <see cref="PreventMappingMultipleTypesDescriptor"/> and <see cref="MappingsDescriptor"/> are obsolete.
	/// Please move to
	/// </summary>
	public class PreventMappingMultipleTypesDescriptor : ObsoleteMappingsBase, IDescriptor
	{
		internal PreventMappingMultipleTypesDescriptor(ITypeMapping mapping) => Wrapped = mapping;
	}

	[Obsolete("MappingsDescriptor is obsolete since 7.x Elasticsearch no longer treats mappings as a dictionary. Please use TypeMappingsDescriptor")]
	public class MappingsDescriptor : IDescriptor
	{
		[Obsolete("MappingsDescriptor is obsolete please call Map() on the parent descriptor")]
		public ITypeMapping Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			new PreventMappingMultipleTypesDescriptor(selector?.Invoke(new TypeMappingDescriptor<T>()));

		[Obsolete("MappingsDescriptor is obsolete please call Map() on the parent descriptor")]
		public ITypeMapping Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			new PreventMappingMultipleTypesDescriptor(selector?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Types are gone from Elasticsearch 7.x the first argument is completely ignored please remove it as we will in 8.x")]
		public ITypeMapping Map<T>(object type, Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			new PreventMappingMultipleTypesDescriptor(selector?.Invoke(new TypeMappingDescriptor<T>()));

		[Obsolete("Types are gone from Elasticsearch 7.x the first argument is completely ignored please remove it as we will in 8.x")]
		public ITypeMapping Map(object type, Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			new PreventMappingMultipleTypesDescriptor(selector?.Invoke(new TypeMappingDescriptor<object>()));
	}
}

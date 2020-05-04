// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TypeMapping))]
	public interface ITypeMapping
	{
		[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
		[IgnoreDataMember]
		IAllField AllField { get; set; }

		/// <summary>
		/// If enabled (default), then new string fields are checked to see whether their contents match
		/// any of the date patterns specified in <see cref="DynamicDateFormats"/>.
		/// If a match is found, a new date field is added with the corresponding format.
		/// </summary>
		[DataMember(Name = "date_detection")]
		bool? DateDetection { get; set; }

		/// <summary>
		/// Whether new unseen fields will be added to the mapping. Default is <c>true</c>.
		/// A value of <c>false</c> will ignore unknown fields and a value of <see cref="DynamicMapping.Strict"/>
		/// will result in an error if an unknown field is encountered in a document.
		/// </summary>
		[DataMember(Name = "dynamic")]
		[JsonFormatter(typeof(DynamicMappingFormatter))]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		/// <summary>
		/// Date formats used by <see cref="DateDetection"/>
		/// </summary>
		[DataMember(Name = "dynamic_date_formats")]
		IEnumerable<string> DynamicDateFormats { get; set; }

		/// <summary>
		/// Dynamic templates allow you to define custom mappings that can be applied to dynamically added fields based on
		/// <para>- the datatype detected by Elasticsearch, with <see cref="IDynamicTemplate.MatchMappingType"/>.</para>
		/// <para>- the name of the field, with <see cref="IDynamicTemplate.Match"/> and <see cref="IDynamicTemplate.Unmatch"/> or
		/// <see cref="IDynamicTemplate.MatchPattern"/>.</para>
		/// <para>- the full dotted path to the field, with <see cref="IDynamicTemplate.PathMatch"/> and
		/// <see cref="IDynamicTemplate.PathUnmatch"/>.</para>
		/// <para>The original field name <c>{name}</c> and the detected datatype <c>{dynamic_type}</c> template variables can be
		/// used in the mapping specification as placeholders.</para>
		/// </summary>
		[DataMember(Name = "dynamic_templates")]
		IDynamicTemplateContainer DynamicTemplates { get; set; }

		/// <summary>
		/// Used to index the names of every field in a document that contains any value other than null.
		/// This field was used by the exists query to find documents that either have or don’t have any non-null value for a particular field.
		/// Now, it only indexes the names of fields that have doc_values and norms disabled.
		/// Can be disabled. Disabling _field_names is often not necessary because it no longer carries the index overhead it once did.
		/// If you have a lot of fields which have doc_values and norms disabled and you do not need to execute exists queries
		/// using those fields you might want to disable
		/// </summary>
		[DataMember(Name = "_field_names")]
		IFieldNamesField FieldNamesField { get; set; }


		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		[IgnoreDataMember]
		IIndexField IndexField { get; set; }

		/// <summary>
		/// Custom meta data to associate with a mapping. Not used by Elasticsearch,
		/// but can be used to store application-specific metadata.
		/// </summary>
		[DataMember(Name = "_meta")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
		IDictionary<string, object> Meta { get; set; }

		/// <summary>
		/// If enabled (not enabled by default), then new string fields are checked to see whether
		/// they wholly contain a numeric value and if so, to map as a numeric field.
		/// </summary>
		[DataMember(Name = "numeric_detection")]
		bool? NumericDetection { get; set; }

		/// <summary>
		/// Specifies the mapping properties
		/// </summary>
		[DataMember(Name = "properties")]
		IProperties Properties { get; set; }

		/// <summary>
		/// Specifies configuration for the _routing parameter
		/// </summary>
		[DataMember(Name = "_routing")]
		IRoutingField RoutingField { get; set; }

		/// <summary>
		/// If enabled, indexes the size in bytes of the original _source field.
		/// Requires mapper-size plugin be installed
		/// </summary>
		[DataMember(Name = "_size")]
		ISizeField SizeField { get; set; }

		/// <summary>
		/// Specifies configuration for the _source field
		/// </summary>
		[DataMember(Name = "_source")]
		ISourceField SourceField { get; set; }
	}

	public class TypeMapping : ITypeMapping
	{
		/// <inheritdoc />
		[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
		public IAllField AllField { get; set; }

		/// <inheritdoc />
		public bool? DateDetection { get; set; }

		/// <inheritdoc />
		public Union<bool, DynamicMapping> Dynamic { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> DynamicDateFormats { get; set; }

		/// <inheritdoc />
		public IDynamicTemplateContainer DynamicTemplates { get; set; }

		/// <inheritdoc />
		public IFieldNamesField FieldNamesField { get; set; }

		/// <inheritdoc />
		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		public IIndexField IndexField { get; set; }

		/// <inheritdoc />
		public IDictionary<string, object> Meta { get; set; }

		/// <inheritdoc />
		public bool? NumericDetection { get; set; }

		/// <inheritdoc />
		public IProperties Properties { get; set; }

		/// <inheritdoc />
		public IRoutingField RoutingField { get; set; }

		/// <inheritdoc />
		public ISizeField SizeField { get; set; }

		/// <inheritdoc />
		public ISourceField SourceField { get; set; }
	}


	public class TypeMappingDescriptor<T> : DescriptorBase<TypeMappingDescriptor<T>, ITypeMapping>, ITypeMapping
		where T : class
	{
		[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
		IAllField ITypeMapping.AllField { get; set; }
		bool? ITypeMapping.DateDetection { get; set; }
		Union<bool, DynamicMapping> ITypeMapping.Dynamic { get; set; }
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get; set; }
		IDynamicTemplateContainer ITypeMapping.DynamicTemplates { get; set; }
		IFieldNamesField ITypeMapping.FieldNamesField { get; set; }
		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		IIndexField ITypeMapping.IndexField { get; set; }
		IDictionary<string, object> ITypeMapping.Meta { get; set; }
		bool? ITypeMapping.NumericDetection { get; set; }
		IProperties ITypeMapping.Properties { get; set; }
		IRoutingField ITypeMapping.RoutingField { get; set; }
		ISizeField ITypeMapping.SizeField { get; set; }
		ISourceField ITypeMapping.SourceField { get; set; }

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type, as well as inferring mappings from the CLR property types.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0) =>
			Assign(Self.Properties.AutoMap<T>(visitor, maxRecursion), (a, v) => a.Properties = v);

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type, as well as inferring mappings from the CLR property types.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(Type documentType, IPropertyVisitor visitor = null, int maxRecursion = 0)
		{
			if (!documentType.IsClass) throw new ArgumentException("must be a reference type", nameof(documentType));
			return Assign(Self.Properties.AutoMap(documentType, visitor, maxRecursion), (a, v) => a.Properties = v);
		}

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type, as well as inferring mappings from the CLR property types.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap<TDocument>(IPropertyVisitor visitor = null, int maxRecursion = 0)
			where TDocument : class =>
			Assign(Self.Properties.AutoMap<TDocument>(visitor, maxRecursion), (a, v) => a.Properties = v);

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type, as well as inferring mappings from the CLR property types.
		/// This overload determines how deep automapping should recurse on a complex CLR type.
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);

		/// <inheritdoc cref="ITypeMapping.Dynamic" />
		public TypeMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <inheritdoc cref="ITypeMapping.Dynamic" />
		public TypeMappingDescriptor<T> Dynamic(bool dynamic = true) => Assign(dynamic, (a, v) => a.Dynamic = v);

		[Obsolete("The _all field is no longer supported in Elasticsearch 7.x and will be removed in the next major release. The value will not be sent in a request. An _all like field can be achieved using copy_to")]
		public TypeMappingDescriptor<T> AllField(Func<AllFieldDescriptor, IAllField> allFieldSelector) =>
			Assign(allFieldSelector, (a, v) => a.AllField = v?.Invoke(new AllFieldDescriptor()));

		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		public TypeMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IIndexField> indexFieldSelector) =>
			Assign(indexFieldSelector, (a, v) => a.IndexField = v?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.SizeField" />
		public TypeMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, ISizeField> sizeFieldSelector) =>
			Assign(sizeFieldSelector, (a, v) => a.SizeField = v?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.SourceField" />
		public TypeMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceFieldSelector) =>
			Assign(sourceFieldSelector, (a, v) => a.SourceField = v?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.SizeField" />
		public TypeMappingDescriptor<T> DisableSizeField(bool? disabled = true) => Assign(new SizeField { Enabled = !disabled }, (a, v) => a.SizeField = v);

		[Obsolete("Configuration for the _index field is no longer supported in Elasticsearch 7.x and will be removed in the next major release.")]
		public TypeMappingDescriptor<T> DisableIndexField(bool? disabled = true) =>
			Assign(new IndexField { Enabled = !disabled }, (a, v) => a.IndexField = v);

		/// <inheritdoc cref="ITypeMapping.DynamicDateFormats" />
		public TypeMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) => Assign(dateFormats, (a, v) => a.DynamicDateFormats = v);

		/// <inheritdoc cref="ITypeMapping.DateDetection" />
		public TypeMappingDescriptor<T> DateDetection(bool? detect = true) => Assign(detect, (a, v) => a.DateDetection = v);

		/// <inheritdoc cref="ITypeMapping.NumericDetection" />
		public TypeMappingDescriptor<T> NumericDetection(bool? detect = true) => Assign(detect, (a, v) => a.NumericDetection = v);

		/// <inheritdoc cref="ITypeMapping.RoutingField" />
		public TypeMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingFieldSelector) =>
			Assign(routingFieldSelector, (a, v) => a.RoutingField = v?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc cref="ITypeMapping.FieldNamesField" />
		public TypeMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesFieldSelector) =>
			Assign(fieldNamesFieldSelector.Invoke(new FieldNamesFieldDescriptor<T>()), (a, v) => a.FieldNamesField = v);

		/// <inheritdoc cref="ITypeMapping.Meta" />
		public TypeMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) =>
			Assign(metaSelector(new FluentDictionary<string, object>()), (a, v) => a.Meta = v);

		/// <inheritdoc cref="ITypeMapping.Meta" />
		public TypeMappingDescriptor<T> Meta(Dictionary<string, object> metaDictionary) => Assign(metaDictionary, (a, v) => a.Meta = v);

		/// <inheritdoc cref="ITypeMapping.Properties" />
		public TypeMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, IPromise<IProperties>> propertiesSelector) =>
			Assign(propertiesSelector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<T>(Self.Properties))?.Value);

		/// <inheritdoc cref="ITypeMapping.Properties" />
		public TypeMappingDescriptor<T> Properties<TDocument>(Func<PropertiesDescriptor<TDocument>, IPromise<IProperties>> propertiesSelector)
			where TDocument : class =>
			Assign(propertiesSelector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<TDocument>(Self.Properties))?.Value);

		/// <inheritdoc cref="ITypeMapping.DynamicTemplates" />
		public TypeMappingDescriptor<T> DynamicTemplates(
			Func<DynamicTemplateContainerDescriptor<T>, IPromise<IDynamicTemplateContainer>> dynamicTemplatesSelector
		) =>
			Assign(dynamicTemplatesSelector, (a, v) => a.DynamicTemplates = v?.Invoke(new DynamicTemplateContainerDescriptor<T>())?.Value);
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TypeMapping))]
	public interface ITypeMapping
	{
		[DataMember(Name = "_all")]
		IAllField AllField { get; set; }

		[DataMember(Name = "date_detection")]
		bool? DateDetection { get; set; }

		[DataMember(Name = "dynamic")]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		[DataMember(Name = "dynamic_date_formats")]
		IEnumerable<string> DynamicDateFormats { get; set; }

		[DataMember(Name = "dynamic_templates")]
		IDynamicTemplateContainer DynamicTemplates { get; set; }

		[DataMember(Name = "_field_names")]
		IFieldNamesField FieldNamesField { get; set; }

		[DataMember(Name = "_index")]
		IIndexField IndexField { get; set; }

		[DataMember(Name = "_meta")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
		IDictionary<string, object> Meta { get; set; }

		[DataMember(Name = "numeric_detection")]
		bool? NumericDetection { get; set; }

		[DataMember(Name = "properties")]
		IProperties Properties { get; set; }

		[DataMember(Name = "_routing")]
		IRoutingField RoutingField { get; set; }

		[DataMember(Name = "_size")]
		ISizeField SizeField { get; set; }

		[DataMember(Name = "_source")]
		ISourceField SourceField { get; set; }
	}

	public class TypeMapping : ITypeMapping
	{
		/// <inheritdoc />
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
		IAllField ITypeMapping.AllField { get; set; }
		bool? ITypeMapping.DateDetection { get; set; }
		Union<bool, DynamicMapping> ITypeMapping.Dynamic { get; set; }
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get; set; }
		IDynamicTemplateContainer ITypeMapping.DynamicTemplates { get; set; }
		IFieldNamesField ITypeMapping.FieldNamesField { get; set; }
		IIndexField ITypeMapping.IndexField { get; set; }
		IDictionary<string, object> ITypeMapping.Meta { get; set; }
		bool? ITypeMapping.NumericDetection { get; set; }
		IProperties ITypeMapping.Properties { get; set; }
		IRoutingField ITypeMapping.RoutingField { get; set; }
		ISizeField ITypeMapping.SizeField { get; set; }
		ISourceField ITypeMapping.SourceField { get; set; }

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0) =>
			Assign(Self.Properties.AutoMap<T>(visitor, maxRecursion), (a, v) => a.Properties = v);

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute" /> attributes set on the
		/// type.
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
		/// type.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap<TDocument>(IPropertyVisitor visitor = null, int maxRecursion = 0)
			where TDocument : class =>
			Assign(Self.Properties.AutoMap<TDocument>(visitor, maxRecursion), (a, v) => a.Properties = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> Dynamic(bool dynamic = true) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> AllField(Func<AllFieldDescriptor, IAllField> allFieldSelector) =>
			Assign(allFieldSelector, (a, v) => a.AllField = v?.Invoke(new AllFieldDescriptor()));

		/// <inheritdoc />
		public TypeMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IIndexField> indexFieldSelector) =>
			Assign(indexFieldSelector, (a, v) => a.IndexField = v?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc />
		public TypeMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, ISizeField> sizeFieldSelector) =>
			Assign(sizeFieldSelector, (a, v) => a.SizeField = v?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc />
		public TypeMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceFieldSelector) =>
			Assign(sourceFieldSelector, (a, v) => a.SourceField = v?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc />
		public TypeMappingDescriptor<T> DisableSizeField(bool? disabled = true) => Assign(new SizeField { Enabled = !disabled }, (a, v) => a.SizeField = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> DisableIndexField(bool? disabled = true) =>
			Assign(new IndexField { Enabled = !disabled }, (a, v) => a.IndexField = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) => Assign(dateFormats, (a, v) => a.DynamicDateFormats = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> DateDetection(bool? detect = true) => Assign(detect, (a, v) => a.DateDetection = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> NumericDetection(bool? detect = true) => Assign(detect, (a, v) => a.NumericDetection = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingFieldSelector) =>
			Assign(routingFieldSelector, (a, v) => a.RoutingField = v?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc />
		public TypeMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesFieldSelector) =>
			Assign(fieldNamesFieldSelector.Invoke(new FieldNamesFieldDescriptor<T>()), (a, v) => a.FieldNamesField = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) =>
			Assign(metaSelector(new FluentDictionary<string, object>()), (a, v) => a.Meta = v);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> Meta(Dictionary<string, object> metaDictionary) => Assign(metaDictionary, (a, v) => a.Meta = v);

		public TypeMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, IPromise<IProperties>> propertiesSelector) =>
			Assign(propertiesSelector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<T>(Self.Properties))?.Value);

		public TypeMappingDescriptor<T> Properties<TDocument>(Func<PropertiesDescriptor<TDocument>, IPromise<IProperties>> propertiesSelector)
			where TDocument : class =>
			Assign(propertiesSelector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<TDocument>(Self.Properties))?.Value);

		/// <inheritdoc />
		public TypeMappingDescriptor<T> DynamicTemplates(
			Func<DynamicTemplateContainerDescriptor<T>, IPromise<IDynamicTemplateContainer>> dynamicTemplatesSelector
		) =>
			Assign(dynamicTemplatesSelector, (a, v) => a.DynamicTemplates = v?.Invoke(new DynamicTemplateContainerDescriptor<T>())?.Value);
	}
}

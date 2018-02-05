using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TypeMapping>))]
	public interface ITypeMapping
	{
		[JsonProperty("dynamic_date_formats")]
		IEnumerable<string> DynamicDateFormats { get; set; }

		[JsonProperty("date_detection")]
		bool? DateDetection { get; set; }

		[JsonProperty("numeric_detection")]
		bool? NumericDetection { get; set; }

		[JsonProperty("_source")]
		ISourceField SourceField { get; set; }

		[JsonProperty("_all")]
		IAllField AllField { get; set; }

		[JsonProperty("_routing")]
		IRoutingField RoutingField { get; set; }

		[JsonProperty("_index")]
		IIndexField IndexField { get; set; }

		[JsonProperty("_size")]
		ISizeField SizeField { get; set; }

		[JsonProperty("_field_names")]
		IFieldNamesField FieldNamesField { get; set; }

		[JsonProperty("_meta")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		IDictionary<string, object> Meta { get; set; }

		[JsonProperty("dynamic_templates")]
		IDynamicTemplateContainer DynamicTemplates { get; set; }

		[JsonProperty("dynamic")]
		Union<bool, DynamicMapping> Dynamic { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		IProperties Properties { get; set; }
	}

	public class TypeMapping : ITypeMapping
	{
		/// <inheritdoc/>
		public IAllField AllField { get; set; }
		/// <inheritdoc/>
		public bool? DateDetection { get; set; }
		/// <inheritdoc/>
		public Union<bool, DynamicMapping> Dynamic { get; set; }
		/// <inheritdoc/>
		public IEnumerable<string> DynamicDateFormats { get; set; }
		/// <inheritdoc/>
		public IDynamicTemplateContainer DynamicTemplates { get; set; }
		/// <inheritdoc/>
		public IFieldNamesField FieldNamesField { get; set; }
		/// <inheritdoc/>
		public IIndexField IndexField { get; set; }
		/// <inheritdoc/>
		public IDictionary<string, object> Meta { get; set; }
		/// <inheritdoc/>
		public bool? NumericDetection { get; set; }
		/// <inheritdoc/>
		public IProperties Properties { get; set; }
		/// <inheritdoc/>
		public IRoutingField RoutingField { get; set; }
		/// <inheritdoc/>
		public ISizeField SizeField { get; set; }
		/// <inheritdoc/>
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
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute"/> attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0) =>
			Assign(a => a.Properties = a.Properties.AutoMap<T>(visitor, maxRecursion));

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute"/> attributes set on the type.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap(Type documentType, IPropertyVisitor visitor = null, int maxRecursion = 0)
		{
			if (!documentType.IsClass()) throw new ArgumentException("must be a reference type", nameof(documentType));
			return Assign(a => a.Properties = a.Properties.AutoMap(documentType, visitor, maxRecursion));
		}

		/// <summary>
		/// Convenience method to map as much as it can based on <see cref="ElasticsearchTypeAttribute"/> attributes set on the type.
		/// This particular overload is useful for automapping any children
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etc)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public TypeMappingDescriptor<T> AutoMap<TDocument>(IPropertyVisitor visitor = null, int maxRecursion = 0)
			where TDocument : class =>
			Assign(a => a.Properties = a.Properties.AutoMap<TDocument>(visitor, maxRecursion));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(a => a.Dynamic = dynamic);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Dynamic(bool dynamic = true) => Assign(a => a.Dynamic = dynamic);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> AllField(Func<AllFieldDescriptor, IAllField> allFieldSelector) => Assign(a => a.AllField = allFieldSelector?.Invoke(new AllFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IIndexField> indexFieldSelector) => Assign(a => a.IndexField = indexFieldSelector?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, ISizeField> sizeFieldSelector) => Assign(a => a.SizeField = sizeFieldSelector?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceFieldSelector) => Assign(a => a.SourceField = sourceFieldSelector?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DisableSizeField(bool? disabled = true) => Assign(a => a.SizeField = new SizeField { Enabled = !disabled });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DisableIndexField(bool? disabled = true) => Assign(a => a.IndexField = new IndexField { Enabled = !disabled });

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) => Assign(a => a.DynamicDateFormats = dateFormats);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DateDetection(bool? detect = true) => Assign(a => a.DateDetection = detect);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> NumericDetection(bool? detect = true) => Assign(a => a.NumericDetection = detect);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingFieldSelector) => Assign(a => a.RoutingField = routingFieldSelector?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesFieldSelector) => Assign(a => a.FieldNamesField = fieldNamesFieldSelector.Invoke(new FieldNamesFieldDescriptor<T>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) =>
			Assign(a => a.Meta = metaSelector(new FluentDictionary<string, object>()));

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> Meta(Dictionary<string, object> metaDictionary) => Assign(a => a.Meta = metaDictionary);

		public TypeMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, IPromise<IProperties>> propertiesSelector) =>
			Assign(a => a.Properties = propertiesSelector?.Invoke(new PropertiesDescriptor<T>(Self.Properties))?.Value);

		public TypeMappingDescriptor<T> Properties<TDocument>(Func<PropertiesDescriptor<TDocument>, IPromise<IProperties>> propertiesSelector)
			where TDocument : class =>
			Assign(a => a.Properties = propertiesSelector?.Invoke(new PropertiesDescriptor<TDocument>(Self.Properties))?.Value);

		/// <inheritdoc/>
		public TypeMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplateContainerDescriptor<T>, IPromise<IDynamicTemplateContainer>> dynamicTemplatesSelector) =>
			Assign(a => a.DynamicTemplates = dynamicTemplatesSelector?.Invoke(new DynamicTemplateContainerDescriptor<T>())?.Value);
	}
}

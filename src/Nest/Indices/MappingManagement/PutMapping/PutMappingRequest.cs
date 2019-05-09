using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutMappingRequest>))]
	public partial interface IPutMappingRequest : ITypeMapping { }

	public interface IPutMappingRequest<T> : IPutMappingRequest where T : class { }

	public partial class PutMappingRequest
	{
		// Needed for ReadAsType
		internal PutMappingRequest() { }

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

	public partial class PutMappingRequest<T> where T : class
	{
		public PutMappingRequest() : this(typeof(T), typeof(T)) { }

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

	[DescriptorFor("IndicesPutMapping")]
	public partial class PutMappingDescriptor<T> where T : class
	{
		/// <summary>/{index}/{type}/_mapping. Will infer the index from the generic type</summary>
		///<param name="type"> this parameter is required</param>
		public PutMappingDescriptor(TypeName type) : base(r=>r.Required("type", type).Required("index", (IndexName)typeof(T))){}

		public PutMappingDescriptor(IndexName index, TypeName type) : base(r => r.Required("index", index).Required("type", type)) { }

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

		[Obsolete("Use the overload that accepts TValue")]
		protected PutMappingDescriptor<T> Assign(Action<ITypeMapping> assigner)
		{
			assigner(this);
			return this;
		}

		protected PutMappingDescriptor<T> Assign<TValue>(TValue value, Action<ITypeMapping, TValue> assigner) =>
			Fluent.Assign(this, value, assigner);

		/// <summary>
		/// Convenience method to map as much as it can based on ElasticType attributes set on the type.
		/// <para>This method also automatically sets up mappings for primitive values types (e.g. int, long, double, DateTime...)</para>
		/// <para>Class types default to object and Enums to int</para>
		/// <para>Later calls can override whatever is set by this call.</para>
		/// </summary>
		public PutMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null, int maxRecursion = 0)
		{
			Self.Properties = Self.Properties.AutoMap<T>(visitor, maxRecursion);
			return this;
		}

		/// <inheritdoc cref="AutoMap(Nest.IPropertyVisitor,int)" />
		public PutMappingDescriptor<T> AutoMap(int maxRecursion) => AutoMap(null, maxRecursion);

		/// <inheritdoc cref="ITypeMapping.Dynamic" />
		public PutMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <inheritdoc cref="ITypeMapping.Dynamic" />
		public PutMappingDescriptor<T> Dynamic(bool? dynamic = true) => Assign(dynamic, (a, v) => a.Dynamic = v);

		/// <inheritdoc cref="ITypeMapping.AllField" />
		public PutMappingDescriptor<T> AllField(Func<AllFieldDescriptor, IAllField> allFieldSelector) =>
			Assign(allFieldSelector, (a, v) => a.AllField = v?.Invoke(new AllFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.IndexField" />
		public PutMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IIndexField> indexFieldSelector) =>
			Assign(indexFieldSelector, (a, v) => a.IndexField = v?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.SizeField" />
		public PutMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, ISizeField> sizeFieldSelector) =>
			Assign(sizeFieldSelector, (a, v) => a.SizeField = v?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.SizeField" />
		public PutMappingDescriptor<T> DisableSizeField(bool? disabled = true) =>
			Assign(disabled, (a, v) => a.SizeField = new SizeField { Enabled = !v });

		/// <inheritdoc cref="ITypeMapping.IndexField" />
		public PutMappingDescriptor<T> DisableIndexField(bool? disabled = true) =>
			Assign(disabled, (a, v) => a.IndexField = new IndexField { Enabled = !v });

		/// <inheritdoc cref="ITypeMapping.DynamicDateFormats" />
		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) =>
			Assign(dateFormats, (a, v) => a.DynamicDateFormats = v);

		/// <inheritdoc cref="ITypeMapping.DateDetection" />
		public PutMappingDescriptor<T> DateDetection(bool? detect = true) => Assign(detect, (a, v) => a.DateDetection = v);

		/// <inheritdoc cref="ITypeMapping.NumericDetection" />
		public PutMappingDescriptor<T> NumericDetection(bool? detect = true) => Assign(detect, (a, v) => a.NumericDetection = v);

		/// <inheritdoc cref="ITypeMapping.SourceField" />
		public PutMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceFieldSelector) =>
			Assign(sourceFieldSelector, (a, v) => a.SourceField = v?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc cref="ITypeMapping.RoutingField" />
		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingFieldSelector) =>
			Assign(routingFieldSelector, (a, v) => a.RoutingField = v?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc cref="ITypeMapping.FieldNamesField" />
		public PutMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesFieldSelector) =>
			Assign(fieldNamesFieldSelector, (a, v) => a.FieldNamesField = v.Invoke(new FieldNamesFieldDescriptor<T>()));

		/// <inheritdoc cref="ITypeMapping.Meta" />
		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) =>
			Assign(metaSelector, (a, v) => a.Meta = v(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="ITypeMapping.Meta" />
		public PutMappingDescriptor<T> Meta(Dictionary<string, object> metaDictionary) => Assign(metaDictionary, (a, v) => a.Meta = v);

		/// <inheritdoc cref="ITypeMapping.Properties" />
		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, IPromise<IProperties>> propertiesSelector) =>
			Assign(propertiesSelector, (a, v) => a.Properties = v?.Invoke(new PropertiesDescriptor<T>(a.Properties))?.Value);

		/// <inheritdoc cref="ITypeMapping.DynamicTemplates" />
		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplateContainerDescriptor<T>, IPromise<IDynamicTemplateContainer>> dynamicTemplatesSelector) =>
			Assign(dynamicTemplatesSelector, (a, v) => a.DynamicTemplates = v?.Invoke(new DynamicTemplateContainerDescriptor<T>())?.Value);
	}
}

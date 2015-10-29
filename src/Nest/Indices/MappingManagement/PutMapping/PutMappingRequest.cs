using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutMappingRequest>))]
	public partial interface IPutMappingRequest : ITypeMapping
	{
	}

	public interface IPutMappingRequest<T> : IPutMappingRequest where T : class { }

	public partial class PutMappingRequest : RequestBase<PutMappingRequestParameters>, IPutMappingRequest
	{
		// Needed for ReadAsType
		internal PutMappingRequest() { }

		/// <inheritdoc/>
		public IAllField AllField { get; set; }
		/// <inheritdoc/>
		public IBoostField BoostField { get; set; }
		/// <inheritdoc/>
		public bool? DateDetection { get; set; }
		/// <inheritdoc/>
		public IEnumerable<string> DynamicDateFormats { get; set; }
		/// <inheritdoc/>
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		/// <inheritdoc/>
		public DynamicMapping? Dynamic { get; set; }
		/// <inheritdoc/>
		public string Analyzer { get; set; }
		/// <inheritdoc/>
		public string SearchAnalyzer { get; set; }
		/// <inheritdoc/>
		public IFieldNamesField FieldNamesField { get; set; }
		/// <inheritdoc/>
		public IIdField IdField { get; set; }
		/// <inheritdoc/>
		public IIndexField IndexField { get; set; }
		/// <inheritdoc/>
		public FluentDictionary<string, object> Meta { get; set; }
		/// <inheritdoc/>
		public bool? NumericDetection { get; set; }
		/// <inheritdoc/>
		public IParentField ParentField { get; set; }
		/// <inheritdoc/>
		public IProperties Properties { get; set; }
		/// <inheritdoc/>
		public IRoutingField RoutingField { get; set; }
		/// <inheritdoc/>
		public ISizeField SizeField { get; set; }
		/// <inheritdoc/>
		public ISourceField SourceField { get; set; }
		/// <inheritdoc/>
		public ITimestampField TimestampField { get; set; }
		/// <inheritdoc/>
		public IList<IMappingTransform> Transform { get; set; }
		/// <inheritdoc/>
		public ITtlField TtlField { get; set; }
		/// <inheritdoc/>
		public ITypeField TypeField { get; set; }
	}

	public partial class PutMappingRequest<T> where T : class
	{
		public PutMappingRequest() : this(typeof(T), typeof(T)) { }

		//TODO constructors
		/// <inheritdoc/>
		public IAllField AllField { get; set; }
		/// <inheritdoc/>
		public IBoostField BoostField { get; set; }
		/// <inheritdoc/>
		public bool? DateDetection { get; set; }
		/// <inheritdoc/>
		public IEnumerable<string> DynamicDateFormats { get; set; }
		/// <inheritdoc/>
		public IDictionary<string, DynamicTemplate> DynamicTemplates { get; set; }
		/// <inheritdoc/>
		public DynamicMapping? Dynamic { get; set; }
		/// <inheritdoc/>
		public string Analyzer { get; set; }
		/// <inheritdoc/>
		public string SearchAnalyzer { get; set; }
		/// <inheritdoc/>
		public IFieldNamesField FieldNamesField { get; set; }
		/// <inheritdoc/>
		public IIdField IdField { get; set; }
		/// <inheritdoc/>
		public IIndexField IndexField { get; set; }
		/// <inheritdoc/>
		public FluentDictionary<string, object> Meta { get; set; }
		/// <inheritdoc/>
		public bool? NumericDetection { get; set; }
		/// <inheritdoc/>
		public IParentField ParentField { get; set; }
		/// <inheritdoc/>
		public IProperties Properties { get; set; }
		/// <inheritdoc/>
		public IRoutingField RoutingField { get; set; }
		/// <inheritdoc/>
		public ISizeField SizeField { get; set; }
		/// <inheritdoc/>
		public ISourceField SourceField { get; set; }
		/// <inheritdoc/>
		public ITimestampField TimestampField { get; set; }
		/// <inheritdoc/>
		public IList<IMappingTransform> Transform { get; set; }
		/// <inheritdoc/>
		public ITtlField TtlField { get; set; }
		/// <inheritdoc/>
		public ITypeField TypeField { get; set; }
	}

	//TODO why is there no typed generated descriptor

	[DescriptorFor("IndicesPutMapping")]
	public partial class PutMappingDescriptor<T> where T : class
	{
		public PutMappingDescriptor() : this(typeof(T), typeof(T)) { }
		public PutMappingDescriptor(IndexName index, TypeName type) : base(r=>r.Required("index", index).Required("type", type)) { }

		protected PutMappingDescriptor<T> Assign(Action<ITypeMapping> assigner) => Fluent.Assign(this, assigner);

		IAllField ITypeMapping.AllField { get; set; }
		IBoostField ITypeMapping.BoostField { get; set; }
		bool? ITypeMapping.DateDetection { get; set; }
		IEnumerable<string> ITypeMapping.DynamicDateFormats { get; set; }
		string ITypeMapping.Analyzer { get; set; }
		string ITypeMapping.SearchAnalyzer { get; set; }
		IDictionary<string, DynamicTemplate> ITypeMapping.DynamicTemplates { get; set; }
		DynamicMapping? ITypeMapping.Dynamic { get; set; }
		IFieldNamesField ITypeMapping.FieldNamesField { get; set; }
		IIdField ITypeMapping.IdField { get; set; }
		IIndexField ITypeMapping.IndexField { get; set; }
		FluentDictionary<string, object> ITypeMapping.Meta { get; set; }
		bool? ITypeMapping.NumericDetection { get; set; }
		IParentField ITypeMapping.ParentField { get; set; }
		IProperties ITypeMapping.Properties { get; set; }
		IRoutingField ITypeMapping.RoutingField { get; set; }
		ISizeField ITypeMapping.SizeField { get; set; }
		ISourceField ITypeMapping.SourceField { get; set; }
		ITimestampField ITypeMapping.TimestampField { get; set; }
		IList<IMappingTransform> ITypeMapping.Transform { get; set; }
		ITtlField ITypeMapping.TtlField { get; set; }
		ITypeField ITypeMapping.TypeField { get; set; }

		/// <summary>
		/// Convenience method to map as much as it can based on ElasticType attributes set on the type.
		/// <pre>This method also automatically sets up mappings for known values types (int, long, double, datetime, etcetera)</pre>
		/// <pre>Class types default to object and Enums to int</pre>
		/// <pre>Later calls can override whatever is set is by this call.</pre>
		/// </summary>
		public PutMappingDescriptor<T> AutoMap(IPropertyVisitor visitor = null) => Assign(a =>
		{
			a.Properties = a.Properties ?? new Properties();
			var autoProperties = new PropertyWalker(typeof(T), visitor).GetProperties();
			foreach (var autoProperty in autoProperties)
				a.Properties[autoProperty.Key] = autoProperty.Value;
		});

		/// <inheritdoc/>
		public PutMappingDescriptor<T> Dynamic(DynamicMapping dynamic) => Assign(a => a.Dynamic = dynamic);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> Dynamic(bool dynamic = true) => this.Dynamic(dynamic ? DynamicMapping.Allow : DynamicMapping.Ignore);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> SetParent(TypeName parentType) => Assign(a => a.ParentField = new ParentField { Type = parentType });

		/// <inheritdoc/>
		public PutMappingDescriptor<T> SetParent<K>() where K : class => Assign(a => a.ParentField = new ParentField { Type = typeof(K) });

		/// <inheritdoc/>
		public PutMappingDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(a => a.SearchAnalyzer = searchAnalyzer);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> AllField(Func<AllFieldDescriptor, AllFieldDescriptor> allFieldSelector) => Assign(a => a.AllField = allFieldSelector?.Invoke(new AllFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> IndexField(Func<IndexFieldDescriptor, IndexFieldDescriptor> indexFieldSelector) => Assign(a => a.IndexField = indexFieldSelector?.Invoke(new IndexFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> SizeField(Func<SizeFieldDescriptor, SizeFieldDescriptor> sizeFieldSelector) => Assign(a => a.SizeField = sizeFieldSelector?.Invoke(new SizeFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> DisableSizeField(bool disabled = true) => Assign(a => a.SizeField = new SizeField { Enabled = !disabled });

		/// <inheritdoc/>
		public PutMappingDescriptor<T> DisableIndexField(bool disabled = true) => Assign(a => a.IndexField = new IndexField { Enabled = !disabled });

		/// <inheritdoc/>
		public PutMappingDescriptor<T> DynamicDateFormats(IEnumerable<string> dateFormats) => Assign(a => a.DynamicDateFormats = dateFormats);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> DateDetection(bool detect = true) => Assign(a => a.DateDetection = detect);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> NumericDetection(bool detect = true) => Assign(a => a.NumericDetection = detect);

		/// <inheritdoc/>
		public PutMappingDescriptor<T> Transform(Func<MappingTransformDescriptor, IMappingTransform> mappingTransformSelector)
		{
			//TODO MappingTransform needs a descriptor so we no longer make this call mutate state
			var t = mappingTransformSelector?.Invoke(new MappingTransformDescriptor());
			if (t == null) return this;
			if (((IPutMappingRequest)this).Transform == null) ((IPutMappingRequest)this).Transform = new List<IMappingTransform>();
			((IPutMappingRequest)this).Transform.Add(t);
			return this;
		}

		/// <inheritdoc/>
		public PutMappingDescriptor<T> IdField(Func<IdFieldDescriptor, IIdField> idMapper) => Assign(a => a.IdField = idMapper?.Invoke(new IdFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> TypeField(Func<TypeFieldDescriptor, ITypeField> typeMapper) => Assign(a => a.TypeField = typeMapper?.Invoke(new TypeFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> SourceField(Func<SourceFieldDescriptor, ISourceField> sourceMapper) => Assign(a => a.SourceField = sourceMapper?.Invoke(new SourceFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> BoostField(Func<BoostFieldDescriptor<T>, IBoostField> boostMapper) => Assign(a => a.BoostField = boostMapper?.Invoke(new BoostFieldDescriptor<T>()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> RoutingField(Func<RoutingFieldDescriptor<T>, IRoutingField> routingMapper) => Assign(a => a.RoutingField = routingMapper?.Invoke(new RoutingFieldDescriptor<T>()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampMapper) => Assign(a => a.TimestampField = timestampMapper?.Invoke(new TimestampFieldDescriptor<T>()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> FieldNamesField(Func<FieldNamesFieldDescriptor<T>, IFieldNamesField> fieldNamesMapper) => Assign(a => a.FieldNamesField = fieldNamesMapper.Invoke(new FieldNamesFieldDescriptor<T>()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldMapper) => Assign(a => a.TtlField = ttlFieldMapper?.Invoke(new TtlFieldDescriptor()));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) => Assign(a => a.Meta = metaSelector(new FluentDictionary<string, object>()));

		public PutMappingDescriptor<T> Properties(Func<PropertiesDescriptor<T>, PropertiesDescriptor<T>> propertiesSelector) =>
			Assign(a => a.Properties = propertiesSelector?.Invoke(new PropertiesDescriptor<T>(a.Properties)));

		/// <inheritdoc/>
		public PutMappingDescriptor<T> DynamicTemplates(Func<DynamicTemplatesDescriptor<T>, DynamicTemplatesDescriptor<T>> dynamicTemplatesSelector)
		{
			//TODO _DELETES concept is wrong?
			dynamicTemplatesSelector.ThrowIfNull("dynamicTemplatesSelector");
			var templates = dynamicTemplatesSelector(new DynamicTemplatesDescriptor<T>());
			if (((IPutMappingRequest)this).DynamicTemplates == null)
				((IPutMappingRequest)this).DynamicTemplates = new Dictionary<string, DynamicTemplate>();
			foreach (var t in templates._Deletes)
				((IPutMappingRequest)this).DynamicTemplates.Remove(t);
			foreach (var t in templates.Templates)
				((IPutMappingRequest)this).DynamicTemplates[t.Key] = t.Value;
			return this;
		}
	}
}
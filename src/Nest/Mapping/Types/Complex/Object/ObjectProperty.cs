using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IObjectProperty : IElasticsearchProperty
	{
		[JsonProperty("dynamic")]
		[JsonConverter(typeof(DynamicMappingOptionConverter))]
		DynamicMappingOption? Dynamic { get; set; }

		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		string Path { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(ElasticTypesConverter))]
		IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
	}

	public class ObjectProperty : ElasticsearchProperty, IObjectProperty
	{
		public ObjectProperty() : base("object") { }

		protected ObjectProperty(TypeName typeName) : base(typeName) { }

		protected internal ObjectProperty(TypeName typeName, ObjectAttribute attribute)
			: this(attribute)
		{
			Type = typeName;
		}

		internal ObjectProperty(ObjectAttribute attribute)
			: base("object", attribute)
		{
			Dynamic = attribute.Dynamic;
			Enabled = attribute.Enabled;
			IncludeInAll = attribute.IncludeInAll;
			Path = attribute.Path;
		}

		public DynamicMappingOption? Dynamic { get; set; }
		public bool? Enabled { get; set; }
		public bool? IncludeInAll { get; set; }
		public string Path { get; set; }
		public IDictionary<FieldName, IElasticsearchProperty> Properties { get; set; }
	}

	public class ObjectTypeDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<ObjectTypeDescriptor<TParent, TChild>, IObjectProperty, TParent, TChild>, IObjectProperty
		where TParent : class
		where TChild : class
	{
	}

	public abstract class ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>
		: PropertyDescriptorBase<TDescriptor, TInterface, TParent>, IObjectProperty
		where TDescriptor : ObjectPropertyDescriptorBase<TDescriptor, TInterface, TParent, TChild>, TInterface
		where TInterface : class, IObjectProperty
		where TParent : class
		where TChild : class
	{
		internal TypeName _TypeName { get; set; }

		DynamicMappingOption? IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		bool? IObjectProperty.IncludeInAll { get; set; }
		string IObjectProperty.Path { get; set; }
		IDictionary<FieldName, IElasticsearchProperty> IObjectProperty.Properties { get; set; }
	
		public ObjectPropertyDescriptorBase()
		{
			_TypeName = TypeName.Create<TChild>();
		}

		public TDescriptor Dynamic(DynamicMappingOption dynamic) =>
			Assign(a => a.Dynamic = dynamic);

		public TDescriptor Dynamic(bool dynamic = true) =>
			Dynamic(dynamic ? DynamicMappingOption.Allow : DynamicMappingOption.Ignore);

		public TDescriptor Enabled(bool enabled = true) =>
			Assign(a => a.Enabled = enabled);

		public TDescriptor IncludeInAll(bool includeInAll = true) =>
			Assign(a => a.IncludeInAll = includeInAll);

		public TDescriptor Path(string path) => 
			Assign(a => a.Path = path);

		public TDescriptor Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> selector) => Assign(a =>
		{
			selector.ThrowIfNull(nameof(selector));
			var properties = selector(new PropertiesDescriptor<TChild>());
			if (a.Properties == null)
				a.Properties = new Dictionary<FieldName, IElasticsearchProperty>();
			foreach (var p in properties.Properties)
				a.Properties[p.Key] = p.Value;
		});

		public TDescriptor AutoMap(ITypeVisitor visitor = null) => Assign(a =>
		{
			var mapper = new PropertyWalker(typeof(TChild), visitor);
			a.Properties = mapper.GetProperties();	
		});
	}
}
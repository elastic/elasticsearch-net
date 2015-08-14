using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Nest.Resolvers.Writers;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IObjectType : IElasticType
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
		IDictionary<FieldName, IElasticType> Properties { get; set; }
	}

	public class ObjectType : ElasticType, IObjectType
	{
		public ObjectType() : base("object") { }
		protected ObjectType(TypeName typeName) : base(typeName) { }

		public DynamicMappingOption? Dynamic { get; set; }
		public bool? Enabled { get; set; }
		public bool? IncludeInAll { get; set; }
		public string Path { get; set; }
		public IDictionary<FieldName, IElasticType> Properties { get; set; }
	}

	public class ObjectTypeDescriptor<TParent, TChild>
		: ObjectTypeDescriptorBase<ObjectTypeDescriptor<TParent, TChild>, IObjectType, TParent, TChild>, IObjectType
		where TParent : class
		where TChild : class
	{
	}

	public abstract class ObjectTypeDescriptorBase<TDescriptor, TInterface, TParent, TChild>
		: TypeDescriptorBase<TDescriptor, TInterface, TParent>, IObjectType
		where TDescriptor : ObjectTypeDescriptorBase<TDescriptor, TInterface, TParent, TChild>, TInterface
		where TInterface : class, IObjectType
		where TParent : class
		where TChild : class
	{
		internal TypeName _TypeName { get; set; }

		DynamicMappingOption? IObjectType.Dynamic { get; set; }
		bool? IObjectType.Enabled { get; set; }
		bool? IObjectType.IncludeInAll { get; set; }
		string IObjectType.Path { get; set; }
		IDictionary<FieldName, IElasticType> IObjectType.Properties { get; set; }
	
		public ObjectTypeDescriptorBase()
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
				a.Properties = new Dictionary<FieldName, IElasticType>();
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
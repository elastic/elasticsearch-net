using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Nest.Resolvers.Writers;

namespace Nest
{
	public interface IObjectType : IElasticType
	{
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectType : ElasticType, IElasticType
	{
		public ObjectType() : base("object") { }

		protected ObjectType(TypeName typeName) : base(typeName)
		{

		}

		[JsonProperty("dynamic")]
		[JsonConverter(typeof(DynamicMappingOptionConverter))]
		public DynamicMappingOption? Dynamic { get; set; }

		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(ElasticTypesConverter))]
		public IDictionary<FieldName, IElasticType> Properties { get; set; }

	}

	public class ObjectTypeDescriptor<TParent, TChild>
		where TParent : class
		where TChild : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal ObjectType _Mapping { get; set; }
		internal TypeName _TypeName { get; set; }
		public ElasticInferrer Infer { get; set; }

		public ObjectTypeDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._TypeName = TypeName.Create<TChild>();
			this._Mapping = new ObjectType() { };
			this._connectionSettings = connectionSettings;
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}

		public ObjectTypeDescriptor<TParent, TChild> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public ObjectTypeDescriptor<TParent, TChild> Name(Expression<Func<TParent, TChild>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public ObjectTypeDescriptor<TParent, TChild> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(TChild), this._TypeName, this._connectionSettings, maxRecursion);
			var mapping = writer.ObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			foreach (var p in properties)
			{
				this._Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}

		public ObjectTypeDescriptor<TParent, TChild> Dynamic(DynamicMappingOption dynamic)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public ObjectTypeDescriptor<TParent, TChild> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMappingOption.Allow : DynamicMappingOption.Ignore);
		}
		public ObjectTypeDescriptor<TParent, TChild> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public ObjectTypeDescriptor<TParent, TChild> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public ObjectTypeDescriptor<TParent, TChild> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public ObjectTypeDescriptor<TParent, TChild> Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<TChild>(this._connectionSettings));
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				var key = this.Infer.FieldName(p.Key);
				_Mapping.Properties[key] = p.Value;
			}
			return this;
		}
	}
}
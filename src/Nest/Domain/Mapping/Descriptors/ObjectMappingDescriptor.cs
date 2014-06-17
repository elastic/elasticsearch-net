using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Writers;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class ObjectMappingDescriptor<TParent, TChild>
		where TParent : class
		where TChild : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal ObjectMapping _Mapping { get; set; }
		internal TypeNameMarker _TypeName { get; set; }
		public ElasticInferrer Infer { get; set; }

		public ObjectMappingDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._TypeName = TypeNameMarker.Create<TChild>();
			this._Mapping = new ObjectMapping() { };
			this._connectionSettings = connectionSettings;
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}

		public ObjectMappingDescriptor<TParent, TChild> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Name(Expression<Func<TParent, TChild>> objectPath)
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
		public ObjectMappingDescriptor<TParent, TChild> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(TChild), this._TypeName, this._connectionSettings, maxRecursion);
			var mapping = writer.ObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			var properties = mapping.Properties;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

			foreach (var p in properties)
			{
				this._Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}

		public ObjectMappingDescriptor<TParent, TChild> Dynamic(DynamicMappingOption dynamic)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMappingOption.allow : DynamicMappingOption.ignore);
		}
		public ObjectMappingDescriptor<TParent, TChild> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public ObjectMappingDescriptor<TParent, TChild> Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<TChild>(this._connectionSettings));
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<PropertyNameMarker, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				var key = this.Infer.PropertyName(p.Key);
				_Mapping.Properties[key] = p.Value;
			}
			return this;
		}
	}
}
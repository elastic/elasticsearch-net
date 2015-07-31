using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class PropertiesDescriptor<T> where T : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public IDictionary<FieldName, IElasticType> Properties { get; private set; }

		internal IList<string> _Deletes = new List<string>();

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<FieldName, IElasticType>();
		}
		
		public PropertiesDescriptor(IConnectionSettingsValues connectionSettings) : this()
		{
			this._connectionSettings = connectionSettings;
		}

		public PropertiesDescriptor<T> Remove(string name)
		{
			this._Deletes.Add(name);
			return this;
		}

		public PropertiesDescriptor<T> String(Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new StringTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new ArgumentException("Could not get field name for string mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Number(Func<NumberTypeDescriptor<T>, NumberTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new NumberTypeDescriptor<T>()) as IElasticType;
			if (type == null || type.Name.IsConditionless())
				throw new ArgumentException("Could not get field name for number mapping");
			this.Properties[type.Name] = type;
			return this;
		}

		public PropertiesDescriptor<T> Date(Func<DateTypeDescriptor<T>, DateTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new DateTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for date mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Boolean(Func<BooleanTypeDescriptor<T>, BooleanTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new BooleanTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for boolean mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Binary(Func<BinaryTypeDescriptor<T>, BinaryTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new BinaryTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for binary mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Attachment(Func<AttachmentTypeDescriptor<T>, AttachmentTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new AttachmentTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for attachment mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, ObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new ObjectTypeDescriptor<T, TChild>(this._connectionSettings));
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for object mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		
		public PropertiesDescriptor<T> NestedObject<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, NestedObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new NestedObjectTypeDescriptor<T, TChild>(this._connectionSettings));
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for nested sobject mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Ip(Func<IpTypeDescriptor<T>, IpTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new IpTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for IP mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointTypeDescriptor<T>, GeoPointTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new GeoPointTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for geo point mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> GeoShape(Func<GeoShapeTypeDescriptor<T>, GeoShapeTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new GeoShapeTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for geo shape mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		public PropertiesDescriptor<T> Generic(Func<GenericMappingDescriptor<T>, GenericMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new GenericMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for generic mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

        public PropertiesDescriptor<T> Completion(Func<CompletionTypeDescriptor<T>, CompletionTypeDescriptor<T>> selector)
        {
            selector.ThrowIfNull(nameof(selector));
            var d = selector(new CompletionTypeDescriptor<T>());
            if (d == null || d._Mapping.Name.IsConditionless())
                throw new Exception("Could not get field name for completion mapping");
            this.Properties.Add(d._Mapping.Name, d._Mapping);
            return this;
        }

		public PropertiesDescriptor<T> Custom(IElasticType customMapping)
		{
			customMapping.ThrowIfNull(nameof(customMapping));
			if (customMapping.Name.IsConditionless())
                throw new Exception("Could not get field name for custom mapping");
			this.Properties.Add(customMapping.Name, customMapping);
			return this;
		}

		public PropertiesDescriptor<T> Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, Murmur3HashTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new Murmur3HashTypeDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for mumur hash mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}
	}
}
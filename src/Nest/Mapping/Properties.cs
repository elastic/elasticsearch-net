using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class PropertiesDescriptor<T> where T : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		
		internal IList<string> _Deletes = new List<string>();

		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public IDictionary<FieldName, IElasticType> Properties { get; private set; }

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<FieldName, IElasticType>();
		}
		
		public PropertiesDescriptor(IConnectionSettingsValues connectionSettings) : this()
		{
			_connectionSettings = connectionSettings;
		}

		public PropertiesDescriptor<T> Remove(string name)
		{
			_Deletes.Add(name);
			return this;
		}

		private PropertiesDescriptor<T> SetProperty<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector)
			where TDescriptor : class, TInterface
			where TInterface : class, IElasticType
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(Activator.CreateInstance<TDescriptor>());
			var typeName = typeof(TInterface).Name;
			if (type == null || type.Name.IsConditionless())
				throw new ArgumentException($"Could not get field name for {typeName} mapping");
			Properties[type.Name] = type;
			return this;
		}

		public PropertiesDescriptor<T> String(Func<StringTypeDescriptor<T>, IStringType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Number(Func<NumberTypeDescriptor<T>, INumberType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Date(Func<DateTypeDescriptor<T>, IDateType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Boolean(Func<BooleanTypeDescriptor<T>, IBooleanType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Binary(Func<BinaryTypeDescriptor<T>, IBinaryType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Attachment(Func<AttachmentTypeDescriptor<T>, IAttachmentType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, ObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Nested<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, NestedObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Ip(Func<IpTypeDescriptor<T>, IIpType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointTypeDescriptor<T>, IGeoPointType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoShape(Func<GeoShapeTypeDescriptor<T>, GeoShapeTypeDescriptor<T>> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Generic(Func<GenericMappingDescriptor<T>, GenericMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull(nameof(selector));
			var d = selector(new GenericMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for generic mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public PropertiesDescriptor<T> Completion(Func<CompletionTypeDescriptor<T>, ICompletionType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Custom(IElasticType customMapping)
		{
			customMapping.ThrowIfNull(nameof(customMapping));
			if (customMapping.Name.IsConditionless())
                throw new Exception("Could not get field name for custom mapping");
			this.Properties.Add(customMapping.Name, customMapping);
			return this;
		}

		public PropertiesDescriptor<T> Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, IMurmur3HashType> selector) => SetProperty(selector);
	}
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPropertiesDescriptor<T, TReturnType>
		where T : class
		where TReturnType : class
	{
		TReturnType String(Func<StringPropertyDescriptor<T>, IStringProperty> selector);
		TReturnType Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector);
		TReturnType TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector);
		TReturnType Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector);
		TReturnType Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector);
		TReturnType Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector);
		TReturnType Attachment(Func<AttachmentPropertyDescriptor<T>, IAttachmentProperty> selector);
		TReturnType Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class;
		TReturnType Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class;
		TReturnType Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector);
		TReturnType GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector);
		TReturnType GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector);
		TReturnType Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector);
		TReturnType Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector);
	}

	public class PropertiesDescriptor<T> : IPropertiesDescriptor<T, PropertiesDescriptor<T>>
		where T : class
	{
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public IDictionary<FieldName, IElasticsearchProperty> Properties { get; private set; }

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<FieldName, IElasticsearchProperty>();
		}

		public PropertiesDescriptor<T> String(Func<StringPropertyDescriptor<T>, IStringProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Attachment(Func<AttachmentPropertyDescriptor<T>, IAttachmentProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Custom(IElasticsearchProperty customType) => SetProperty(customType);

		private PropertiesDescriptor<T> SetProperty<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector)
			where TDescriptor : class, TInterface, new()
			where TInterface : class, IElasticsearchProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			SetProperty(type);
			return this;
		}

		private PropertiesDescriptor<T> SetProperty(IElasticsearchProperty type)
		{
			type.ThrowIfNull(nameof(type));
			var typeName = type.GetType().Name;
			if (type.Name.IsConditionless())
				throw new ArgumentException($"Could not get field name for {typeName} mapping");
			Properties[type.Name] = type;
			return this;
		}
	}
}
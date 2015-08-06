using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPropertiesDescriptor<T, TReturnType>
		where T : class
		where TReturnType : class
	{
		TReturnType String(Func<StringTypeDescriptor<T>, IStringType> selector);
		TReturnType Number(Func<NumberTypeDescriptor<T>, INumberType> selector);
		TReturnType TokenCount(Func<TokenCountTypeDescriptor<T>, ITokenCountType> selector);
		TReturnType Date(Func<DateTypeDescriptor<T>, IDateType> selector);
		TReturnType Boolean(Func<BooleanTypeDescriptor<T>, IBooleanType> selector);
		TReturnType Binary(Func<BinaryTypeDescriptor<T>, IBinaryType> selector);
		TReturnType Attachment(Func<AttachmentTypeDescriptor<T>, IAttachmentType> selector);
		TReturnType Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectType> selector)
			where TChild : class;
		TReturnType Nested<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, INestedType> selector)
			where TChild : class;
		TReturnType Ip(Func<IpTypeDescriptor<T>, IIpType> selector);
		TReturnType GeoPoint(Func<GeoPointTypeDescriptor<T>, IGeoPointType> selector);
		TReturnType GeoShape(Func<GeoShapeTypeDescriptor<T>, IGeoShapeType> selector);
		TReturnType Completion(Func<CompletionTypeDescriptor<T>, ICompletionType> selector);
		TReturnType Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, IMurmur3HashType> selector);
	}

	public class PropertiesDescriptor<T> : IPropertiesDescriptor<T, PropertiesDescriptor<T>>
		where T : class
	{
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public IDictionary<FieldName, IElasticType> Properties { get; private set; }

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<FieldName, IElasticType>();
		}

		public PropertiesDescriptor<T> String(Func<StringTypeDescriptor<T>, IStringType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Number(Func<NumberTypeDescriptor<T>, INumberType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> TokenCount(Func<TokenCountTypeDescriptor<T>, ITokenCountType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Date(Func<DateTypeDescriptor<T>, IDateType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Boolean(Func<BooleanTypeDescriptor<T>, IBooleanType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Binary(Func<BinaryTypeDescriptor<T>, IBinaryType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Attachment(Func<AttachmentTypeDescriptor<T>, IAttachmentType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectType> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Nested<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, INestedType> selector)
			where TChild : class => SetProperty(selector);

		public PropertiesDescriptor<T> Ip(Func<IpTypeDescriptor<T>, IIpType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointTypeDescriptor<T>, IGeoPointType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> GeoShape(Func<GeoShapeTypeDescriptor<T>, IGeoShapeType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Completion(Func<CompletionTypeDescriptor<T>, ICompletionType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, IMurmur3HashType> selector) => SetProperty(selector);

		public PropertiesDescriptor<T> Custom(IElasticType customType) => SetProperty(customType);

		private PropertiesDescriptor<T> SetProperty<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector)
			where TDescriptor : class, TInterface, new()
			where TInterface : class, IElasticType
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			SetProperty(type);
			return this;
		}

		private PropertiesDescriptor<T> SetProperty(IElasticType type)
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
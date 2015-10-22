using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(PropertiesJsonConverter))]
	public interface IProperties : IIsADictionary<PropertyName, IProperty>
	{
		
	}

	public class Properties : IsADictionary<PropertyName, IProperty>, IProperties
	{
		public Properties() : base() { }
		public Properties(IDictionary<PropertyName, IProperty> container) : base(container) { }
		public Properties(Dictionary<PropertyName, IProperty> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(PropertyName name, IProperty property) => this.BackingDictionary.Add(name, property);
	}

	public class Properties<T> : IsADictionary<PropertyName, IProperty>, IProperties
	{
		public Properties() : base() { }
		public Properties(IDictionary<PropertyName, IProperty> container) : base(container) { }
		public Properties(IProperties properties) : base(properties) { }
		public Properties(Dictionary<PropertyName, IProperty> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(PropertyName name, IProperty property) => this.BackingDictionary.Add(name, property);
		public void Add(Expression<Func<T, object>> name, IProperty property) => this.BackingDictionary.Add(name, property);
	}

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

	public class PropertiesDescriptor<T> 
		: IsADictionaryDescriptor<PropertiesDescriptor<T>, IProperties, PropertyName, IProperty>, IPropertiesDescriptor<T, PropertiesDescriptor<T>>, IProperties
		where T : class
	{
		public PropertiesDescriptor() : base() { }
		public PropertiesDescriptor(IProperties properties) : base(properties) { }

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

		public PropertiesDescriptor<T> Custom(IProperty customType) => SetProperty(customType);

		private PropertiesDescriptor<T> SetProperty<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector)
			where TDescriptor : class, TInterface, new()
			where TInterface : IProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			SetProperty(type);
			return this;
		}

		private PropertiesDescriptor<T> SetProperty(IProperty type)
		{
			type.ThrowIfNull(nameof(type));
			var typeName = type.GetType().Name;
			if (type.Name.IsConditionless())
				throw new ArgumentException($"Could not get field name for {typeName} mapping");

			return this.Assign(a => a.Dictionary[type.Name] = type);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
	public interface IProperties : IHasADictionary<FieldName, IElasticsearchProperty>
	{
		
	}

	public class Properties : IsADictionary<FieldName, IElasticsearchProperty>, IProperties
	{
		IDictionary<FieldName, IElasticsearchProperty> IHasADictionary<FieldName, IElasticsearchProperty>.Dictionary => this.BackingDictionary;
		public Properties() : base() { }
		public Properties(IDictionary<FieldName, IElasticsearchProperty> container) : base(container) { }
		public Properties(IProperties properties) : base(properties?.Dictionary) { }
		public Properties(Dictionary<FieldName, IElasticsearchProperty> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(FieldName field, IElasticsearchProperty property) => this.BackingDictionary.Add(field, property);
	}

	public class Properties<T> : IsADictionary<FieldName, IElasticsearchProperty>, IProperties
	{
		IDictionary<FieldName, IElasticsearchProperty> IHasADictionary<FieldName, IElasticsearchProperty>.Dictionary => this.BackingDictionary;
		public Properties() : base() { }
		public Properties(IDictionary<FieldName, IElasticsearchProperty> container) : base(container) { }
		public Properties(IProperties properties) : base(properties?.Dictionary) { }
		public Properties(Dictionary<FieldName, IElasticsearchProperty> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(FieldName field, IElasticsearchProperty property) => this.BackingDictionary.Add(field, property);
		public void Add(Expression<Func<T, object>> field, IElasticsearchProperty property) => this.BackingDictionary.Add(field, property);
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
		: HasADictionary<PropertiesDescriptor<T>, IProperties, FieldName, IElasticsearchProperty>, IPropertiesDescriptor<T, PropertiesDescriptor<T>>, IProperties
		where T : class
	{
		IDictionary<FieldName, IElasticsearchProperty> IHasADictionary<FieldName, IElasticsearchProperty>.Dictionary => this.BackingDictionary;

		public PropertiesDescriptor() : base() { }
		public PropertiesDescriptor(IProperties properties) : base(properties?.Dictionary) { }

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
			where TInterface : IElasticsearchProperty
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

			return this.Assign(a => a.Dictionary.Add(type.Name, type));
		}
	}
}
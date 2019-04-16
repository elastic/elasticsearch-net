using System;
using System.Collections.Concurrent;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	internal interface IJsonFormatterResolverWithSettings
	{
		IConnectionSettingsValues Settings { get; }
	}

	internal class NestFormatterResolver : IJsonFormatterResolver, IJsonFormatterResolverWithSettings
	{
		private readonly IJsonFormatter<object> _fallbackFormatter;
		private readonly InnerResolver _innerFormatterResolver;

		public NestFormatterResolver(IConnectionSettingsValues settings)
		{
			Settings = settings;
			_innerFormatterResolver = new InnerResolver(settings);
			_fallbackFormatter = new DynamicObjectTypeFallbackFormatter(_innerFormatterResolver);
		}

		public IConnectionSettingsValues Settings { get; }

		public IJsonFormatter<T> GetFormatter<T>() =>
			typeof(T) == typeof(object)
				? (IJsonFormatter<T>)_fallbackFormatter
				: _innerFormatterResolver.GetFormatter<T>();

		internal sealed class InnerResolver : IJsonFormatterResolver
		{
			private static readonly IJsonFormatterResolver[] Resolvers =
			{
				// IL emit a resolver that registers formatters
				DynamicCompositeResolver.Create(new IJsonFormatter[]
				{
					new QueryContainerCollectionFormatter(),
					new QueryContainerListFormatter(),
					new SimpleQueryStringFlagsFormatter(),
					// TODO: condition on TimeSpanToStringFormatter and NullableTimeSpanToStringFormatter to only take effect when StringTimeSpanAttribute is not present.
					new TimeSpanToStringFormatter(),
					new NullableTimeSpanToStringFormatter(),
					new JsonNetCompatibleUriFormatter(),
				}, new IJsonFormatterResolver[0]),
				BuiltinResolver.Instance, // Builtin primitives
				ElasticsearchNetEnumResolver.Instance, // Specialized Enum handling
				AttributeFormatterResolver.Instance, // [JsonFormatter]
				ReadAsFormatterResolver.Instance, // [ReadAs]
				IsADictionaryFormatterResolver.Instance, // IsADictionaryBase<TKey, TValue>
				DynamicGenericResolver.Instance, // T[], List<T>, etc...
				InterfaceGenericDictionaryResolver.Instance,
				InterfaceGenericReadOnlyDictionaryResolver.Instance
			};

			private readonly IJsonFormatterResolver _finalFormatter;
			private readonly ConcurrentDictionary<Type, object> _formatters = new ConcurrentDictionary<Type, object>();
			private readonly IConnectionSettingsValues _settings;

			internal InnerResolver(IConnectionSettingsValues settings)
			{
				_settings = settings;
				_finalFormatter =
					DynamicObjectResolver.Create(GetMapping, new Lazy<Func<string, string>>(() => settings.DefaultFieldNameInferrer), true);
			}

			public IJsonFormatter<T> GetFormatter<T>() =>
				(IJsonFormatter<T>)_formatters.GetOrAdd(typeof(T), type =>
				{
					foreach (var item in Resolvers)
					{
						var formatter = item.GetFormatter<T>();
						if (formatter != null)
							return formatter;
					}

					return _finalFormatter.GetFormatter<T>();
				});

			private IJsonProperty GetMapping(MemberInfo member)
			{
				// TODO: Skip calling this method for NEST and Elasticsearch.Net types, at the type level
				if (!_settings.PropertyMappings.TryGetValue(member, out var propertyMapping))
					propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

				var serializerMapping = _settings.PropertyMappingProvider?.CreatePropertyMapping(member);

				var nameOverride = propertyMapping?.Name ?? serializerMapping?.Name;

				var property = new JsonProperty(nameOverride);

				var overrideIgnore = propertyMapping?.Ignore ?? serializerMapping?.Ignore;
				if (overrideIgnore.HasValue)
					property.Ignore = overrideIgnore.Value;

				if (propertyMapping != null || serializerMapping != null)
					property.AllowPrivate = true;

				return property;
			}
		}
	}
}

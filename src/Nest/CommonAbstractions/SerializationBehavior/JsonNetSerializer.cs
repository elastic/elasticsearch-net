using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A JSON serializer that uses Json.NET for serialization
	/// </summary>
	public class JsonNetSerializer : IElasticsearchSerializer
	{
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);

		protected readonly ConcurrentDictionary<string, IPropertyMapping> Properties = new ConcurrentDictionary<string, IPropertyMapping>();
		private JsonSerializer _indentedSerializer;

		public JsonNetSerializer(IConnectionSettingsValues settings, Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier) :
			this(settings, null, settingsModifier) { }

		public JsonNetSerializer(IConnectionSettingsValues settings) : this(settings, null, null) { }

		/// <summary>
		/// this constructor is only here for stateful (de)serialization
		/// </summary>
		protected internal JsonNetSerializer(
			IConnectionSettingsValues settings,
			JsonConverter statefulConverter,
			Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier = null
		)
		{
			Settings = settings;
			var piggyBackState = statefulConverter == null ? null : new JsonConverterPiggyBackState { ActualJsonConverter = statefulConverter };
			// ReSharper disable once VirtualMemberCallInContructor
			ContractResolver = new ElasticContractResolver(Settings, ContractConverters) { PiggyBackState = piggyBackState };

			OverwriteDefaultSerializers(settingsModifier ?? ((s, csv) => { }));
		}

		/// <summary>
		/// The size of the buffer to use when writing the serialized request
		/// to the request stream
		/// </summary>
		// Performance tests as part of https://github.com/elastic/elasticsearch-net/issues/1899 indicate this
		// to be a good compromise buffer size for performance throughput and bytes allocated.
		protected virtual int BufferSize => 1024;

		protected virtual IList<Func<Type, JsonConverter>> ContractConverters => null;

		protected IConnectionSettingsValues Settings { get; }

		//TODO this internal smells
		internal JsonSerializer Serializer { get; private set; }

		/// <summary>
		/// Resolves JsonContracts for types
		/// </summary>
		private ElasticContractResolver ContractResolver { get; }

		public virtual IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			var memberInfoString = $"{memberInfo.DeclaringType?.FullName}.{memberInfo.Name}";
			if (Properties.TryGetValue(memberInfoString, out var mapping))
				return mapping;

			mapping = PropertyMappingFromAttributes(memberInfo);
			Properties.TryAdd(memberInfoString, mapping);
			return mapping;
		}

		public virtual T Deserialize<T>(Stream stream)
		{
			if (stream == null) return default(T);

			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = Serializer.Deserialize<T>(jsonTextReader);
				return t;
			}
		}

		public virtual Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//Json.NET does not support reading a stream asynchronously :(
			var result = Deserialize<T>(stream);
			return Task.FromResult(result);
		}

		public virtual void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serializer = formatting == SerializationFormatting.Indented
				? _indentedSerializer
				: Serializer;

			using (var writer = new StreamWriter(writableStream, ExpectedEncoding, BufferSize, true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		/// <summary>
		/// If you subclass JsonNetSerializer and want to apply state passed in the constructor, call this to
		/// overwrite the DefaultSerializers's JsonSerializerSettings and/or connectionSettings.
		/// </summary>
		/// <param name="settingsModifier">a delegate used to modify json serializer and connection settings</param>
		protected void OverwriteDefaultSerializers(Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier)
		{
			settingsModifier.ThrowIfNull(nameof(settingsModifier));
			var collapsed = CreateSettings(SerializationFormatting.None);
			var indented = CreateSettings(SerializationFormatting.Indented);
			settingsModifier(collapsed, Settings);
			settingsModifier(indented, Settings);

			Serializer = JsonSerializer.Create(collapsed);
			_indentedSerializer = JsonSerializer.Create(indented);
		}

		private static IPropertyMapping PropertyMappingFromAttributes(MemberInfo memberInfo)
		{
			var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>(true);
			var dataMember = memberInfo.GetCustomAttribute<DataMemberAttribute>(true);
			var ignoreProperty = memberInfo.GetCustomAttribute<JsonIgnoreAttribute>(true);
			if (jsonProperty == null && ignoreProperty == null && dataMember == null) return null;

			return new PropertyMapping { Name = jsonProperty?.PropertyName ?? dataMember?.Name, Ignore = ignoreProperty != null };
		}

		private JsonSerializerSettings CreateSettings(SerializationFormatting formatting)
		{
			var settings = new JsonSerializerSettings()
			{
				Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None,
				ContractResolver = ContractResolver,
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

			var contract = settings.ContractResolver as ElasticContractResolver;
			if (contract == null)
				throw new Exception($"NEST needs an instance of {nameof(ElasticContractResolver)} registered on Json.NET's JsonSerializerSettings");

			return settings;
		}
	}
}

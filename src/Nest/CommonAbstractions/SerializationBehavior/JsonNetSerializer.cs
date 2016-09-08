using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public class JsonNetSerializer : IElasticsearchSerializer
	{
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);


		protected IConnectionSettingsValues Settings { get; }
		protected ElasticContractResolver ContractResolver { get; }

		//todo this internal smells
		internal JsonSerializer Serializer => _defaultSerializer;

		private Dictionary<SerializationFormatting, JsonSerializer> _defaultSerializers;
		private JsonSerializer _defaultSerializer;

		[Obsolete("Use the connection settings constructor that takes an ISerializerFactory")]
		protected virtual void ModifyJsonSerializerSettings(JsonSerializerSettings settings) { }

		protected virtual IList<Func<Type, JsonConverter>> ContractConverters => null;

		public JsonNetSerializer(IConnectionSettingsValues settings, Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier) : this(settings, null, settingsModifier) { }

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
			this.Settings = settings;
			var piggyBackState = statefulConverter == null ? null : new JsonConverterPiggyBackState { ActualJsonConverter = statefulConverter };
			// ReSharper disable once VirtualMemberCallInContructor
			this.ContractResolver = new ElasticContractResolver(this.Settings, this.ContractConverters) { PiggyBackState = piggyBackState };

			OverwriteDefaultSerializers(settingsModifier ?? ((s, csv) => { }));
		}

		/// <summary>
		/// If you subclass JsonNetSerializer and want to apply state passed in the constructor call this to
		/// overwrite the DefaultSerializers's JsonSerializerSettings and/or connectionsettings.
		/// </summary>
		/// <param name="settingsModifier"></param>
		protected void OverwriteDefaultSerializers(Action<JsonSerializerSettings, IConnectionSettingsValues> settingsModifier)
		{
			settingsModifier.ThrowIfNull(nameof(settingsModifier));
			var collapsed = this.CreateSettings(SerializationFormatting.None);
			var indented = this.CreateSettings(SerializationFormatting.Indented);
			settingsModifier(collapsed, this.Settings);
			settingsModifier(indented, this.Settings);

			this._defaultSerializer = JsonSerializer.Create(collapsed);
			var indentedSerializer = JsonSerializer.Create(indented);
			this._defaultSerializers = new Dictionary<SerializationFormatting, JsonSerializer>
			{
				{ SerializationFormatting.None, this._defaultSerializer },
				{ SerializationFormatting.Indented, indentedSerializer }
			};

		}


		public virtual void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			var serializer = _defaultSerializers[formatting];
			using (var writer = new StreamWriter(writableStream, ExpectedEncoding, 8096, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, data);
				writer.Flush();
				jsonWriter.Flush();
			}
		}

		protected readonly ConcurrentDictionary<int, IPropertyMapping> Properties = new ConcurrentDictionary<int, IPropertyMapping>();

		public virtual IPropertyMapping CreatePropertyMapping(MemberInfo memberInfo)
		{
			IPropertyMapping mapping;
			if (Properties.TryGetValue(memberInfo.GetHashCode(), out mapping)) return mapping;
			mapping =  PropertyMappingFromAtrributes(memberInfo);
			this.Properties.TryAdd(memberInfo.GetHashCode(), mapping);
			return mapping;
		}

		private static IPropertyMapping PropertyMappingFromAtrributes(MemberInfo memberInfo)
		{
			var jsonProperty = memberInfo.GetCustomAttribute<JsonPropertyAttribute>(true);
			var ignoreProperty = memberInfo.GetCustomAttribute<JsonIgnoreAttribute>(true);
			if (jsonProperty == null && ignoreProperty == null) return null;
			return new PropertyMapping {Name = jsonProperty?.PropertyName, Ignore = ignoreProperty != null};
		}

		public virtual T Deserialize<T>(Stream stream)
		{
			if (stream == null) return default(T);
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var t = this._defaultSerializer.Deserialize(jsonTextReader, typeof(T));
				return (T)t;
			}
		}

		public virtual Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			//Json.NET does not support reading a stream asynchronously :(
			var result = this.Deserialize<T>(stream);
			return Task.FromResult<T>(result);
		}

		private JsonSerializerSettings CreateSettings(SerializationFormatting formatting)
		{
			var settings = new JsonSerializerSettings()
			{
				Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None,
				ContractResolver = this.ContractResolver,
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Ignore
			};

#pragma warning disable CS0618 // Type or member is obsolete
			this.ModifyJsonSerializerSettings(settings);
#pragma warning restore CS0618 // Type or member is obsolete

			var contract = settings.ContractResolver as ElasticContractResolver;
			if (contract == null) throw new Exception($"NEST needs an instance of {nameof(ElasticContractResolver)} registered on Json.NET's JsonSerializerSettings");

			return settings;
		}
	}
}

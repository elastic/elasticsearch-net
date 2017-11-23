using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Nest.JsonNetSerializer
{
	public abstract class JsonNetSourceSerializerBase : IElasticsearchSerializer
	{
		protected IElasticsearchSerializer BuiltinSerializer { get; }
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		protected virtual int BufferSize => 1024;

		private readonly JsonSerializer _serializer;
		private readonly JsonSerializer _collapsedSerializer;

		protected JsonNetSourceSerializerBase(IElasticsearchSerializer builtinSerializer)
		{
			BuiltinSerializer = builtinSerializer;
			_serializer = CreateSerializer(SerializationFormatting.Indented);
			_collapsedSerializer = CreateSerializer(SerializationFormatting.None);
		}

		protected abstract JsonSerializerSettings CreateJsonSerializerSettings();
		protected abstract IEnumerable<JsonConverter> CreateJsonConverters();
		protected virtual IContractResolver CreateContractResolver() => new DefaultContractResolver();

		private JsonSerializer CreateSerializer(SerializationFormatting formatting)
		{
			var s = CreateJsonSerializerSettings();
			var converters = CreateJsonConverters() ?? Enumerable.Empty<JsonConverter>();
			var contract = CreateContractResolver();
			s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
			s.ContractResolver = contract;
			s.Converters = converters.Concat(new List<JsonConverter>
			{
				new RevertBackToBuiltinSerializer(BuiltinSerializer)
			}).ToList();
			return JsonSerializer.Create(s);
		}

		public T Deserialize<T>(Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize<T>(jsonTextReader);
		}

		public object Deserialize(Type type, Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
				return _serializer.Deserialize(jsonTextReader, type);
		}

		public virtual async Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await JToken.LoadAsync(jsonTextReader, cancellationToken);
				return token.ToObject<T>(this._serializer);
			}
		}

		public virtual async Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var streamReader = new StreamReader(stream))
			using (var jsonTextReader = new JsonTextReader(streamReader))
			{
				var token = await JToken.LoadAsync(jsonTextReader, cancellationToken);
				return token.ToObject(type, this._serializer);
			}
		}

		public void Serialize(object data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
				(formatting == SerializationFormatting.Indented ? _serializer : _collapsedSerializer)
					.Serialize(jsonWriter, data);
		}

		//we still support net45 so Task.Completed is not available
		private static readonly Task CompletedTask = Task.FromResult(false);
		public Task SerializeAsync(object data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			//This makes no sense now but we need the async method on the interface in 6.x so we can start swapping this out
			//for an implementation that does make sense without having to wait for 7.x
			this.Serialize(data, stream, formatting);
			return CompletedTask;
		}
	}
}

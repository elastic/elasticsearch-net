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
using Tests.ClientConcepts.HighLevel.Caching;
using Tests.ClientConcepts.HighLevel.Serialization;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{
	public abstract class CustomJsonNetSourceSerializer : IElasticsearchSerializer
	{
		protected IElasticsearchSerializer BuiltinSerializer { get; }
		private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
		protected virtual int BufferSize => 1024;

		private readonly JsonSerializer _serializer;
		private readonly JsonSerializer _collapsedSerializer;

		protected CustomJsonNetSourceSerializer(IElasticsearchSerializer builtinSerializer)
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

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = new CancellationToken())
		{
			var o = this.Deserialize<T>(stream);
			return Task.FromResult(o);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = new CancellationToken())
		{
			var o = this.Deserialize(type, stream);
			return Task.FromResult(o);
		}

		public void Serialize(object data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var writer = new StreamWriter(stream, ExpectedEncoding, BufferSize, leaveOpen: true))
			using (var jsonWriter = new JsonTextWriter(writer))
				(formatting == SerializationFormatting.Indented ? _serializer : _collapsedSerializer)
					.Serialize(jsonWriter, data);
		}
	}
}

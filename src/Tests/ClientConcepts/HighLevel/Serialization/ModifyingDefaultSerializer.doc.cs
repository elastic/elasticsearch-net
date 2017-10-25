using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	public class ModifyingTheDefaultSerializer
	{
		public class CustomSerializer : IElasticsearchSerializer
		{
			private static readonly Encoding ExpectedEncoding = new UTF8Encoding(false);
			protected virtual int BufferSize => 1024;

			private readonly JsonSerializer _serializer;
			private readonly JsonSerializer _collapsedSerializer;

			public CustomSerializer(Func<JsonSerializerSettings> settings)
			{
				var contract = new DefaultContractResolver();
				_serializer = CreateSerializer(settings, contract, SerializationFormatting.Indented);
				_collapsedSerializer = CreateSerializer(settings, contract, SerializationFormatting.None);
			}

			private static JsonSerializer CreateSerializer(
				Func<JsonSerializerSettings> settings, IContractResolver contract, SerializationFormatting formatting)
			{
				var s = settings();
				s.Formatting = formatting == SerializationFormatting.Indented ? Formatting.Indented : Formatting.None;
				s.ContractResolver = contract;
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

		public class ADocument
		{
			public int Id { get; set; } = 1;
			public string Name { get; set; }
		}

		private object DefaultSerialized = new {id = 1};

		private Dictionary<string, object> IncludesNullAndSource = new Dictionary<string, object>
		{
			{"$type", $"{typeof(ADocument).FullName}, Tests"},
			{"Name", null},
			{"Id", 1},
		};

		private void CanAlterSource<T>(Func<IElasticClient, T> call, object usingDefaults, object withSourceSerializer)
			where T : IResponse
		{
			Expect(usingDefaults).FromRequest(call);

			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				NullValueHandling = NullValueHandling.Include
			};

			WithSourceSerializer(new CustomSerializer(() => settings))
				.Expect(withSourceSerializer)
				.FromRequest(call);
		}

		[U]
		public void IndexRequest()
		{
			CanAlterSource(
				r => r.Index(new ADocument()),
				usingDefaults: DefaultSerialized,
				withSourceSerializer: IncludesNullAndSource
			);
		}

		[U]
		public void CreateRequest()
		{
			CanAlterSource(
				r => r.Create(new ADocument()),
				usingDefaults: DefaultSerialized,
				withSourceSerializer: IncludesNullAndSource
			);
		}

		[U]
		public void UpdateRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.Update<ADocument>(doc, u => u
					.Doc(doc)
					.Upsert(doc)
				),
				usingDefaults: new
				{
					doc = DefaultSerialized,
					upsert = DefaultSerialized,
				},
				withSourceSerializer: new
				{
					doc = IncludesNullAndSource,
					upsert = IncludesNullAndSource,
				}
			);
		}

		[U]
		public void TermVectorRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.TermVectors<ADocument>(t => t
					.Document(doc)
				),
				usingDefaults: new {doc = DefaultSerialized},
				withSourceSerializer: new {doc = IncludesNullAndSource}
			);
		}

		private static IEnumerable<object> ExpectBulk(object document)
		{
			yield return new {delete = new {_index = "default-index", _type = "adocument", _id = "1"}};
			yield return new {index = new {_index = "default-index", _type = "adocument", _id = "1"}};
			yield return document;
			yield return new {create = new {_index = "default-index", _type = "adocument", _id = "1"}};
			yield return document;
			yield return new {update = new {_index = "default-index", _type = "adocument", _id = "1"}};
			yield return new {doc = document, upsert = document};
		}

		[U]
		public void BulkRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.Bulk(b => b
					.Delete<ADocument>(i => i.Document(doc))
					.Index<ADocument>(i => i.Document(doc))
					.Create<ADocument>(i => i.Document(doc))
					.Update<ADocument>(i => i.Doc(doc).Upsert(doc))
				),
				usingDefaults: ExpectBulk(DefaultSerialized).ToArray(),
				withSourceSerializer: ExpectBulk(IncludesNullAndSource).ToArray()
			);
		}
	}
}

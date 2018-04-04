using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.SourceSerializers;
using Tests.Framework.MockData;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	public class SendsUsingSourceSerializer
	{

		public class ADocument
		{
			public int Id { get; set; } = 1;
			public string Name { get; set; }
		}

		private readonly object DefaultSerialized = new {id = 1};

		private readonly Dictionary<string, object> IncludesNullAndType = new Dictionary<string, object>
		{
			{"$type", $"{typeof(ADocument).FullName}, Tests"},
			{"name", null},
			{"id", 1},
		};

		private class CustomSettingsSerializerBase : TestSourceSerializerBase
		{
			public CustomSettingsSerializerBase(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
				: base(builtinSerializer, connectionSettings) { }

			protected override JsonSerializerSettings CreateJsonSerializerSettings()
			{
				return new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All,
					NullValueHandling = NullValueHandling.Include
				};
			}

			protected override IEnumerable<JsonConverter> CreateJsonConverters()
			{
				foreach (var c in base.CreateJsonConverters()) yield return c;
				yield return new StringEnumConverter();
			}
		}

		private static void CanAlterSource<T>(Func<IElasticClient, T> call, object usingDefaults, object withSourceSerializer)
			where T : IResponse
		{
			Expect(usingDefaults).FromRequest(call);

			WithSourceSerializer((s, c) => new CustomSettingsSerializerBase(s, c))
				.Expect(withSourceSerializer)
				.FromRequest(call);
		}

		private static void Serializes<T>(T o, object usingDefaults, object withSourceSerializer)
		{
			SerializesDefault(o, usingDefaults);
			SerializesSourceSerializer(o, withSourceSerializer);
		}

		private static void SerializesSourceSerializer<T>(T o, object withSourceSerializer)
		{
			WithSourceSerializer((s, c) => new CustomSettingsSerializerBase(s, c))
				.Expect(withSourceSerializer)
				.WhenSerializing(o);
		}

		private static void SerializesDefault<T>(T o, object usingDefaults)
		{
			Expect(usingDefaults).WhenSerializing(o);
		}

		[U] public void IndexRequest()
		{
			CanAlterSource(
				r => r.IndexDocument(new ADocument()),
				usingDefaults: DefaultSerialized,
				withSourceSerializer: IncludesNullAndType
			);
		}

		[U] public void CreateRequest()
		{
			CanAlterSource(
				r => r.CreateDocument(new ADocument()),
				usingDefaults: DefaultSerialized,
				withSourceSerializer: IncludesNullAndType
			);
		}

		[U] public void UpdateRequest()
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
					doc = IncludesNullAndType,
					upsert = IncludesNullAndType,
				}
			);
		}

		[U] public void TermVectorRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.TermVectors<ADocument>(t => t
					.Document(doc)
				),
				usingDefaults: new {doc = DefaultSerialized},
				withSourceSerializer: new {doc = IncludesNullAndType}
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

		[U] public void BulkRequest()
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
				withSourceSerializer: ExpectBulk(IncludesNullAndType).ToArray()
			);
		}

		public object ExpectMultiTermVectors(object document)
		{
			return new
			{
				docs = new object[]
				{
					new {_index = "default-index", _type = "adocument", doc = document},
					new {_index = "default-index", _type = "adocument", doc = document}
				}
			};
		}

		[U] public void MultiTermVectorsRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.MultiTermVectors(b => b
					.Get<ADocument>(g => g.Document(doc))
					.Get<ADocument>(g => g.Document(doc))
				),
				usingDefaults: ExpectMultiTermVectors(DefaultSerialized),
				withSourceSerializer: ExpectMultiTermVectors(IncludesNullAndType)
			);
		}

		public enum SomeEnum
		{
			Value,
			[EnumMember(Value = "different")]
			AnotherValue
		}

		[U] public void TermQuery() =>
			SerializesEnumValue(new TermQuery { Field = Infer.Field<Project>(p=>p.Name), Value = SomeEnum.AnotherValue});

		[U] public void WildcardQuery() =>
			SerializesEnumValue(new WildcardQuery {Field = Infer.Field<Project>(p => p.Name), Value = SomeEnum.AnotherValue});

		[U] public void PrefixQuery() =>
			SerializesEnumValue(new PrefixQuery { Field = Infer.Field<Project>(p=>p.Name), Value = SomeEnum.AnotherValue});

		[U] public void SpanTermQueryInitializer() =>
			SerializesEnumValue(new SpanTermQuery { Field = Infer.Field<Project>(p=>p.Name), Value = SomeEnum.AnotherValue});

		[U] public void SpanTermQueryFluent() =>
			SerializesEnumValue<ISpanTermQuery>(new SpanTermQueryDescriptor<Project>().Field(p=>p.Name).Value(SomeEnum.AnotherValue));

		private static void SerializesEnumValue<T>(T query)
		{
			Serializes(query,
				usingDefaults: new {name = new {value = 1}},
				withSourceSerializer: new {name = new {value = "different"}}
			);
		}
	}
}

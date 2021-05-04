// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Core.Client.Serializers;
using Tests.Domain;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	public class SendsUsingSourceSerializer
	{
		public enum SomeEnum
		{
			Value,

			[EnumMember(Value = "different")]
			AnotherValue
		}

		private readonly object _defaultSerialized = new { id = 1 };

		private readonly Dictionary<string, object> _includesNullAndType = new Dictionary<string, object>
		{
			{ "$type", $"{typeof(ADocument).FullName}, Tests" },
			{ "name", null },
			{ "id", 1 },
		};

		private static void CanAlterSource<T>(Func<IElasticClient, T> call, object usingDefaults, object withSourceSerializer)
			where T : IResponse
		{
			Expect(usingDefaults).FromRequest(call);

			WithSourceSerializer((s, c) => new CustomSettingsSerializerBase(s, c))
				.Expect(withSourceSerializer, true)
				.FromRequest(call);
		}

		private static void Serializes<T>(T o, object usingDefaults, object withSourceSerializer)
		{
			SerializesDefault(o, usingDefaults);
			SerializesSourceSerializer(o, withSourceSerializer);
		}

		private static void SerializesSourceSerializer<T>(T o, object withSourceSerializer) =>
			WithSourceSerializer((s, c) => new CustomSettingsSerializerBase(s, c))
				.Expect(withSourceSerializer, true)
				.WhenSerializing(o);

		private static void SerializesDefault<T>(T o, object usingDefaults) => Expect(usingDefaults).WhenSerializing(o);

		[U] public void IndexRequest() => CanAlterSource(
			r => r.IndexDocument(new ADocument()),
			_defaultSerialized,
			_includesNullAndType
		);

		[U] public void CreateRequest() => CanAlterSource(
			r => r.CreateDocument(new ADocument()),
			_defaultSerialized,
			_includesNullAndType
		);

		[U] public void UpdateRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.Update<ADocument>(doc, u => u
					.Doc(doc)
					.Upsert(doc)
				),
				new
				{
					doc = _defaultSerialized,
					upsert = _defaultSerialized,
				},
				new
				{
					doc = _includesNullAndType,
					upsert = _includesNullAndType,
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
				new { doc = _defaultSerialized },
				new { doc = _includesNullAndType }
			);
		}

		private static IEnumerable<object> ExpectBulk(object document)
		{
			yield return new { delete = new { _index = "default-index", _id = "1" } };
			yield return new { index = new { _index = "default-index",  _id = "1" } };
			yield return document;
			yield return new { create = new { _index = "default-index", _id = "1" } };
			yield return document;
			yield return new { update = new { _index = "default-index", _id = "1" } };
			yield return new { doc = document, upsert = document };
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
				ExpectBulk(_defaultSerialized).ToArray(),
				ExpectBulk(_includesNullAndType).ToArray()
			);
		}

		public object ExpectMultiTermVectors(object document) => new
		{
			docs = new object[]
			{
				new { _index = "default-index", doc = document },
				new { _index = "default-index", doc = document }
			}
		};

		[U] public void MultiTermVectorsRequest()
		{
			var doc = new ADocument();
			CanAlterSource(
				r => r.MultiTermVectors(b => b
					.Documents<ADocument>(g => g.Document(doc))
					.Documents<ADocument>(g => g.Document(doc))
				),
				ExpectMultiTermVectors(_defaultSerialized),
				ExpectMultiTermVectors(_includesNullAndType)
			);
		}

		[U] public void TermQuery() =>
			SerializesEnumValue<ITermQuery>(new TermQuery { Field = Infer.Field<Project>(p => p.Name), Value = SomeEnum.AnotherValue });

		[U] public void TermsQuery() =>
			Serializes<ITermsQuery>(new TermsQuery { Field = Infer.Field<Project>(p => p.Name), Terms = new object[] { SomeEnum.AnotherValue } },
				new { name = new[] { 1 } },
				new { name = new[] { "different" } }
			);

		[U] public void WildcardQuery() =>
			SerializesEnumValue<IWildcardQuery>(new WildcardQuery { Field = Infer.Field<Project>(p => p.Name), Value = SomeEnum.AnotherValue });

		[U] public void PrefixQuery() =>
			SerializesEnumValue<IPrefixQuery>(new PrefixQuery { Field = Infer.Field<Project>(p => p.Name), Value = SomeEnum.AnotherValue });

		[U] public void SpanTermQueryInitializer() =>
			SerializesEnumValue<ISpanTermQuery>(new SpanTermQuery { Field = Infer.Field<Project>(p => p.Name), Value = SomeEnum.AnotherValue });

		[U] public void SpanTermQueryFluent() =>
			SerializesEnumValue<ISpanTermQuery>(new SpanTermQueryDescriptor<Project>().Field(p => p.Name).Value(SomeEnum.AnotherValue));

		private static void SerializesEnumValue<T>(T query) => Serializes(query,
			new { name = new { value = 1 } },
			new { name = new { value = "different" } }
		);

		public class ADocument
		{
			public int Id { get; set; } = 1;
			public string Name { get; set; }
		}

		private class CustomSettingsSerializerBase : TestSourceSerializerBase
		{
			public CustomSettingsSerializerBase(ITransportSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
				: base(builtinSerializer, connectionSettings) { }

			protected override JsonSerializerSettings CreateJsonSerializerSettings() => new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				NullValueHandling = NullValueHandling.Include
			};

			protected override IEnumerable<JsonConverter> CreateJsonConverters()
			{
				foreach (var c in base.CreateJsonConverters()) yield return c;

				yield return new StringEnumConverter();
			}
		}
	}
}

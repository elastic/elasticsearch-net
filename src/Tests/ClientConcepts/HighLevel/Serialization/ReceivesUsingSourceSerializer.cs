using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.SourceSerializers;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	public class ReceivesUsingSourceSerializer
	{
		public class ADocument
		{
			public int Id { get; set; } = 1;
			public string Name { get; set; }
		}

		private object DefaultSerialized = new {id = 1};

		private Dictionary<string, object> IncludesNullAndType = new Dictionary<string, object>
		{
			{"$type", $"{typeof(ADocument).FullName}, Tests"},
			{"Name", null},
			{"Id", 1},
		};

		private static void CanAlterSource<T>(Func<IElasticClient, T> call, object usingDefaults, object withSourceSerializer)
			where T : IResponse
		{
			Expect(usingDefaults).FromRequest(call);

			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All,
				NullValueHandling = NullValueHandling.Include
			};

			WithSourceSerializer(new CustomSourceSerializer(() => settings))
				.Expect(withSourceSerializer)
				.FromRequest(call);
		}

		[U] public void IndexRequest()
		{
			CanAlterSource(
				r => r.Index(new ADocument()),
				usingDefaults: DefaultSerialized,
				withSourceSerializer: IncludesNullAndType
			);
		}

		[U] public void CreateRequest()
		{
			CanAlterSource(
				r => r.Create(new ADocument()),
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
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VerifyXunit;

namespace Tests.Serialization.Bulk
{
	[UsesVerify]
	public class BulkRequestOperationsSerializationTests
	{
		[U]
		public async Task BulkRequestWithIndexOperations_ObjectInitializer_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var request = new BulkRequest("index-from-request")
			{
				Operations = new BulkOperationsCollection
				{
					new BulkIndexOperation<Inferrable>(Inferrable.Instance),
					new BulkIndexOperation<Inferrable>(Inferrable.Instance)
					{
						Id = "OverriddenId",
						Routing = "OverriddenRoute",
						Index = "overridden-index",
						Pipeline = "my-pipeline",
						RequireAlias = false,
						Version = 1,
						VersionType = VersionType.External,
						DynamicTemplates = new System.Collections.Generic.Dictionary<string, string>{ { "t1", "v1" } }
					},
					new BulkIndexOperation<NonInferrable>(NonInferrable.Instance, true),
					new BulkIndexOperation<NonInferrable>(NonInferrable.Instance, index: null),
					new BulkIndexOperation<NonInferrable>(NonInferrable.Instance) { Index = "configured-index" },
					new BulkIndexOperation<NonInferrable>(NonInferrable.Instance)
					{
						Id = "ConfiguredId",
						Routing = "ConfiguredRoute",
						Index = "configured-index",
						IfPrimaryTerm = 100,
						IfSequenceNumber = 10,
						Pipeline = "my-pipeline",
						RequireAlias = false,
						DynamicTemplates = new System.Collections.Generic.Dictionary<string, string>{ { "t1", "v1" } }
					}
				}
			};

			await request.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithDeleteOperations_ObjectInitializer_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var request = new BulkRequest
			{
				Operations = new BulkOperationsCollection
				{
					new BulkDeleteOperation("123"),
					new BulkDeleteOperation("123")
					{
						Routing = "ConfiguredRoute",
						Index = "configured-index",
						RequireAlias = false,
						Version = 1,
						VersionType = VersionType.External
					},
					new BulkDeleteOperation<Inferrable>(Inferrable.Instance),
					new BulkDeleteOperation<Inferrable>(Inferrable.Instance)
					{
						Id = "OverriddenId",
						Routing = "OverriddenRoute",
						Index = "overridden-index",
						IfPrimaryTerm = 100,
						IfSequenceNumber = 10,
						RequireAlias = false,
					}
				}
			};

			await request.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithCreateOperations_ObjectInitializer_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var request = new BulkRequest
			{
				Operations = new BulkOperationsCollection
				{
					new BulkCreateOperation<Inferrable>(Inferrable.Instance),
					new BulkCreateOperation<Inferrable>(Inferrable.Instance)
					{
						Id = "OverriddenId",
						Routing = "OverriddenRoute",
						Index = "overridden-index",
						Pipeline = "my-pipeline",
						RequireAlias = false,
						Version = 1,
						VersionType = VersionType.External,
						DynamicTemplates = new System.Collections.Generic.Dictionary<string, string>{ { "t1", "v1" } }
					},
					new BulkCreateOperation<NonInferrable>(NonInferrable.Instance, true),
					new BulkCreateOperation<NonInferrable>(NonInferrable.Instance, index: null),
					new BulkCreateOperation<NonInferrable>(NonInferrable.Instance) { Index = "configured-index" },
					new BulkCreateOperation<NonInferrable>(NonInferrable.Instance)
					{
						Id = "ConfiguredId",
						Routing = "ConfiguredRoute",
						Index = "configured-index",
						IfPrimaryTerm = 100,
						IfSequenceNumber = 10,
						Pipeline = "my-pipeline",
						RequireAlias = false,
						DynamicTemplates = new System.Collections.Generic.Dictionary<string, string>{ { "t1", "v1" } }
					}
				}
			};

			await request.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithUpdateOperations_ObjectInitializer_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var request = new BulkRequest
			{
				Operations = new BulkOperationsCollection
				{
					BulkUpdateOperationFactory.WithScript("123", "test-index", new StoredScriptId("my-script-id")
					{
						Params = new Dictionary<string, object> { { "param1", "paramvalue1" } }
					}),
					BulkUpdateOperationFactory.WithScript("123", new InlineScript("ctx._source.counter += params.param1")
					{
						Language = BuiltinScriptLanguage.Painless,
						Params = new Dictionary<string, object>{ { "param1", 1 } },
						Options = new Dictionary<string, string>{ { "option1", "optionvalue1" } }
					}),
					BulkUpdateOperationFactory.WithScript("123", new InlineScript("ctx._source.counter += params.param1")
					{
						Language = BuiltinScriptLanguage.Painless,
						Params = new Dictionary<string, object>{ { "param1", 1 } },
						Options = new Dictionary<string, string>{ { "option1", "optionvalue1" } }
					}, Inferrable.Instance),
					BulkUpdateOperationFactory.WithScript("123", "configured-index", new InlineScript("ctx._source.counter += params.param1")
					{
						Language = BuiltinScriptLanguage.Painless,
						Params = new Dictionary<string, object>{ { "param1", 1 } },
						Options = new Dictionary<string, string>{ { "option1", "optionvalue1" } }
					}, Inferrable.Instance),
					// TODO - Partial
					// TODO - Full operation
				}
			};

			await request.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithIndexOperations_Descriptor_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var fluentRequest = new BulkRequestDescriptor(b => b
				.Index(Inferrable.Instance)
				.Index(Inferrable.Instance, i => i
					.Id("OverriddenId")
					.Routing("OverriddenRoute")
					.Index("overridden-index")
					.Pipeline("my-pipeline")
					.RequireAlias(false)
					.Version(1)
					.VersionType(VersionType.External)
					.DynamicTemplates(d => d.Add("t1","v1")))
				.Index(NonInferrable.Instance, true)
				.Index(NonInferrable.Instance, index: null)
				.Index(NonInferrable.Instance, "configured-index")
				.Index(NonInferrable.Instance, i => i
					.Id("ConfiguredId")
					.Routing("ConfiguredRoute")
					.Index("configured-index")
					.IfPrimaryTerm(100)
					.IfSequenceNumber(10)
					.Pipeline("my-pipeline")
					.RequireAlias(false)
					.DynamicTemplates(d => d.Add("t1", "v1"))));

			await fluentRequest.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithDeleteOperations_Descriptor_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var fluentRequest = new BulkRequestDescriptor(b => b
				.Delete("123")
				.Delete("123", i => i
					.Id("ConfiguredId")
					.Routing("ConfiguredRoute")
					.Index("configured-index")
					.IfPrimaryTerm(100)
					.IfSequenceNumber(10)
					.RequireAlias(false)
					.Version(1)
					.VersionType(VersionType.External))
				.Delete(Inferrable.Instance)
				.Delete(Inferrable.Instance, i => i
					.Id("OverriddenId")
					.Routing("OverriddenRoute")
					.Index("overridden-index")
					.IfPrimaryTerm(100)
					.IfSequenceNumber(10)
					.RequireAlias(false)
					.Version(1)
					.VersionType(VersionType.External))
				);

			await fluentRequest.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		[U]
		public async Task BulkRequestWithCreateOperations_Descriptor_SerializesCorrectly()
		{
			var settings = new ElasticsearchClientSettings();
			settings.DefaultMappingFor<Inferrable>(m => m.IdProperty(p => p.Name).RoutingProperty(r => r.Name).IndexName("test-index"));

			var ms = new MemoryStream();

			var fluentRequest = new BulkRequestDescriptor(b => b
				.Create(Inferrable.Instance)
				.Create(Inferrable.Instance, i => i
					.Id("OverriddenId")
					.Routing("OverriddenRoute")
					.Index("overridden-index")
					.Pipeline("my-pipeline")
					.RequireAlias(false)
					.Version(1)
					.VersionType(VersionType.External)
					.DynamicTemplates(d => d.Add("t1", "v1")))
				.Create(NonInferrable.Instance, true)
				.Create(NonInferrable.Instance, index: null)
				.Create(NonInferrable.Instance, "configured-index")
				.Create(NonInferrable.Instance, i => i
					.Id("ConfiguredId")
					.Routing("ConfiguredRoute")
					.Index("configured-index")
					.IfPrimaryTerm(100)
					.IfSequenceNumber(10)
					.Pipeline("my-pipeline")
					.RequireAlias(false)
					.DynamicTemplates(d => d.Add("t1", "v1"))));

			await fluentRequest.SerializeAsync(ms, settings);

			ms.Position = 0;
			var reader = new StreamReader(ms);
			var ndjson = reader.ReadToEnd();

			// NOTE: The verified output from this test should be valid if pasted into a bulk API call via Kibana.
			// Although it may fail due to version concurrency not being the expected values it should be parsed correctly.
			// Use: POST configured-index/_bulk
			await Verifier.Verify(ndjson);
		}

		private class Inferrable
		{
			public static Inferrable Instance = new() { Name = "TestName" };

			public string Name { get; set; }
		}

		private class NonInferrable
		{
			public static NonInferrable Instance = new() { Forename = "Steve" };

			public string Forename { get; set; }
		}
	}
}

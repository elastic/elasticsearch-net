using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class IndicesTest : IntegrationTests 
	{
		protected void TestDefaultAssertions(QueryResponse<ElasticSearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid);
			Assert.NotNull(queryResponse.ConnectionStatus);
			Assert.Null(queryResponse.ConnectionStatus.Error);
			Assert.True(queryResponse.Total > 0, "No hits");
			Assert.True(queryResponse.Documents.Any());
			Assert.True(queryResponse.Documents.Count() > 0);
			Assert.True(queryResponse.Shards.Total > 0);
			Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total);
			Assert.True(queryResponse.Shards.Failed == 0);

		}

		[Test]
		public void GetIndexSettingsSimple()
		{
			var r = this._client.GetIndexSettings();
			Assert.True(r.IsValid);
			Assert.NotNull(r.Settings);
			Assert.GreaterOrEqual(r.Settings.NumberOfReplicas, 0);
			Assert.GreaterOrEqual(r.Settings.NumberOfShards, 1);
		}

		[Test]
		public void GetIndexSettingsComplex()
		{
			var index = Guid.NewGuid().ToString();
			var settings = new IndexSettings();
			settings.NumberOfReplicas = 4;
			settings.NumberOfShards = 8;
			settings.Analysis.Analyzers.Add("snowball", new SnowballAnalyzer { Language = "English" });
			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			typeMapping.TypeNameMarker = index;
			settings.Mappings.Add(typeMapping);

			settings.Add("merge.policy.merge_factor", "10");

			var createResponse = this._client.CreateIndex(index, settings);

			var r = this._client.GetIndexSettings(index);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Settings);
			Assert.AreEqual(r.Settings.NumberOfReplicas, 4);
			Assert.AreEqual(r.Settings.NumberOfShards, 8);
			Assert.Greater(r.Settings.Count(), 0);
			Assert.True(r.Settings.ContainsKey("merge.policy.merge_factor"));

			this._client.DeleteIndex(index);
		}
		[Test]
		public void UpdateSettingsSimple()
		{
			var index = Guid.NewGuid().ToString();
			var client = this._client;
			var settings = new IndexSettings();
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Add("refresh_interval", "1s");
			settings.Add("search.slowlog.threshold.fetch.warn", "1s");
			client.CreateIndex(index, settings);

			settings["refresh_interval"] = "-1";
			settings["search.slowlog.threshold.fetch.warn"] = "5s";

			var r = this._client.UpdateSettings(index, settings);

			Assert.True(r.IsValid);
			Assert.True(r.OK);
			var getResponse = this._client.GetIndexSettings(index);
			Assert.AreEqual(getResponse.Settings["refresh_interval"], "-1");
			Assert.AreEqual(getResponse.Settings["search.slowlog.threshold.fetch.warn"], "1s");

			this._client.DeleteIndex(index);
		}




		[Test]
		public void CreateIndex()
		{
			var client = this._client;
			var typeMapping = this._client.GetMapping(ElasticsearchConfiguration.DefaultIndex, "elasticsearchprojects");
			typeMapping.TypeNameMarker = "mytype";
			var settings = new IndexSettings();
			settings.Mappings.Add(typeMapping);
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Analysis.Analyzers.Add("snowball", new SnowballAnalyzer { Language = "English" });

			var indexName = Guid.NewGuid().ToString();
			var response = client.CreateIndex(indexName, settings);

			Assert.IsTrue(response.IsValid);
			Assert.IsTrue(response.OK);

			Assert.IsNotNull(this._client.GetMapping(indexName, "mytype"));

			var deleteResponse = client.DeleteIndex(indexName);

			Assert.IsTrue(deleteResponse.IsValid);
			Assert.IsTrue(deleteResponse.OK);

		}

		[Test]
		public void CreateIndexUsingDescriptor()
		{
			var index = ElasticsearchConfiguration.DefaultIndex + "_clone";
			if (this._client.IndexExists(index).Exists)
				_client.DeleteIndex(index);

			var result = this._client.CreateIndex(index, c => c
				.NumberOfReplicas(1)
				.NumberOfShards(1)
				.Settings(s => s
					.Add("compound_format", true)
					.Add("term_index_interval", 128)
					.Add("search.slowlog.threshold.query.warn", "2s")
				)
				.AddMapping<ElasticSearchProject>(m => m
					.MapFromAttributes()
					.NumericDetection()
					.DateDetection()
				)
				.AddMapping<Person>(m => m
					.MapFromAttributes()
				)
				.Analysis(a=>a
					.Analyzers(an=>an
						.Add("standard", new StandardAnalyzer()
						{
							StopWords = new [] { "stop1", "stop2" }
						})
					)
					.Tokenizers(t=>t
						.Add("myTokenizer", new StandardTokenizer { MaximumTokenLength = 900 })
					)
					.TokenFilters(t => t
						.Add("myTokenFilter1", new StopTokenFilter { Stopwords = new [] { "stop1", "stop2" } })
					)
					.CharFilters(t => t
						.Add("htmlFilter", new HtmlStripCharFilter())
					)
				)
			);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.ConnectionStatus.Should().NotBeNull();
		}


		[Test]
		public void PutMapping()
		{
			var fieldName = Guid.NewGuid().ToString();
			var mapping = this._client.GetMapping<ElasticSearchProject>();
			var property = new StringMapping
			{
				Index = FieldIndexOption.not_analyzed
			};
			mapping.Properties.Add(fieldName, property);

			var response = this._client.Map(mapping);

			Assert.IsTrue(response.IsValid, response.ConnectionStatus.ToString());
			Assert.IsTrue(response.OK, response.ConnectionStatus.ToString());

			mapping = this._client.GetMapping<ElasticSearchProject>();
			Assert.IsNotNull(mapping.Properties.ContainsKey(fieldName));
		}


		[Test]
		public void CreateIndexMultiFieldMap()
		{
			var client = this._client;

			var typeMapping = new RootObjectMapping();
			typeMapping.TypeNameMarker = Guid.NewGuid().ToString("n");
			var property = new MultiFieldMapping();

			var primaryField = new StringMapping()
			{
				Index = FieldIndexOption.not_analyzed
			};

			var analyzedField = new StringMapping()
			{
				Index = FieldIndexOption.analyzed
			};

			property.Fields.Add("name", primaryField);
			property.Fields.Add("name_analyzed", analyzedField);
			typeMapping.Properties = typeMapping.Properties ?? new Dictionary<string, IElasticType>();
			typeMapping.Properties.Add("name", property);

			var settings = new IndexSettings();
			settings.Mappings.Add(typeMapping);
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Analysis.Analyzers.Add("snowball", new SnowballAnalyzer { Language = "English" });

			var indexName = Guid.NewGuid().ToString();
			var response = client.CreateIndex(indexName, settings);

			Assert.IsTrue(response.IsValid);
			Assert.IsTrue(response.OK);

			var typeName = typeMapping.TypeNameMarker.Resolve(this._settings);
			Assert.IsNotNull(this._client.GetMapping(indexName, typeName));

			var deleteResponse = client.DeleteIndex(indexName);

			Assert.IsTrue(deleteResponse.IsValid);
			Assert.IsTrue(deleteResponse.OK);

		}
	}
}

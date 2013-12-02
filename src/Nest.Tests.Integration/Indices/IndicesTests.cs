using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Domain.Settings;
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
			settings.Analysis.Analyzers.Add("standard", new StandardAnalyzer { StopWords = new[]{"word1", "word2"}});
			settings.Analysis.Analyzers.Add("swedishlanguage", new LanguageAnalyzer(Language.Swedish) { StopWords = new[] { "word1", "word2" }, StemExclusionList = new[] { "stem1", "stem2" } });

			settings.Analysis.CharFilters.Add("char1", new HtmlStripCharFilter());
			settings.Analysis.CharFilters.Add("char2", new MappingCharFilter{ Mappings = new []{"ph=>f", "qu=>q"}});

			settings.Analysis.TokenFilters.Add("tokenfilter1", new EdgeNGramTokenFilter());
			settings.Analysis.TokenFilters.Add("tokenfilter2", new SnowballTokenFilter());

			settings.Analysis.Tokenizers.Add("token1", new KeywordTokenizer());
			settings.Analysis.Tokenizers.Add("token2", new PathHierarchyTokenizer());

			settings.Similarity = new SimilaritySettings();
			var dfr = new CustomSimilaritySettings("test1", "DFR");
			dfr.SimilarityParameters.Add("basic_model", "g");
			dfr.SimilarityParameters.Add("after_effect", "l");
			dfr.SimilarityParameters.Add("normalization", "h2");
			dfr.SimilarityParameters.Add("normalization.h2.c", 3);
			settings.Similarity.CustomSimilarities.Add(dfr);

			var ib = new CustomSimilaritySettings("test2", "IB");
			ib.SimilarityParameters.Add("distribution", "spl");
			ib.SimilarityParameters.Add("lambda", "ttf");
			ib.SimilarityParameters.Add("normalization", "h1");
			settings.Similarity.CustomSimilarities.Add(ib);

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
			Assert.AreEqual("10", r.Settings["merge.policy.merge_factor"]);
			
			Assert.AreEqual(3, r.Settings.Analysis.Analyzers.Count);
			{ // assert analyzers
				Assert.True(r.Settings.Analysis.Analyzers.ContainsKey("snowball"));
				var snoballAnalyser = r.Settings.Analysis.Analyzers["snowball"] as SnowballAnalyzer;
				Assert.NotNull(snoballAnalyser);
				Assert.AreEqual("English", snoballAnalyser.Language);

				Assert.True(r.Settings.Analysis.Analyzers.ContainsKey("standard"));
				var standardAnalyser = r.Settings.Analysis.Analyzers["standard"] as StandardAnalyzer;
				Assert.NotNull(standardAnalyser);
				Assert.NotNull(standardAnalyser.StopWords);
				Assert.AreEqual(2, standardAnalyser.StopWords.Count());
				Assert.True(standardAnalyser.StopWords.Contains("word1"));
				Assert.True(standardAnalyser.StopWords.Contains("word2"));

				Assert.True(r.Settings.Analysis.Analyzers.ContainsKey("swedishlanguage"));
				var languageAnalyser = r.Settings.Analysis.Analyzers["swedishlanguage"] as LanguageAnalyzer;
				Assert.NotNull(languageAnalyser);
				Assert.AreEqual(Language.Swedish.ToString().ToLower(), languageAnalyser.Type);
				Assert.NotNull(languageAnalyser.StopWords);
				Assert.AreEqual(2, languageAnalyser.StopWords.Count());
				Assert.True(languageAnalyser.StopWords.Contains("word1"));
				Assert.True(languageAnalyser.StopWords.Contains("word2"));
				Assert.AreEqual(2, languageAnalyser.StemExclusionList.Count());
				Assert.True(languageAnalyser.StemExclusionList.Contains("stem1"));
				Assert.True(languageAnalyser.StemExclusionList.Contains("stem2"));
			}

			Assert.AreEqual(2, r.Settings.Analysis.CharFilters.Count);
			{ // assert char filters
				Assert.True(r.Settings.Analysis.CharFilters.ContainsKey("char1"));
				var filter1 = r.Settings.Analysis.CharFilters["char1"] as HtmlStripCharFilter;
				Assert.NotNull(filter1);
				Assert.True(r.Settings.Analysis.CharFilters.ContainsKey("char2"));
				var filter2 = r.Settings.Analysis.CharFilters["char2"] as MappingCharFilter;
				Assert.NotNull(filter2);
				Assert.AreEqual(2, filter2.Mappings.Count());
				Assert.True(filter2.Mappings.Contains("ph=>f"));
				Assert.True(filter2.Mappings.Contains("qu=>q"));
			}

			Assert.AreEqual(2, r.Settings.Analysis.TokenFilters.Count);
			{ // assert token filters
				Assert.True(r.Settings.Analysis.TokenFilters.ContainsKey("tokenfilter1"));
				var filter1 = r.Settings.Analysis.TokenFilters["tokenfilter1"] as EdgeNGramTokenFilter;
				Assert.NotNull(filter1);
				Assert.True(r.Settings.Analysis.TokenFilters.ContainsKey("tokenfilter2"));
				var filter2 = r.Settings.Analysis.TokenFilters["tokenfilter2"] as SnowballTokenFilter;
				Assert.NotNull(filter2);
			}

			Assert.AreEqual(2, r.Settings.Analysis.Tokenizers.Count);
			{ // assert tokenizers
				Assert.True(r.Settings.Analysis.Tokenizers.ContainsKey("token1"));
				var tokenizer1 = r.Settings.Analysis.Tokenizers["token1"] as KeywordTokenizer;
				Assert.NotNull(tokenizer1);
				Assert.True(r.Settings.Analysis.Tokenizers.ContainsKey("token2"));
				var tokenizer2 = r.Settings.Analysis.Tokenizers["token2"] as PathHierarchyTokenizer;
				Assert.NotNull(tokenizer2);
			}


			Assert.NotNull(r.Settings.Similarity);
			Assert.NotNull(r.Settings.Similarity.CustomSimilarities);
			Assert.AreEqual(2, r.Settings.Similarity.CustomSimilarities.Count);
			{ // assert similarity
				var similarity1 = r.Settings.Similarity.CustomSimilarities.FirstOrDefault(x => x.Name.Equals("test1", StringComparison.InvariantCultureIgnoreCase));
				Assert.NotNull(similarity1);
				Assert.AreEqual("DFR", similarity1.Type);
				Assert.AreEqual(4, similarity1.SimilarityParameters.Count);
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("basic_model") && x.Value.ToString().Equals("g")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("after_effect") && x.Value.ToString().Equals("l")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("normalization") && x.Value.ToString().Equals("h2")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("normalization.h2.c") && x.Value.ToString().Equals("3")));

				var similarity2 = r.Settings.Similarity.CustomSimilarities.FirstOrDefault(x => x.Name.Equals("test2", StringComparison.InvariantCultureIgnoreCase));
				Assert.NotNull(similarity2);
				Assert.AreEqual("IB", similarity2.Type);
				Assert.AreEqual(3, similarity2.SimilarityParameters.Count);
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("distribution") && x.Value.ToString().Equals("spl")));
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("lambda") && x.Value.ToString().Equals("ttf")));
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("normalization") && x.Value.ToString().Equals("h1")));
			}
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

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Domain.Settings;
using Nest.Resolvers;
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
		protected void TestDefaultAssertions(SearchResponse<ElasticsearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid);
			Assert.NotNull(queryResponse.ConnectionStatus);
			Assert.Null(queryResponse.ConnectionStatus.OriginalException);
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
			var r = this.Client.GetIndexSettings(i=>i.Index<ElasticsearchProject>());
			Assert.True(r.IsValid);
			Assert.NotNull(r.IndexSettings);
			Assert.GreaterOrEqual(r.IndexSettings.NumberOfReplicas, 0);
			Assert.GreaterOrEqual(r.IndexSettings.NumberOfShards, 1);
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
            settings.Analysis.CharFilters.Add("char3", new PatternReplaceCharFilter { Pattern = "sample(.*)", Replacement = "replacedSample $1" });
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

			var typeMappingResult = this.Client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects"));
			var typeMapping = typeMappingResult.Mapping;
			typeMapping.Name = index;
			settings.Mappings.Add(typeMapping);

			settings.Settings.Add("merge.policy.merge_factor", "10");

			var createResponse = this.Client.CreateIndex(index, i=>i.InitializeUsing(settings));

			var r = this.Client.GetIndexSettings(i=>i.Index(index));
			Assert.True(r.IsValid);
			Assert.NotNull(r.IndexSettings.Settings.Count);
			Assert.AreEqual(r.IndexSettings.NumberOfReplicas, 4);
			Assert.AreEqual(r.IndexSettings.NumberOfShards, 8);
			//Assert.Greater(r.Setttings, 0);
			Assert.NotNull(r.IndexSettings._.merge.policy.merge_factor);
			Assert.AreEqual(10, r.IndexSettings._.merge.policy.merge_factor);
			
			Assert.AreEqual(3, r.IndexSettings.Analysis.Analyzers.Count);
			{ // assert analyzers
				Assert.True(r.IndexSettings.Analysis.Analyzers.ContainsKey("snowball"));
				var snoballAnalyser = r.IndexSettings.Analysis.Analyzers["snowball"] as SnowballAnalyzer;
				Assert.NotNull(snoballAnalyser);
				Assert.AreEqual("English", snoballAnalyser.Language);

				Assert.True(r.IndexSettings.Analysis.Analyzers.ContainsKey("standard"));
				var standardAnalyser = r.IndexSettings.Analysis.Analyzers["standard"] as StandardAnalyzer;
				Assert.NotNull(standardAnalyser);
				Assert.NotNull(standardAnalyser.StopWords);
				Assert.AreEqual(2, standardAnalyser.StopWords.Count());
				Assert.True(standardAnalyser.StopWords.Contains("word1"));
				Assert.True(standardAnalyser.StopWords.Contains("word2"));

				Assert.True(r.IndexSettings.Analysis.Analyzers.ContainsKey("swedishlanguage"));
				var languageAnalyser = r.IndexSettings.Analysis.Analyzers["swedishlanguage"] as LanguageAnalyzer;
				Assert.NotNull(languageAnalyser);
				Assert.AreEqual(Language.Swedish.ToString().ToLowerInvariant(), languageAnalyser.Type);
				Assert.NotNull(languageAnalyser.StopWords);
				Assert.AreEqual(2, languageAnalyser.StopWords.Count());
				Assert.True(languageAnalyser.StopWords.Contains("word1"));
				Assert.True(languageAnalyser.StopWords.Contains("word2"));
				Assert.AreEqual(2, languageAnalyser.StemExclusionList.Count());
				Assert.True(languageAnalyser.StemExclusionList.Contains("stem1"));
				Assert.True(languageAnalyser.StemExclusionList.Contains("stem2"));
			}

			Assert.AreEqual(3, r.IndexSettings.Analysis.CharFilters.Count);
			{ // assert char filters
				Assert.True(r.IndexSettings.Analysis.CharFilters.ContainsKey("char1"));
				var filter1 = r.IndexSettings.Analysis.CharFilters["char1"] as HtmlStripCharFilter;
				Assert.NotNull(filter1);
				Assert.True(r.IndexSettings.Analysis.CharFilters.ContainsKey("char2"));
				var filter2 = r.IndexSettings.Analysis.CharFilters["char2"] as MappingCharFilter;
				Assert.NotNull(filter2);
				Assert.AreEqual(2, filter2.Mappings.Count());
				Assert.True(filter2.Mappings.Contains("ph=>f"));
				Assert.True(filter2.Mappings.Contains("qu=>q"));
                var filter3 = r.IndexSettings.Analysis.CharFilters["char3"] as PatternReplaceCharFilter;
                Assert.NotNull(filter3);
                Assert.AreEqual("sample(.*)", filter3.Pattern);
                Assert.AreEqual("replacedSample $1", filter3.Replacement);
			}

			Assert.AreEqual(2, r.IndexSettings.Analysis.TokenFilters.Count);
			{ // assert token filters
				Assert.True(r.IndexSettings.Analysis.TokenFilters.ContainsKey("tokenfilter1"));
				var filter1 = r.IndexSettings.Analysis.TokenFilters["tokenfilter1"] as EdgeNGramTokenFilter;
				Assert.NotNull(filter1);
				Assert.True(r.IndexSettings.Analysis.TokenFilters.ContainsKey("tokenfilter2"));
				var filter2 = r.IndexSettings.Analysis.TokenFilters["tokenfilter2"] as SnowballTokenFilter;
				Assert.NotNull(filter2);
			}

			Assert.AreEqual(2, r.IndexSettings.Analysis.Tokenizers.Count);
			{ // assert tokenizers
				Assert.True(r.IndexSettings.Analysis.Tokenizers.ContainsKey("token1"));
				var tokenizer1 = r.IndexSettings.Analysis.Tokenizers["token1"] as KeywordTokenizer;
				Assert.NotNull(tokenizer1);
				Assert.True(r.IndexSettings.Analysis.Tokenizers.ContainsKey("token2"));
				var tokenizer2 = r.IndexSettings.Analysis.Tokenizers["token2"] as PathHierarchyTokenizer;
				Assert.NotNull(tokenizer2);
			}


			Assert.NotNull(r.IndexSettings.Similarity);
			Assert.NotNull(r.IndexSettings.Similarity.CustomSimilarities);
			Assert.AreEqual(2, r.IndexSettings.Similarity.CustomSimilarities.Count);
			{ // assert similarity
				var similarity1 = r.IndexSettings.Similarity.CustomSimilarities.FirstOrDefault(x => x.Name.Equals("test1", StringComparison.InvariantCultureIgnoreCase));
				Assert.NotNull(similarity1);
				Assert.AreEqual("DFR", similarity1.Type);
				Assert.AreEqual(4, similarity1.SimilarityParameters.Count);
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("basic_model") && x.Value.ToString().Equals("g")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("after_effect") && x.Value.ToString().Equals("l")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("normalization") && x.Value.ToString().Equals("h2")));
				Assert.True(similarity1.SimilarityParameters.Any(x => x.Key.Equals("normalization.h2")));

				var similarity2 = r.IndexSettings.Similarity.CustomSimilarities.FirstOrDefault(x => x.Name.Equals("test2", StringComparison.InvariantCultureIgnoreCase));
				Assert.NotNull(similarity2);
				Assert.AreEqual("IB", similarity2.Type);
				Assert.AreEqual(3, similarity2.SimilarityParameters.Count);
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("distribution") && x.Value.ToString().Equals("spl")));
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("lambda") && x.Value.ToString().Equals("ttf")));
				Assert.True(similarity2.SimilarityParameters.Any(x => x.Key.Equals("normalization") && x.Value.ToString().Equals("h1")));
			}
			this.Client.DeleteIndex(i=>i.Index(index));
		}
		[Test]
		public void UpdateSettingsSimple()
		{
			var index = Guid.NewGuid().ToString();
			var client = this.Client;
			var settings = new IndexSettings();
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Settings.Add("refresh_interval", "1s");
			settings.Settings.Add("search.slowlog.threshold.fetch.warn", "1s");
			client.CreateIndex(index, i=>i.InitializeUsing(settings));

			settings.Settings["refresh_interval"] = "-1";

			var r = this.Client.UpdateSettings(us=>us
				.Index(index)
				.RefreshInterval("-1")
			);

			Assert.True(r.IsValid);
			Assert.True(r.Acknowledged);
			var getResponse = this.Client.GetIndexSettings(i=>i.Index(index));
			Assert.AreEqual(getResponse.IndexSettings.Settings["refresh_interval"], "-1");

			this.Client.DeleteIndex(i=>i.Index(index));
		}




		[Test]
		public void CreateIndex()
		{
			var client = this.Client;
			var typeMappingResult = this.Client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects"));
			var typeMapping = typeMappingResult.Mapping;
			typeMapping.Name = "mytype";
			var settings = new IndexSettings();
			settings.Mappings.Add(typeMapping);
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Analysis.Analyzers.Add("snowball", new SnowballAnalyzer { Language = "English" });

			var indexName = Guid.NewGuid().ToString();
			var response = client.CreateIndex(indexName, i=>i.InitializeUsing(settings));

			Assert.IsTrue(response.IsValid);
			Assert.IsTrue(response.Acknowledged);

			var mappingResult = this.Client.GetMapping(gm=>gm.Index(indexName).Type("mytype"));
			mappingResult.Mapping.Should().NotBeNull();
			var deleteResponse = client.DeleteIndex(i=>i.Index(indexName));

			Assert.IsTrue(deleteResponse.IsValid);
			Assert.IsTrue(deleteResponse.Acknowledged);

		}

		[Test]
		public void CreateIndexUsingDescriptor()
		{
			var index = ElasticsearchConfiguration.DefaultIndex + "_clone";
			if (this.Client.IndexExists(i=>i.Index(index)).Exists)
				Client.DeleteIndex(d=>d.Index(index));

			var result = this.Client.CreateIndex(index, c => c
				.NumberOfReplicas(1)
				.NumberOfShards(1)
				.Settings(s => s
					.Add("compound_format", true)
					.Add("term_index_interval", 128)
					.Add("search.slowlog.threshold.query.warn", "2s")
				)
				.AddMapping<ElasticsearchProject>(m => m
					.MapFromAttributes()
					.NumericDetection()
					.DateDetection()
				)
				.AddMapping<Person>(m => m
					.MapFromAttributes()
				)
				.AddMapping<Person>(m => m
					.MapFromAttributes()
					.Type("override")
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
			var mapping = this.Client.GetMapping<ElasticsearchProject>().Mapping;
			var property = new StringMapping
			{
				Index = FieldIndexOption.not_analyzed
			};
			mapping.Properties.Add(fieldName, property);

			var response = this.Client.Map<ElasticsearchProject>(m=>m.InitializeUsing(mapping));

			Assert.IsTrue(response.IsValid, response.ConnectionStatus.ToString());
			Assert.IsTrue(response.Acknowledged, response.ConnectionStatus.ToString());

			mapping = this.Client.GetMapping(gm => gm.Index<ElasticsearchProject>().Type<ElasticsearchProject>()).Mapping;
			Assert.IsNotNull(mapping.Properties.ContainsKey(fieldName));
		}


		[Test]
		public void CreateIndexMultiFieldMap()
		{
			var client = this.Client;

			var typeMapping = new RootObjectMapping();
			typeMapping.Name = Guid.NewGuid().ToString("n");
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
			typeMapping.Properties = typeMapping.Properties ?? new Dictionary<PropertyNameMarker, IElasticType>();
			typeMapping.Properties.Add("name", property);

			var settings = new IndexSettings();
			settings.Mappings.Add(typeMapping);
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Analysis.Analyzers.Add("snowball", new SnowballAnalyzer { Language = "English" });

			var indexName = Guid.NewGuid().ToString();
			var response = client.CreateIndex(indexName, i=>i.InitializeUsing(settings));

			Assert.IsTrue(response.IsValid);
			Assert.IsTrue(response.Acknowledged);

			var inferrer = new ElasticInferrer(this.Settings);
			var typeName = inferrer.PropertyName(typeMapping.Name);
			Assert.IsNotNull(this.Client.GetMapping(gm=>gm.Index(indexName).Type(typeName)));

			var deleteResponse = client.DeleteIndex(i=>i.Index(indexName));

			Assert.IsTrue(deleteResponse.IsValid);
			Assert.IsTrue(deleteResponse.Acknowledged);

		}
	}
}

using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices.Analysis.Analyzers
{
	[TestFixture]
	public class AnalyzerTests : IntegrationTests
	{
		
		public AnalyzerTestResult MapAndAnalyze(
			Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector,
			Func<RootObjectMappingDescriptor<AnalyzerTest>, RootObjectMappingDescriptor<AnalyzerTest>> typeMappingDescriptor,
			string text = "ElasticSearch is yummy"
			)
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var result = this._client.CreateIndex(index, c => c
				.NumberOfReplicas(1)
				.NumberOfShards(1)
				.Analysis(analysisSelector)
				.AddMapping<AnalyzerTest>(typeMappingDescriptor)
				
			);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.OK.Should().BeTrue();
			result.Acknowledged.Should().BeTrue();
			
			//index a doc so we can be sure a shard is available
			this._client.Index<AnalyzerTest>(new AnalyzerTest() { Txt = text }, index, new IndexParameters { Refresh = true });

			var settingsResult = this._client.GetMapping<AnalyzerTest>(index);
			settingsResult.Should().NotBeNull();
			settingsResult.Properties.Should().NotBeNull();
			settingsResult.Properties["txt"].Should().NotBeNull();
			settingsResult.Properties["txt"].Type.Should().NotBeNullOrEmpty().And.BeEquivalentTo("string");

			var validateResult = this._client.Analyze<AnalyzerTest>(p => p.Txt, index, text);
			validateResult.Should().NotBeNull();
			validateResult.IsValid.Should().BeTrue();
			validateResult.Tokens.Should().NotBeEmpty();

			return new AnalyzerTestResult
			{
				AnalyzeResponse = validateResult,
				ElasticType = settingsResult.Properties["txt"]
			};
		}


		[Test]
		public void KeywordAnalysis()
		{
			var analyzerName = "keyword";
			var result = this.MapAndAnalyze(
				a => a
					.Analyzers(an => an.Add(analyzerName, new KeywordAnalyzer()))
				, m => m
					.Properties(p => p.String(sm => sm.Name(f => f.Txt).IndexAnalyzer(analyzerName).SearchAnalyzer(analyzerName)))
			);

			result.AnalyzeResponse.Tokens.Should().HaveCount(1);
			var type = result.ElasticType as StringMapping;
			type.Should().NotBeNull();
			type.Analyzer.Should().NotBeNullOrEmpty().And.BeEquivalentTo(analyzerName);

		}

		[Test]
		public void SimpleAnalysis()
		{
			var analyzerName = "simple";
			var result = this.MapAndAnalyze(
				a => a
					.Analyzers(an => an.Add(analyzerName, new SimpleAnalyzer()))
				, m => m
					.Properties(p => p.String(sm => sm.Name(f => f.Txt).IndexAnalyzer(analyzerName).SearchAnalyzer(analyzerName)))
			);

			result.AnalyzeResponse.Tokens.Should().HaveCount(3);
			var type = result.ElasticType as StringMapping;
			type.Should().NotBeNull();
			type.Analyzer.Should().NotBeNullOrEmpty().And.BeEquivalentTo(analyzerName);

		}
		[Test]
		public void WhitespaceAnalysis()
		{
			var analyzerName = "whitespace";
			var result = this.MapAndAnalyze(
				a => a
					.Analyzers(an => an.Add(analyzerName, new WhitespaceAnalyzer()))
				, m => m
					.Properties(p => p.String(sm => sm.Name(f => f.Txt).IndexAnalyzer(analyzerName).SearchAnalyzer(analyzerName)))
			);

			result.AnalyzeResponse.Tokens.Should().HaveCount(3);
			var type = result.ElasticType as StringMapping;
			type.Should().NotBeNull();
			type.Analyzer.Should().NotBeNullOrEmpty().And.BeEquivalentTo(analyzerName);

		}
		[Test]
		public void StopAnalysis()
		{
			var analyzerName = "stop";
			var result = this.MapAndAnalyze(
				a => a
					.Analyzers(an => an.Add(analyzerName, new StopAnalyzer() { StopWords = new [] { "yummy", "is"} }))
				, m => m
					.Properties(p => p.String(sm => sm.Name(f => f.Txt).IndexAnalyzer(analyzerName).SearchAnalyzer(analyzerName)))
			);

			result.AnalyzeResponse.Tokens.Should().HaveCount(1);
			var type = result.ElasticType as StringMapping;
			type.Should().NotBeNull();
			type.Analyzer.Should().NotBeNullOrEmpty().And.BeEquivalentTo(analyzerName);

		}
		[Test]
		public void SnowballAnalyzer()
		{
			var analyzerName = "snowball";
			var result = this.MapAndAnalyze(
				a => a
					.Analyzers(an => an.Add(analyzerName, new SnowballAnalyzer { Language = "Dutch" }))
				, m => m
					.Properties(p => p.String(sm => sm.Name(f => f.Txt).IndexAnalyzer(analyzerName).SearchAnalyzer(analyzerName)))
				, text: "De wereld draait door"
			);

			//'de' and 'door' are 2 dutch stopwords
			result.AnalyzeResponse.Tokens.Should().HaveCount(2);
			var type = result.ElasticType as StringMapping;
			type.Should().NotBeNull();
			type.Analyzer.Should().NotBeNullOrEmpty().And.BeEquivalentTo(analyzerName);

		}

	}
}
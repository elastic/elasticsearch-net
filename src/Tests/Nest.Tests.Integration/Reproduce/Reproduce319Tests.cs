using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce319Tests : IntegrationTests
	{
		
		/// <summary>
		///	https://github.com/Mpdreamz/NEST/issues/319
		/// </summary>
		[Test]
		public void CreateIndexShouldNotThrowNullReference()
		{
			var settings = new IndexSettings();
			settings.Similarity = new SimilaritySettings();
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Settings.Add("index.refresh_interval", "10s");
			settings.Settings.Add("merge.policy.merge_factor", "10");
			settings.Settings.Add("search.slowlog.threshold.fetch.warn", "1s");
			settings.Analysis.Analyzers.Add(new KeyValuePair<string, AnalyzerBase>("keyword", new KeywordAnalyzer()));
			settings.Analysis.Analyzers.Add(new KeyValuePair<string, AnalyzerBase>("simple", new SimpleAnalyzer()));
			settings.Mappings.Add(new RootObjectMapping
			{
				Name = "my_root_object",
				Properties = new Dictionary<PropertyNameMarker, IElasticType>
				{
					{"my_field", new StringMapping() { Name = "my_string_field "}}
				}
			});

			Assert.DoesNotThrow(() =>
			{
				var idxRsp = this._client.CreateIndex(ElasticsearchConfiguration.NewUniqueIndexName(), i=>i.InitializeUsing(settings));
				Assert.IsTrue(idxRsp.IsValid, idxRsp.ConnectionStatus.ToString());			
			});
		}

	}
}

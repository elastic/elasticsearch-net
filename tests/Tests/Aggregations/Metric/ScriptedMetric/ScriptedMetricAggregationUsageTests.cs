// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations.Metric.ScriptedMetric
{
	public class ScriptedMetricAggregationUsageTests : ProjectsOnlyAggregationUsageTestBase
	{
		private readonly Scripted _script = new Scripted
		{
			Language = "painless",
			Init = "state.commits = []",
			Map = "if (doc['state'].value == \"Stable\") { state.commits.add(doc['numberOfCommits'].value) }",
			Combine = "def sum = 0.0; for (c in state.commits) { sum += c } return sum",
			Reduce = "def sum = 0.0; for (a in states) { sum += a } return sum",
		};

		public ScriptedMetricAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			sum_the_hard_way = new
			{
				scripted_metric = new
				{
					init_script = new { source = _script.Init },
					map_script = new { source = _script.Map },
					combine_script = new { source = _script.Combine },
					reduce_script = new { source = _script.Reduce }
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.ScriptedMetric("sum_the_hard_way", sm => sm
				.InitScript(ss => ss.Source(_script.Init))
				.MapScript(ss => ss.Source(_script.Map))
				.CombineScript(ss => ss.Source(_script.Combine))
				.ReduceScript(ss => ss.Source(_script.Reduce))
			);

		protected override AggregationDictionary InitializerAggs =>
			new ScriptedMetricAggregation("sum_the_hard_way")
			{
				InitScript = new InlineScript(_script.Init),
				MapScript = new InlineScript(_script.Map),
				CombineScript = new InlineScript(_script.Combine),
				ReduceScript = new InlineScript(_script.Reduce)
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var sumTheHardWay = response.Aggregations.ScriptedMetric("sum_the_hard_way");
			sumTheHardWay.Should().NotBeNull();
			sumTheHardWay.Value<int>().Should().BeGreaterThan(0);
		}

		private class Scripted
		{
			public string Combine { get; set; }
			public string Init { get; set; }
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public string Language { get; set; }
			public string Map { get; set; }
			public string Reduce { get; set; }
		}
	}

	/// <summary>
	/// Multiple scripted metric with dictionary result
	/// </summary>
	public class ScriptedMetricMultiAggregationTests : ProjectsOnlyAggregationUsageTestBase
	{
		private readonly Scripted _first = new Scripted
		{
			Language = "painless",
			Init = "state.map = [:]",
			Map =
				"if (state.map.containsKey(doc['state'].value))" +
				"    state.map[doc['state'].value] += 1;" +
				"else" +
				"    state.map[doc['state'].value] = 1;",

			Reduce =
				"def reduce = [:];" +
				"for (map in states)" +
				"{" +
				"    for (entry in map.entrySet())" +
				"    {" +
				"        if (reduce.containsKey(entry.getKey()))" +
				"            reduce[entry.getKey()] += entry.getValue();" +
				"        else" +
				"            reduce[entry.getKey()] = entry.getValue();" +
				"    }" +
				"}" +
				"return reduce;",
			Combine = "return state.map;"
		};

		private readonly Scripted _second = new Scripted
		{
			Language = "painless",
			Init = "state.commits = []",
			Map = "if (doc['state'].value == \"Stable\") { state.commits.add(doc['numberOfCommits'].value) }",
			Reduce = "def sum = 0.0; for (a in states) { sum += a } return sum",
			Combine = "def sum = 0.0; for (c in state.commits) { sum += c } return sum",
		};

		public ScriptedMetricMultiAggregationTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			by_state_total = new
			{
				scripted_metric = new
				{
					init_script = new
					{
						source = _first.Init,
						lang = _first.Language
					},
					combine_script = new
					{
						source = _first.Combine,
						lang = _first.Language
					},
					map_script = new
					{
						source = _first.Map,
						lang = _first.Language
					},
					reduce_script = new
					{
						source = _first.Reduce,
						lang = _first.Language
					}
				}
			},
			total_commits = new
			{
				scripted_metric = new
				{
					init_script = new
					{
						source = _second.Init,
						lang = _second.Language
					},
					map_script = new
					{
						source = _second.Map,
						lang = _second.Language
					},
					combine_script = new
					{
						source = _second.Combine,
						lang = _second.Language
					},
					reduce_script = new
					{
						source = _second.Reduce,
						lang = _second.Language
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.ScriptedMetric("by_state_total", sm => sm
				.InitScript(ss => ss.Source(_first.Init).Lang(_first.Language))
				.CombineScript(ss => ss.Source(_first.Combine).Lang(_first.Language))
				.MapScript(ss => ss.Source(_first.Map).Lang(_first.Language))
				.ReduceScript(ss => ss.Source(_first.Reduce).Lang(_first.Language))
			)
			.ScriptedMetric("total_commits", sm => sm
				.InitScript(ss => ss.Source(_second.Init).Lang(_second.Language))
				.MapScript(ss => ss.Source(_second.Map).Lang(_second.Language))
				.CombineScript(ss => ss.Source(_second.Combine).Lang(_second.Language))
				.ReduceScript(ss => ss.Source(_second.Reduce).Lang(_second.Language))
			);

		protected override AggregationDictionary InitializerAggs =>
			new ScriptedMetricAggregation("by_state_total")
			{
				InitScript = new InlineScript(_first.Init) { Lang = _first.Language },
				CombineScript = new InlineScript(_first.Combine) { Lang = _first.Language },
				MapScript = new InlineScript(_first.Map) { Lang = _first.Language },
				ReduceScript = new InlineScript(_first.Reduce) { Lang = _first.Language }
			}
			&& new ScriptedMetricAggregation("total_commits")
			{
				InitScript = new InlineScript(_second.Init) { Lang = _second.Language },
				MapScript = new InlineScript(_second.Map) { Lang = _second.Language },
				CombineScript = new InlineScript(_second.Combine) { Lang = _second.Language },
				ReduceScript = new InlineScript(_second.Reduce) { Lang = _second.Language }
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var byStateTotal = response.Aggregations.ScriptedMetric("by_state_total");
			var totalCommits = response.Aggregations.ScriptedMetric("total_commits");

			byStateTotal.Should().NotBeNull();
			totalCommits.Should().NotBeNull();

			byStateTotal.Value<IDictionary<string, int>>().Should().NotBeNull();
			totalCommits.Value<int>().Should().BeGreaterThan(0);
		}

		// hide
		private class Scripted
		{
			public string Combine { get; set; }
			public string Init { get; set; }
			public string Language { get; set; }
			public string Map { get; set; }
			public string Reduce { get; set; }
		}
	}
}

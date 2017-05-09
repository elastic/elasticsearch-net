using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using System.Collections.Generic;

namespace Tests.Aggregations.Metric.ScriptedMetric
{
	public class ScriptedMetricAggregationUsageTests : AggregationUsageTestBase
	{
		public ScriptedMetricAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
		}

		protected override object ExpectJson => new
		{
			aggs = new
			{
				sum_the_hard_way = new
				{
					scripted_metric = new
					{
						init_script = new
						{
							inline = "_agg['commits'] = []",
							lang = "groovy"
						},
						map_script = new
						{
							inline = "if (doc['state'].value == \"Stable\") { _agg.commits.add(doc['numberOfCommits']) }",
							lang = "groovy"
						},
						combine_script = new
						{
							inline = "sum = 0; for (c in _agg.commits) { sum += c }; return sum",
							lang = "groovy"
						},
						reduce_script = new
						{
							inline = "sum = 0; for (a in _aggs) { sum += a }; return sum",
							lang = "groovy"
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.ScriptedMetric("sum_the_hard_way", sm => sm
					.InitScript(ss => ss.Inline("_agg['commits'] = []").Lang("groovy"))
					.MapScript(ss => ss.Inline("if (doc['state'].value == \"Stable\") { _agg.commits.add(doc['numberOfCommits']) }").Lang("groovy"))
					.CombineScript(ss => ss.Inline("sum = 0; for (c in _agg.commits) { sum += c }; return sum").Lang("groovy"))
					.ReduceScript(ss => ss.Inline("sum = 0; for (a in _aggs) { sum += a }; return sum").Lang("groovy"))
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new ScriptedMetricAggregation("sum_the_hard_way")
				{
					InitScript = new InlineScript("_agg['commits'] = []") {Lang = "groovy"},
					MapScript = new InlineScript("if (doc['state'].value == \"Stable\") { _agg.commits.add(doc['numberOfCommits']) }") {Lang = "groovy"},
					CombineScript = new InlineScript("sum = 0; for (c in _agg.commits) { sum += c }; return sum") {Lang = "groovy"},
					ReduceScript = new InlineScript("sum = 0; for (a in _aggs) { sum += a }; return sum") {Lang = "groovy"}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var sumTheHardWay = response.Aggs.ScriptedMetric("sum_the_hard_way");
			sumTheHardWay.Should().NotBeNull();
			sumTheHardWay.Value<int>().Should().BeGreaterThan(0);
		}
	}

	/// <summary>
	/// Multiple scripted metric with dictionary result
	/// </summary>
	public class ScriptedMetricMultiAggregationTests : AggregationUsageTestBase
	{
		public ScriptedMetricMultiAggregationTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage)
		{
		}

		protected override object ExpectJson => new
		{
			aggs = new
			{
				by_state_total = new
				{
					scripted_metric = new
					{
						init_script = new
						{
							inline = "params._agg.map = [:]",
							lang = "painless"
						},
						map_script = new
						{
							inline =
							"if (params._agg.map.containsKey(doc['state'].value))" +
							"    params._agg.map[doc['state'].value] += 1;" +
							"else" +
							"    params._agg.map[doc['state'].value] = 1;",
							lang = "painless"
						},
						reduce_script = new
						{
							inline =
							"def reduce = [:];" +
							"for (agg in params._aggs)" +
							"{" +
							"    for (entry in agg.map.entrySet())" +
							"    {" +
							"        if (reduce.containsKey(entry.getKey()))" +
							"            reduce[entry.getKey()] += entry.getValue();" +
							"        else" +
							"            reduce[entry.getKey()] = entry.getValue();" +
							"    }" +
							"}" +
							"return reduce;",
							lang = "painless"
						}
					}
				},
				total_commits = new
				{
					scripted_metric = new
					{
						init_script = new
						{
							inline = "params._agg.commits = []",
							lang = "painless"
						},
						map_script = new
						{
							inline = "if (doc['state'].value == \"Stable\") { params._agg.commits.add(doc['numberOfCommits']) }",
							lang = "painless"
						},
						combine_script = new
						{
							inline = "sum = 0; for (c in params._agg.commits) { sum += c }; return sum",
							lang = "painless"
						},
						reduce_script = new
						{
							inline = "sum = 0; for (a in params._aggs) { sum += a }; return sum",
							lang = "painless"
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.ScriptedMetric("by_state_total", sm => sm
					.InitScript(ss => ss.Inline("params._agg.map = [:]").Lang("painless"))
					.MapScript(ss => ss.Inline(
							"if (params._agg.map.containsKey(doc['state'].value))" +
							"    params._agg.map[doc['state'].value] += 1;" +
							"else" +
							"    params._agg.map[doc['state'].value] = 1;"
						)
						.Lang("painless"))
					.ReduceScript(ss => ss.Inline(
							"def reduce = [:];" +
							"for (agg in params._aggs)" +
							"{" +
							"    for (entry in agg.map.entrySet())" +
							"    {" +
							"        if (reduce.containsKey(entry.getKey()))" +
							"            reduce[entry.getKey()] += entry.getValue();" +
							"        else" +
							"            reduce[entry.getKey()] = entry.getValue();" +
							"    }" +
							"}" +
							"return reduce;"
						)
						.Lang("painless"))
				)
				.ScriptedMetric("total_commits", sm => sm
					.InitScript(ss => ss.Inline("params._agg.commits = []").Lang("painless"))
					.MapScript(ss => ss.Inline("if (doc['state'].value == \"Stable\") { params._agg.commits.add(doc['numberOfCommits']) }").Lang("painless"))
					.CombineScript(ss => ss.Inline("sum = 0; for (c in params._agg.commits) { sum += c }; return sum").Lang("painless"))
					.ReduceScript(ss => ss.Inline("sum = 0; for (a in params._aggs) { sum += a }; return sum").Lang("painless"))
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations =
					new ScriptedMetricAggregation("by_state_total")
					{
						InitScript = new InlineScript("params._agg.map = [:]") {Lang = "painless"},
						MapScript = new InlineScript(
								"if (params._agg.map.containsKey(doc['state'].value))" +
								"    params._agg.map[doc['state'].value] += 1;" +
								"else" +
								"    params._agg.map[doc['state'].value] = 1;")
							{Lang = "painless"},
						ReduceScript = new InlineScript(
								"def reduce = [:];" +
								"for (agg in params._aggs)" +
								"{" +
								"    for (entry in agg.map.entrySet())" +
								"    {" +
								"        if (reduce.containsKey(entry.getKey()))" +
								"            reduce[entry.getKey()] += entry.getValue();" +
								"        else" +
								"            reduce[entry.getKey()] = entry.getValue();" +
								"    }" +
								"}" +
								"return reduce;")
							{Lang = "painless"}
					} &&
					new ScriptedMetricAggregation("total_commits")
					{
						InitScript = new InlineScript("params._agg.commits = []") {Lang = "painless"},
						MapScript = new InlineScript("if (doc['state'].value == \"Stable\") { params._agg.commits.add(doc['numberOfCommits']) }") {Lang = "painless"},
						CombineScript = new InlineScript("sum = 0; for (c in params._agg.commits) { sum += c }; return sum") {Lang = "painless"},
						ReduceScript = new InlineScript("sum = 0; for (a in params._aggs) { sum += a }; return sum") {Lang = "painless"}
					}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var by_state_total = response.Aggs.ScriptedMetric("by_state_total");
			var total_commits = response.Aggs.ScriptedMetric("total_commits");

			by_state_total.Should().NotBeNull();
			total_commits.Should().NotBeNull();

			by_state_total.Value<IDictionary<string, int>>().Should().NotBeNull();
			total_commits.Value<int>().Should().BeGreaterThan(0);
		}
	}
}

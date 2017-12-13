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
		// hide
		class Scripted
		{
			public string Language { get; set; }
			public string Combine { get; set; }
			public string Reduce { get; set; }
			public string Map { get; set; }
			public string Init { get; set; }
		}

		private Scripted First = new Scripted
		{
			Language = "groovy",
			Combine = "sum = 0; for (c in _agg.commits) { sum += c }; return sum",
			Reduce = "sum = 0; for (a in _aggs) { sum += a }; return sum",
			Map = "if (doc['state'].value == \"Stable\") { _agg.commits.add(doc['numberOfCommits']) }",
			Init = "_agg['commits'] = []"
		};

		private Scripted Second = new Scripted
		{
			Language = "groovy",
			Combine = "sum = 0; for (c in _agg.commits) { sum += c }; return sum",
			Reduce = "sum = 0; for (a in _aggs) { sum += a }; return sum",
			Map = "if (doc['state'].value == \"Stable\") { _agg.commits.add(doc['numberOfCommits']) }",
			Init = "_agg['commits'] = []"
		};

		public ScriptedMetricMultiAggregationTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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
							inline = First.Init,
							lang = First.Language
						},
						map_script = new
						{
							inline = First.Map,
							lang = First.Language
						},
						combine_script = new
						{
							inline = First.Combine,
							lang = First.Language
						},
						reduce_script = new
						{
							inline = First.Reduce,
							lang = First.Language
						}
					}
				},
				total_commits = new
				{
					scripted_metric = new
					{
						init_script = new
						{
							inline = Second.Init,
							lang = Second.Language
						},
						map_script = new
						{
							inline = Second.Map,
							lang = Second.Language
						},
						combine_script = new
						{
							inline = Second.Combine,
							lang = Second.Language
						},
						reduce_script = new
						{
							inline = Second.Reduce,
							lang = Second.Language
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.ScriptedMetric("by_state_total", sm => sm
					.InitScript(ss => ss.Inline(First.Init).Lang(First.Language))
					.MapScript(ss => ss.Inline(First.Map).Lang(First.Language))
					.CombineScript(ss => ss.Inline(First.Combine).Lang(First.Language))
					.ReduceScript(ss => ss.Inline(First.Reduce).Lang(First.Language))
				)
				.ScriptedMetric("total_commits", sm => sm
					.InitScript(ss => ss.Inline(Second.Init).Lang(Second.Language))
					.MapScript(ss => ss.Inline(Second.Map).Lang(Second.Language))
					.CombineScript(ss => ss.Inline(Second.Combine).Lang(Second.Language))
					.ReduceScript(ss => ss.Inline(Second.Reduce).Lang(Second.Language))
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations =
					new ScriptedMetricAggregation("by_state_total")
					{
						InitScript = new InlineScript(First.Init) {Lang = First.Language},
						MapScript = new InlineScript(First.Map) {Lang = First.Language},
						CombineScript = new InlineScript(First.Combine) {Lang = First.Language},
						ReduceScript = new InlineScript(First.Reduce) {Lang = First.Language}
					} &&
					new ScriptedMetricAggregation("total_commits")
					{
						InitScript = new InlineScript(Second.Init) {Lang = Second.Language},
						MapScript = new InlineScript(Second.Map) {Lang = Second.Language},
						CombineScript = new InlineScript(Second.Combine) {Lang = Second.Language},
						ReduceScript = new InlineScript(Second.Reduce) {Lang = Second.Language}
					}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var by_state_total = response.Aggs.ScriptedMetric("by_state_total");
			var total_commits = response.Aggs.ScriptedMetric("total_commits");

			by_state_total.Should().NotBeNull();
			total_commits.Should().NotBeNull();

			by_state_total.Value<double>().Should().BeGreaterThan(0);
			total_commits.Value<int>().Should().BeGreaterThan(0);
		}
	}
}

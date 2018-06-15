using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.TopHits
{
	public class TopHitsAggregationUsageTests : AggregationUsageTestBase
	{
		public TopHitsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			states = new
			{
				terms = new
				{
					field = "state",
				},
				aggs = new
				{
					top_state_hits = new
					{
						top_hits = new
						{
							sort = new object[]
							{
								new
								{
									startedOn = new
									{
										order = "desc"
									}
								},
								new
								{
									_script = new
									{
										type = "number",
										script = new
										{
											lang = "painless",
											source = "Math.sin(34*(double)doc['numberOfCommits'].value)"
										},
										order = "desc"
									}
								}
							},
							_source = new
							{
								includes = new[] {"name", "lastActivity", "sourceOnly"}
							},
							size = 1,
							version = true,
							track_scores = true,
							explain = true,
							stored_fields = new[] {"startedOn"},
							highlight = new
							{
								fields = new
								{
									tags = new { },
									description = new { }
								}
							},
							script_fields = new
							{
								commit_factor = new
								{
									script = new
									{
										source = "doc['numberOfCommits'].value * 2",
									}
								}
							},
							docvalue_fields = new [] { "state" }
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs =>a => a
				.Terms("states", t => t
					.Field(p => p.State)
					.Aggregations(aa => aa
						.TopHits("top_state_hits", th => th
							.Sort(srt => srt
								.Field(sf => sf
									.Field(p => p.StartedOn)
								.Order(SortOrder.Descending))
								.Script(ss => ss
									.Type("number")
									.Script(sss => sss
										.Source("Math.sin(34*(double)doc['numberOfCommits'].value)")
										.Lang("painless")
									)
									.Order(SortOrder.Descending)
								)
							)
							.Source(src => src
								.Includes(fs => fs
									.Field(p => p.Name)
									.Field(p => p.LastActivity)
									.Field(p => p.SourceOnly)
								)
							)
							.Size(1)
							.Version()
							.TrackScores()
							.Explain()
							.StoredFields(f => f
								.Field(p => p.StartedOn)
							)
							.Highlight(h => h
								.Fields(
									hf => hf.Field(p => p.Tags),
									hf => hf.Field(p => p.Description)
								)
							)
							.ScriptFields(sfs => sfs
								.ScriptField("commit_factor", sf => sf
									.Source("doc['numberOfCommits'].value * 2")

								)

							)
							.DocValueFields(d => d
								.Field(p => p.State)
							)
						)
					)
				);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("states")
			{

					Field = Field<Project>(p => p.State),
					Aggregations = new TopHitsAggregation("top_state_hits")
					{
						Sort = new List<ISort>
						{
							new SortField { Field = Field<Project>(p => p.StartedOn), Order = SortOrder.Descending },
						new ScriptSort
							{
								Type = "number",
								Script = new InlineScript("Math.sin(34*(double)doc['numberOfCommits'].value)") { Lang = "painless" },
								Order = SortOrder.Descending
							},},
						Source = new SourceFilter
						{
							Includes = new [] { "name", "lastActivity", "sourceOnly" }
						},
						Size = 1,
						Version = true,
						TrackScores = true,
						Explain = true,
						StoredFields = new[] { "startedOn" },
						Highlight = new Highlight
						{
							Fields = new Dictionary<Field, IHighlightField>
							{
								{ Field<Project>(p => p.Tags), new HighlightField() },
								{ Field<Project>(p => p.Description), new HighlightField() }
							}
						},
						ScriptFields = new ScriptFields
						{
							{
								"commit_factor", new ScriptField
								{
									Script = new InlineScript("doc['numberOfCommits'].value * 2")
								}
							},
						},
						DocValueFields = Infer.Fields<Project>(f => f.State)
					}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var states = response.Aggregations.Terms("states");
			states.Should().NotBeNull();
			states.Buckets.Should().NotBeNullOrEmpty();
			foreach(var state in states.Buckets)
			{
				state.Key.Should().NotBeNullOrEmpty();
				state.DocCount.Should().BeGreaterThan(0);
				var topStateHits = state.TopHits("top_state_hits");
				topStateHits.Should().NotBeNull();
				topStateHits.Total.Should().BeGreaterThan(0);
				var hits = topStateHits.Hits<Project>();
				hits.Should().NotBeNullOrEmpty();
				hits.All(h => h.Explanation != null).Should().BeTrue();
				hits.All(h => h.Version.HasValue).Should().BeTrue();
				hits.All(h => h.Fields.ValuesOf<int>("commit_factor").Any()).Should().BeTrue();
				hits.All(h => h.Fields.ValuesOf<DateTime>("startedOn").Any()).Should().BeTrue();
				var projects = topStateHits.Documents<Project>();
				projects.Should().NotBeEmpty();
				projects.Should().OnlyContain(p=>!string.IsNullOrWhiteSpace(p.Name), "source filter included name");
				projects.Should().OnlyContain(p=>string.IsNullOrWhiteSpace(p.Description), "source filter does NOT include description");
				foreach (var project in projects)
					project.ShouldAdhereToSourceSerializerWhenSet();
			}
		}
	}
}

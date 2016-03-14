using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Aggregations.Metric.TopHits
{
	public class TopHitsAggregationUsageTests : AggregationUsageTestBase
	{
		public TopHitsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
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
									}
								},
								_source = new
								{
									include = new [] { "name", "startedOn" }
								},
								size = 1,
								version = true,
								explain = true,
								fielddata_fields = new [] { "state", "numberOfCommits" },
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
											inline = "doc['numberOfCommits'].value * 2"
										}
									}
								}
							}
						}
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.Terms("states", t => t
					.Field(p => p.State)
					.Aggregations(aa => aa
						.TopHits("top_state_hits", th => th
							.Sort(srt => srt
								.Field(p => p.StartedOn)
								.Order(SortOrder.Descending)
							)
							.Source(src => src
								.Include(fs => fs
									.Field(p => p.Name)
									.Field(p => p.StartedOn)
								)
							)
							.Size(1)
							.Version()
							.Explain()
							.FielddataFields(fd => fd
								.Field(p => p.State)
								.Field(p => p.NumberOfCommits)
							)
							.Highlight(h => h
								.Fields(
									hf => hf.Field(p => p.Tags),
									hf => hf.Field(p => p.Description)
								)
							)
							.ScriptFields(sfs => sfs
								.ScriptField("commit_factor", sf => sf
									.Inline("doc['numberOfCommits'].value * 2")
								)
							)
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State),
					Aggregations = new TopHitsAggregation("top_state_hits")
					{
						Sort = new List<ISort>
						{
							{
								new SortField { Field = Field<Project>(p => p.StartedOn), Order = SortOrder.Descending }
							}
						},
						Source = new SourceFilter
						{
							Include = new [] { "name", "startedOn" }
						},
						Size = 1,
						Version = true,
						Explain = true,
						FielddataFields = new [] { "state", "numberOfCommits" },
						Highlight = new Highlight
						{
							Fields = new Dictionary<Nest.Field, IHighlightField>
							{
								{ Field<Project>(p => p.Tags), new HighlightField() },
								{ Field<Project>(p => p.Description), new HighlightField() }
							}
						},
						ScriptFields = new ScriptFields
						{
							{ "commit_factor", new ScriptField { Script = new InlineScript("doc['numberOfCommits'].value * 2") } }
						}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var states = response.Aggs.Terms("states");
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
				//hits.All(h => h.Highlights.Count() > 0).Should().BeTrue();
				hits.All(h => h.Fields.ValuesOf<StateOfBeing>("state").Any()).Should().BeTrue();
				hits.All(h => h.Fields.ValuesOf<int>("numberOfCommits").Any()).Should().BeTrue();
				hits.All(h => h.Fields.ValuesOf<int>("commit_factor").Any()).Should().BeTrue();
				topStateHits.Documents<Project>().Should().NotBeEmpty();
			}
		}
	}
}

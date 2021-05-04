// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Xunit;

namespace Tests.Search.Request
{
	/**
	* Allows to highlight search results on one or more fields.
	* The implementation uses either the lucene `highlighter` or `fast-vector-highlighter`.
	*
	* See the Elasticsearch documentation on {ref_current}/search-request-body.html#request-body-search-highlighting[highlighting] for more detail.
	*/
	public class HighlightingUsageTests : SearchUsageTestBase
	{
		public HighlightingUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public string LastNameSearch { get; } = Project.First.LeadDeveloper.LastName;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new Dictionary<string, object>
				{
					{
						"name.standard", new Dictionary<string, object>
						{
							{ "query", "Upton Sons Shield Rice Rowe Roberts" }
						}
					}
				}
			},
			highlight = new
			{
				pre_tags = new[] { "<tag1>" },
				post_tags = new[] { "</tag1>" },
				encoder = "html",
				highlight_query = new
				{
					match = new Dictionary<string, object>
					{
						{
							"name.standard", new Dictionary<string, object>
							{
								{ "query", "Upton Sons Shield Rice Rowe Roberts" }
							}
						}
					}
				},
				fields = new Dictionary<string, object>
				{
					{
						"name.standard", new Dictionary<string, object>
						{
							{ "type", "plain" },
							{ "force_source", true },
							{ "fragment_size", 150 },
							{ "fragmenter", "span" },
							{ "number_of_fragments", 3 },
							{ "no_match_size", 150 }
						}
					},
					{
						"leadDeveloper.firstName", new Dictionary<string, object>
						{
							{ "type", "fvh" },
							{ "phrase_limit", 10 },
							{ "boundary_max_scan", 50 },
							{ "pre_tags", new[] { "<name>" } },
							{ "post_tags", new[] { "</name>" } },
							{
								"highlight_query", new Dictionary<string, object>
								{
									{
										"match", new Dictionary<string, object>
										{
											{
												"leadDeveloper.firstName", new Dictionary<string, object>
												{
													{ "query", "Kurt Edgardo Naomi Dariana Justice Felton" }
												}
											}
										}
									}
								}
							}
						}
					},
					{
						"leadDeveloper.lastName", new Dictionary<string, object>
						{
							{ "type", "unified" },
							{ "pre_tags", new[] { "<name>" } },
							{ "post_tags", new[] { "</name>" } },
							{
								"highlight_query", new Dictionary<string, object>
								{
									{
										"match", new Dictionary<string, object>
										{
											{
												"leadDeveloper.lastName", new Dictionary<string, object>
												{
													{ "query", LastNameSearch }
												}
											}
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
			.Query(q => q
				.Match(m => m
					.Field(f => f.Name.Suffix("standard"))
					.Query("Upton Sons Shield Rice Rowe Roberts")
				)
			)
			.Highlight(h => h
				.PreTags("<tag1>")
				.PostTags("</tag1>")
				.Encoder(HighlighterEncoder.Html)
				.HighlightQuery(q => q
					.Match(m => m
						.Field(f => f.Name.Suffix("standard"))
						.Query("Upton Sons Shield Rice Rowe Roberts")
					)
				)
				.Fields(
					fs => fs
						.Field(p => p.Name.Suffix("standard"))
						.Type("plain")
						.ForceSource()
						.FragmentSize(150)
						.Fragmenter(HighlighterFragmenter.Span)
						.NumberOfFragments(3)
						.NoMatchSize(150),
					fs => fs
						.Field(p => p.LeadDeveloper.FirstName)
						.Type(HighlighterType.Fvh)
						.PreTags("<name>")
						.PostTags("</name>")
						.BoundaryMaxScan(50)
						.PhraseLimit(10)
						.HighlightQuery(q => q
							.Match(m => m
								.Field(p => p.LeadDeveloper.FirstName)
								.Query("Kurt Edgardo Naomi Dariana Justice Felton")
							)
						),
					fs => fs
						.Field(p => p.LeadDeveloper.LastName)
						.Type(HighlighterType.Unified)
						.PreTags("<name>")
						.PostTags("</name>")
						.HighlightQuery(q => q
							.Match(m => m
								.Field(p => p.LeadDeveloper.LastName)
								.Query(LastNameSearch)
							)
						)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = new MatchQuery
				{
					Query = "Upton Sons Shield Rice Rowe Roberts",
					Field = "name.standard"
				},
				Highlight = new Highlight
				{
					PreTags = new[] { "<tag1>" },
					PostTags = new[] { "</tag1>" },
					Encoder = HighlighterEncoder.Html,
					HighlightQuery = new MatchQuery
					{
						Query = "Upton Sons Shield Rice Rowe Roberts",
						Field = "name.standard"
					},
					Fields = new Dictionary<Field, IHighlightField>
					{
						{
							"name.standard", new HighlightField
							{
								Type = HighlighterType.Plain,
								ForceSource = true,
								FragmentSize = 150,
								Fragmenter = HighlighterFragmenter.Span,
								NumberOfFragments = 3,
								NoMatchSize = 150
							}
						},
						{
							"leadDeveloper.firstName", new HighlightField
							{
								Type = "fvh",
								PhraseLimit = 10,
								BoundaryMaxScan = 50,
								PreTags = new[] { "<name>" },
								PostTags = new[] { "</name>" },
								HighlightQuery = new MatchQuery
								{
									Field = "leadDeveloper.firstName",
									Query = "Kurt Edgardo Naomi Dariana Justice Felton"
								}
							}
						},
						{
							"leadDeveloper.lastName", new HighlightField
							{
								Type = HighlighterType.Unified,
								PreTags = new[] { "<name>" },
								PostTags = new[] { "</name>" },
								HighlightQuery = new MatchQuery
								{
									Field = "leadDeveloper.lastName",
									Query = LastNameSearch
								}
							}
						}
					}
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			foreach (var highlightsInEachHit in response.Hits.Select(d => d.Highlight))
			{
				foreach (var highlightField in highlightsInEachHit)
				{
					if (highlightField.Key == "name.standard")
					{
						foreach (var highlight in highlightField.Value)
						{
							highlight.Should().Contain("<tag1>");
							highlight.Should().Contain("</tag1>");
						}
					}
					else if (highlightField.Key == "leadDeveloper.firstName")
					{
						foreach (var highlight in highlightField.Value)
						{
							highlight.Should().Contain("<name>");
							highlight.Should().Contain("</name>");
						}
					}
					else if (highlightField.Key == "leadDeveloper.lastName")
					{
						foreach (var highlight in highlightField.Value)
						{
							highlight.Should().Contain("<name>");
							highlight.Should().Contain("</name>");
						}
					}
					else
						Assert.True(false, $"highlights contains unexpected key {highlightField.Key}");
				}
			}
		}
	}
}

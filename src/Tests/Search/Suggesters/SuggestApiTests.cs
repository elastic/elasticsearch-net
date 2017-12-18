using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;
using FluentAssertions;
using System.Linq;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Search.Suggesters
{
	/** == Suggest API

	*/
	public class SuggestApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISuggestResponse<Project>, ISuggestRequest, SuggestDescriptor<Project>, SuggestRequest<Project>>
	{
		private string _phraseSuggestField = "description.shingle";

		public SuggestApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
#pragma warning disable 618
			fluent: (c, f) => c.Suggest<Project>(f),
			fluentAsync: (c, f) => c.SuggestAsync<Project>(f),
			request: (c, r) => c.Suggest<Project>(r),
			requestAsync: (c, r) => c.SuggestAsync<Project>(r)
#pragma warning restore 618
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_suggest";
		protected override bool SupportsDeserialization => false;

		protected override Func<SuggestDescriptor<Project>, ISuggestRequest> Fluent => s => s
			.Term("my-term-suggest", t => t
				.MaxEdits(1)
				.MaxInspections(20)
				.MaxTermFrequency(300000)
				.MinDocFrequency(1)
				.MinWordLength(2)
				.PrefixLength(1)
				.SuggestMode(SuggestMode.Always)
				.Analyzer("standard")
				.Field(p => p.Description)
				.ShardSize(7)
				.Size(8)
				.Text(SuggestText)
			)
			.Completion("my-completion-suggest", c => c
				.Contexts(ctxs => ctxs
					.Context("color", ctx => ctx.Context(Project.First.Suggest.Contexts.Values.SelectMany(v => v).First()))
				)
				.Fuzzy(f => f
					.Fuzziness(Fuzziness.Auto)
					.MinLength(1)
					.PrefixLength(2)
					.Transpositions()
					.UnicodeAware(false)
				)
				.Analyzer("simple")
				.Field(p => p.Suggest)
				.Size(8)
				.Prefix(Project.First.Name)
			)
			.Phrase("my-phrase-suggest", ph => ph
				.Text(PhraseSuggest)
				.Field(p => p.Description.Suffix("shingle"))
				.GramSize(4)
				.RealWordErrorLikelihood(0.95)
				.MaxErrors(1)
				.Confidence(1)
				.Collate(c => c
					.Query(q => q
						.Inline("{ \"match\": { \"{{field_name}}\" : \"{{suggestion}}\" }}")
					)
					.Params(p => p.Add("field_name", _phraseSuggestField))
					.Prune()
				)
				.DirectGenerator(d => d
					.Field(p => p.Description.Suffix("shingle"))
				)
				.Highlight(h => h
					.PreTag("<em>")
					.PostTag("</em>")
				)
				.Smoothing(smoothing => smoothing
					.StupidBackoff(b => b.Discount(0.4))
				)
			);

		protected override SuggestRequest<Project> Initializer =>
			new SuggestRequest<Project>
			{
				Suggest = new SuggestContainer
				{
					{
						"my-term-suggest", new SuggestBucket
						{
							Text = SuggestText,
							Term = new TermSuggester
							{
								MaxEdits = 1,
								MaxInspections = 20,
								MaxTermFrequency = 300000,
								MinDocFrequency = 1,
								MinWordLength = 2,
								PrefixLength = 1,
								SuggestMode = SuggestMode.Always,
								Analyzer = "standard",
								Field = Field<Project>(p => p.Description),
								ShardSize = 7,
								Size = 8,
							}
						}
					},
					{
						"my-completion-suggest", new SuggestBucket
						{
							Prefix = Project.Instance.Name,
							Completion = new CompletionSuggester
							{
								Contexts = new Dictionary<string, IList<ISuggestContextQuery>>
								{
									{
										"color",
										new List<ISuggestContextQuery>
										{
											new SuggestContextQuery
											{
												Context = Project.First.Suggest.Contexts.Values.SelectMany(v => v).First()
											}
										}
									}
								},
								Fuzzy = new FuzzySuggester
								{
									Fuzziness = Fuzziness.Auto,
									MinLength = 1,
									PrefixLength = 2,
									Transpositions = true,
									UnicodeAware = false
								},
								Analyzer = "simple",
								Field = Field<Project>(p => p.Suggest),
								Size = 8,
							}
						}
					},
					{
						"my-phrase-suggest", new SuggestBucket
						{
							Text = PhraseSuggest,
							Phrase = new PhraseSuggester
							{
								Field = Field<Project>(p => p.Description.Suffix("shingle")),
								GramSize = 4,
								RealWordErrorLikelihood = 0.95,
								MaxErrors = 1,
								Confidence = 1.0,
								Collate = new PhraseSuggestCollate
								{
									Query = new TemplateQuery
									{
										Inline = "{ \"match\": { \"{{field_name}}\" : \"{{suggestion}}\" }}",
									},
									Params = new Dictionary<string, object>
									{
										{"field_name", _phraseSuggestField}
									},
									Prune = true
								},
								DirectGenerator = new List<DirectGenerator>
								{
									new DirectGenerator {Field = _phraseSuggestField}
								},
								Smoothing = new StupidBackoffSmoothingModel
								{
									Discount = 0.4
								},
								Highlight = new PhraseSuggestHighlight {PreTag = "<em>", PostTag = "</em>"}
							}
						}
					},
				}
			};

		private static string SuggestText { get; } = (
			from word in Project.First.Description.Split(' ')
			where word.Length > 4
			select word.Remove(word.Length - 1)
		).First();


		private static string PhraseSuggest
		{
			get
			{
				var words = Project.First.Description.Split(' ');
				var pairs = words.Zip(words.Skip(1), (x, y) => new[] {x, y});
				var pair = pairs.First(t => t[0].Length > 4 && t[1].Length > 4);
				return $"{(pair[0].Remove(pair[0].Length - 1))} {pair[1]}";
			}
		}


		protected override void ExpectResponse(ISuggestResponse<Project> response)
		{
			/** === Handling Responses
			* Get the suggestions for a suggester by indexing into
			* the `.Suggestions` dictionary on the response
			*/
			AssertCompletionSuggestResponse(response);
			AssertTermSuggestResponse(response);
			AssertPhraseSuggestResponse(response);
		}

		private static void AssertPhraseSuggestResponse(ISuggestResponse<Project> response)
		{
			var myTermSuggest = response.Suggestions["my-phrase-suggest"];
			myTermSuggest.Should().NotBeNull();

			var suggest = myTermSuggest.First();
			suggest.Text.Should().Be(PhraseSuggest);
			suggest.Length.Should().BeGreaterThan(0);
			suggest.Options.Should().NotBeNull().And.NotBeEmpty();

			foreach (var opt in suggest.Options)
			{
				opt.Text.Should().NotBeNullOrWhiteSpace();
				opt.Score.Should().BeGreaterThan(0);
				opt.Highlighted.Should().NotBeNullOrEmpty().And.Contain("</em>");
				opt.CollateMatch.Should().BeTrue();
			}
		}

		private static void AssertTermSuggestResponse(ISuggestResponse<Project> response)
		{
			var myTermSuggest = response.Suggestions["my-term-suggest"];
			myTermSuggest.Should().NotBeNull();

			var suggest = myTermSuggest.First();
			suggest.Text.Should().BeEquivalentTo(SuggestText);
			suggest.Length.Should().BeGreaterThan(0);
			suggest.Options.Should().NotBeNull().And.NotBeEmpty();

			foreach (var opt in suggest.Options)
			{
				opt.Text.Should().NotBeNullOrWhiteSpace();
				opt.Score.Should().BeGreaterThan(0);
				opt.Frequency.Should().BeGreaterThan(0);
			}
		}

		private static void AssertCompletionSuggestResponse(ISuggestResponse<Project> response)
		{
			var myCompletionSuggest = response.Suggestions["my-completion-suggest"];
			myCompletionSuggest.Should().NotBeNull();

			var suggest = myCompletionSuggest.First();
			suggest.Text.Should().Be(Project.Instance.Name);
			suggest.Length.Should().BeGreaterThan(0);

			var option = suggest.Options.First();
			option.Text.Should().NotBeNullOrEmpty();
			option.Index.Should().Be("project");
			option.Type.Should().Be("project");
			option.Id.Should().NotBeNull();
			option.Source.Should().NotBeNull();
			option.Source.Name.Should().NotBeNullOrWhiteSpace();
			option.Score.Should().BeGreaterThan(0);
			option.Contexts.Should().NotBeNull().And.NotBeEmpty();
			option.Contexts.Should().ContainKey("color");
			var colorContexts = option.Contexts["color"];
			colorContexts.Should().NotBeNull().And.HaveCount(1);
			colorContexts.First().Category.Should().Be((Project.First.Suggest.Contexts.Values.SelectMany(v => v).First()));
		}

		protected override object ExpectJson => new Dictionary<string, object>
		{
			{
				"my-completion-suggest", new
				{
					completion = new
					{
						analyzer = "simple",
						contexts = new
						{
							color = new[]
							{
								new {context = Project.First.Suggest.Contexts.Values.SelectMany(v => v).First()}
							}
						},
						field = "suggest",
						fuzzy = new
						{
							fuzziness = "AUTO",
							min_length = 1,
							prefix_length = 2,
							transpositions = true,
							unicode_aware = false
						},
						size = 8,
					},
					prefix = Project.Instance.Name
				}
			},
			{
				"my-phrase-suggest", new
				{
					phrase = new
					{
						collate = new
						{
							query = new
							{
								inline = "{ \"match\": { \"{{field_name}}\" : \"{{suggestion}}\" }}",
							},
							@params = new
							{
								field_name = _phraseSuggestField
							},
							prune = true,
						},
						confidence = 1.0,
						max_errors = 1.0,
						direct_generator = new[]
						{
							new {field = _phraseSuggestField}
						},
						highlight = new {post_tag = "</em>", pre_tag = "<em>"},
						smoothing = new { stupid_backoff = new { discount = 0.4 }},
						field = _phraseSuggestField,
						gram_size = 4,
						real_word_error_likelihood = 0.95
					},
					text = PhraseSuggest
				}
			},
			{
				"my-term-suggest", new
				{
					term = new
					{
						analyzer = "standard",
						field = "description",
						max_edits = 1,
						max_inspections = 20,
						max_term_freq = 300000.0,
						min_doc_freq = 1.0,
						min_word_length = 2,
						prefix_length = 1,
						shard_size = 7,
						size = 8,
						suggest_mode = "always"
					},
					text = SuggestText
				}
			}
		};
	}
}

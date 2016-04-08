using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;
using FluentAssertions;
using System.Linq;

namespace Tests.Search.Request
{
	/**
	 * The suggest feature suggests similar looking terms based on a provided text by using a suggester.
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-suggesters.html[Suggesters] for more detail.
	 */
	public class SuggestUsageTests : SearchUsageTestBase
	{
		public SuggestUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson =>
			new
			{
				suggest = new Dictionary<string, object>{
					{  "my-completion-suggest", new {
					  completion = new {
						analyzer = "simple",
						contexts = new {
						  color = new object[] {
							  new { context = Project.Projects.First().Suggest.Contexts.Values.SelectMany(v => v).First() }
						  }
						},
						field = "suggest",
						fuzzy = new {
						  fuzziness = "AUTO",
						  min_length = 1,
						  prefix_length = 2,
						  transpositions = true,
						  unicode_aware = false
						},
						size = 8,
						payload = new [] { "numberOfCommits" }
					  },
					  prefix = Project.Instance.Name
					} },
					{  "my-phrase-suggest", new {
					  phrase = new {
						collate = new {
						  query = new {
							inline = "{ \"match\": { \"{{field_name}}\": \"{{suggestion}}\" }}",
							@params = new {
						      field_name = "title"
							}
						  },
						  prune = true,
						},
						confidence = 10.1,
						direct_generator = new [] {
						  new { field = "description" }
						},
						field = "name",
						gram_size = 1,
						real_word_error_likelihood = 0.5
					  },
					  text = "hello world"
					} },
					{  "my-term-suggest", new {
					  term = new {
						analyzer = "standard",
						field = "name",
						max_edits = 1,
						max_inspections = 2,
						max_term_freq = 3.0,
						min_doc_freq = 4.0,
						min_word_length = 5,
						prefix_length = 6,
						shard_size = 7,
						size = 8,
						suggest_mode = "always"
					  },
					  text = "hello world"
					} }
				  }
			};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Suggest(ss => ss
				.Term("my-term-suggest", t => t
					.MaxEdits(1)
					.MaxInspections(2)
					.MaxTermFrequency(3)
					.MinDocFrequency(4)
					.MinWordLength(5)
					.PrefixLength(6)
					.SuggestMode(SuggestMode.Always)
					.Analyzer("standard")
					.Field(p => p.Name)
					.ShardSize(7)
					.Size(8)
					.Text("hello world")
				)
				.Completion("my-completion-suggest", c => c
					.Contexts(ctxs => ctxs
						.Context("color",
							ctx => ctx.Context(Project.Projects.First().Suggest.Contexts.Values.SelectMany(v => v).First())
						)
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
					.Prefix(Project.Instance.Name)
					.Payload(fs => fs.Field(p => p.NumberOfCommits))
				)
				.Phrase("my-phrase-suggest", ph => ph
					.Collate(c => c
						.Query(q => q
							.Inline("{ \"match\": { \"{{field_name}}\": \"{{suggestion}}\" }}")
							.Params(p => p.Add("field_name", "title"))
						)
						.Prune()
					)
					.Confidence(10.1)
					.DirectGenerator(d => d
						.Field(p => p.Description)
					)
					.GramSize(1)
					.Field(p => p.Name)
					.Text("hello world")
					.RealWordErrorLikelihood(0.5)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Suggest = new SuggestContainer
				{
					{ "my-term-suggest", new SuggestBucket
					{
						Text = "hello world",
						Term = new TermSuggester
						{
							MaxEdits = 1,
							MaxInspections = 2,
							MaxTermFrequency = 3,
							MinDocFrequency = 4,
							MinWordLength = 5,
							PrefixLength = 6,
							SuggestMode = SuggestMode.Always,
							Analyzer = "standard",
							Field = Field<Project>(p=>p.Name),
							ShardSize = 7,
							Size = 8
						}
					} },
					{ "my-completion-suggest", new SuggestBucket
					{
						Prefix = Project.Instance.Name,
						Completion = new CompletionSuggester
						{
							Contexts = new Dictionary<string, IList<ISuggestContextQuery>>
							{
								{ "color", new List<ISuggestContextQuery> { new SuggestContextQuery { Context = Project.Projects.First().Suggest.Contexts.Values.SelectMany(v => v).First() } } }
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
							Field = Field<Project>(p=>p.Suggest),
							Size = 8,
							Payload = Fields<Project>("numberOfCommits")
						}
					} },
					{ "my-phrase-suggest", new SuggestBucket
					{
						Text = "hello world",
						Phrase = new PhraseSuggester
						{
							Collate = new PhraseSuggestCollate
							{
								Query = new InlineScript("{ \"match\": { \"{{field_name}}\": \"{{suggestion}}\" }}")
								{
									Params = new Dictionary<string, object>
									{
										{ "field_name", "title" }
									}
								},
								Prune = true
							},
							Confidence = 10.1,
							DirectGenerator = new List<DirectGenerator>
							{
								new DirectGenerator { Field = "description" }
							},
							GramSize = 1,
							Field = "name",
							RealWordErrorLikelihood = 0.5
						}
					} },
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			var myCompletionSuggest = response.Suggest["my-completion-suggest"];
			myCompletionSuggest.Should().NotBeNull();
			var suggest = myCompletionSuggest.First();
			suggest.Text.Should().Be(Project.Instance.Name);
			suggest.Length.Should().BeGreaterThan(0);
			var option = suggest.Options.First();
			option.Text.Should().NotBeNullOrEmpty();
			option.Score.Should().BeGreaterThan(0);
			option.Payload.Should().NotBeNull();
			option.Payload.Value<int>("numberOfCommits").Should().BeGreaterThan(0);
			option.Contexts.Should().NotBeNull().And.NotBeEmpty();
			option.Contexts.Should().ContainKey("color");
			var colorContexts = option.Contexts["color"];
			colorContexts.Should().NotBeNull().And.HaveCount(1);
			colorContexts.First().Category.Should().Be((Project.Projects.First().Suggest.Contexts.Values.SelectMany(v => v).First()));
		}
	}
}

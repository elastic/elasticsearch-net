using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.Search.Request
{
	/**
	 * Allows to add one or more sort on specific fields. Each sort can be reversed as well. 
	 * The sort is defined on a per field level, with special field name for _score to sort by score.
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
						analyzer = "analyzer",
						context = new {
						  color = "blue"
						},
						field = "name",
						fuzzy = new {
						  fuzziness = "AUTO",
						  min_length = 1,
						  prefix_length = 2,
						  transpositions = true,
						  unicode_aware = false
						},
						shard_size = 7,
						size = 8
					  },
					  text = "hello world"
					} },
					{  "my-phrase-suggest", new {
					  phrase = new {
						collate = new {
				          @params = new {
							field_name = "title"
						  },
						  prune = true,
						  query = new {
							match_all = new {}
						  }
						},
						confidence = 10.1,
						direct_generator = new [] {
						  new { field = "description" }
						},
						field = "name",
						gram_size = 1
					  },
					  text = "hello world"
					} },
					{  "my-term-suggest", new {
					  term = new {
						analyzer = "analyzer",
						field = "name",
						max_edits = 1,
						max_inspections = 2,
						max_term_freq = 3.0,
						min_doc_freq = 4.0,
						min_word_len = 5,
						prefix_len = 6,
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
				.Term("my-term-suggest", t=>t
					.MaxEdits(1)
					.MaxInspections(2)
					.MaxTermFrequency(3)
					.MinDocFrequency(4)
					.MinWordLength(5)
					.PrefixLength(6)
					.SuggestMode(SuggestMode.Always)
					.Analyzer("analyzer")
					.Field(p=>p.Name)
					.ShardSize(7)
					.Size(8)
					.Text("hello world")
				)
				.Completion("my-completion-suggest", c=>c
					.Context(ctx=>ctx
						.Add("color", "blue")
					)
					.Fuzzy(f=>f
						.Fuzziness(Fuzziness.Auto)
						.MinLength(1)
						.PrefixLength(2)
						.Transpositions()
						.UnicodeAware(false)
					)
					.Analyzer("analyzer")
					.Field(p=>p.Name)
					.ShardSize(7)
					.Size(8)
					.Text("hello world")
				)
				.Phrase("my-phrase-suggest", ph=>ph
					.Collate(c=>c
						.Query(q=>q.MatchAll())
						.Params(p=>p.Add("field_name", "title"))
						.Prune()
					)
					.Confidence(10.1)
					.DirectGenerator(d=>d
						.Field(p=>p.Description)
					)
					.GramSize(1)
					.Field(p=>p.Name)
					.Text("hello world")
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
							MinWordLen = 5,
							PrefixLen = 6,
							SuggestMode = SuggestMode.Always,
							Analyzer = "analyzer",
							Field = Field<Project>(p=>p.Name),
							ShardSize = 7,
							Size = 8
						}
					} },
					{ "my-completion-suggest", new SuggestBucket
					{
						Text = "hello world",
						Completion = new CompletionSuggester
						{
							Context = new Dictionary<string, object> { { "color", "blue" } },
							Fuzzy = new FuzzySuggester
							{
								Fuzziness = Fuzziness.Auto,
								MinLength = 1,
								PrefixLength = 2,
								Transpositions = true,
								UnicodeAware = false
							},
							Analyzer = "analyzer",
							Field = Field<Project>(p=>p.Name),
							ShardSize = 7,
							Size = 8
						}
					} },
					{ "my-phrase-suggest", new SuggestBucket
					{
						Text = "hello world",
						Phrase = new PhraseSuggester
						{
							Collate = new PhraseSuggestCollate
							{
								Query = new MatchAllQuery(),
								Params = new Dictionary<string, object> { { "field_name", "title" } },
								Prune = true
							},
							Confidence = 10.1,
							DirectGenerator = new List<DirectGenerator>
							{
								new DirectGenerator { Field = "description" }
							},
							GramSize = 1,
							Field = "name",
						}
					} },
				}
			};
	}
}

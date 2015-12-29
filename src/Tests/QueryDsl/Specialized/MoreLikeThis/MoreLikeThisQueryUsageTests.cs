using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Specialized.MoreLikeThis
{
	public class MoreLikeThisUsageTests : QueryDslUsageTestsBase
	{
		public MoreLikeThisUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			mlt = new
			{
				fields = new[] { "name" },
				minimum_should_match = 1,
				stop_words = new[] { "and", "the" },
				min_term_freq = 1,
				max_query_terms = 12,
				min_doc_freq = 1,
				max_doc_freq = 12,
				min_word_len = 10,
				max_word_len = 300,
				boost_terms = 1.1,
				analyzer = "some_analyzer",
				include = true,
				like = new object[] {
					new {
						_index = "project",
						_type = "project",
						_id = Project.Instance.Name
					},
					"some long text"
				},
				unlike = new[] { "not like this text" },
				_name = "named_query",
				boost = 1.1
			}
		};

		protected override QueryContainer QueryInitializer => new MoreLikeThisQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Fields = Fields<Project>(p=>p.Name),
			Like = new List<Like>
			{
				new LikeDocument<Project>(Project.Instance.Name),
				"some long text"
			},
			Analyzer = "some_analyzer",
			BoostTerms = 1.1,
			Include = true,
			MaxDocumentFrequency = 12,
			MaxQueryTerms = 12,
			MaxWordLength = 300,
			MinDocumentFrequency = 1,
			MinTermFrequency = 1,
			MinWordLength = 10,
			MinimumShouldMatch = 1,
			StopWords = new [] { "and", "the"},
			Unlike = new List<Like>
			{
				"not like this text"
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MoreLikeThis(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Like(l=>l
					.Document(d=>d .Id(Project.Instance.Name))
					.Text("some long text")
				)
				.Analyzer("some_analyzer")
				.BoostTerms(1.1)
				.Include()
				.MaxDocumentFrequency(12)
				.MaxQueryTerms(12)
				.MaxWordLength(300)
				.MinDocumentFrequency(1)
				.MinTermFrequency(1)
				.MinWordLength(10)
				.StopWords("and", "the")
				.MinimumShouldMatch(1)
				.Fields(f=>f.Field(p=>p.Name))
				.Unlike(l=>l
					.Text("not like this text")
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMoreLikeThisQuery>(a => a.MoreLikeThis)
		{
			q => q.Like = null,
			q => q.Like = Enumerable.Empty<Like>(),
			q => q.Fields = null,
		};
	}
}

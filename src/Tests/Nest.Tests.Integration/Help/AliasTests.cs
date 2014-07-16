using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{

	/// <summary>
	/// I use this class to answer questions on github issues/stackoverflow
	/// Tests that are written here are subject to removal at any time
	/// </summary>
	[TestFixture]
	public class HelpTests : IntegrationTests
	{
		[ElasticType(Name = "Entry", IdProperty = "Id")]
		public class Entry
		{
			public string Id { get; set; }
			public string Title { get; set; }
			public string Description { get; set; }
			public string Award { get; set; }
			public int Year { get; set; }
		}
		[Test]
		public void CustomBoosting()
		{
			var searchText = "myQuery";
			Client.Search<Entry>(s=>s
				.Query(q =>q
					.Boosting(bq=>bq
						.Positive(pq=>pq
							//disabling obsolete message in this test
							#pragma warning disable 0618
							.CustomScore(cbf=>cbf
								.Query(cbfq=>cbfq
									.QueryString(qs => qs
										.OnFieldsWithBoost(d =>
											d.Add(entry => entry.Title, 5.0)
											.Add(entry => entry.Description, 2.0)
										)
										.Query(searchText)
									)
								)
								.Script("_score + doc['year'].value")
							)
							#pragma warning restore 0618
						)
						.Negative(nq=>nq
							.Filtered(nfq=>nfq
								.Query(qq=>qq.MatchAll())
								.Filter(f=>f.Missing(p=>p.Award))
							)
						)
						.NegativeBoost(0.2)
					)
				)
			);
		}
	}
}
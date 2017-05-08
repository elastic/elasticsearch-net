using System;
using System.Diagnostics;
using FluentAssertions;
using Tests.Framework;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue2723
	{
		public class Residential
		{
			public string SourceDisplayName { get; set; }
			public string FullAddress { get; set; }
			public string FirstName1 { get; set; }
			public string CityName { get; set; }
			public string PostalCode { get; set; }
			public string Country { get; set; }
			public string SourceDisplayPhone { get; set; }
		}

		[U]
		public void SerializationOverhead()
		{
			var fixedResponse =
				@"{""took"":147,""timed_out"":false,""_shards"":{""total"":10,""successful"":10,""failed"":0},""hits"":{""total"":0,""max_score"":null,""hits"":[]}}";
			var client = TestClient.GetFixedStringResponseClient(fixedResponse);

			//warmup
			var response = FixedSearch(client);
			response.IsValid.Should().BeTrue();
			response.Took.Should().Be(147);

			var iterations = 10000;
			var sw = new Stopwatch();
			var elapsed = new List<TimeSpan>(iterations);
			for (var i = 0; i < iterations; i++)
			{
				sw.Restart();
				response = FixedSearch(client);
				elapsed.Add(sw.Elapsed);
				response.IsValid.Should().BeTrue();
				response.Took.Should().Be(147);
			}

			var orderedElapsed = elapsed.OrderBy(e=>e).ToList();
			var median = TimeSpan.FromTicks((orderedElapsed.ElementAt(iterations/2).Ticks + orderedElapsed.ElementAt((iterations-1)/2).Ticks) / 2);
			median.Should().BeLessThan(TimeSpan.FromMilliseconds(5)).And.BeGreaterThan(TimeSpan.FromMilliseconds(0));
		}

		private static ISearchResponse<Residential> FixedSearch(IElasticClient client)
		{
			var response = client.Search<Residential>(q => q
				.Index("someindex")
				.Type("sometype")
				.ExecuteOnLocalShard()
				.Query(qq => qq
					.Bool(b => b
						.Must(s => s.Term(n => n.SourceDisplayPhone, "some-phone-number"))
						.Filter(f => f.Bool(bb => bb
							.MustNot(
								n => n.Term(t => t.Field("publicationFlag").Value("0")),
								n => n.Term(t => t.Field("infobelWebPublicationFlag").Value("0")))
						))))
				.Take(1)
			);
			return response;
		}
	}
}

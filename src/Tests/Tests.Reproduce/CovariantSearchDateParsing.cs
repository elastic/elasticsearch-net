using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class CovariantSearchDateParsing : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public CovariantSearchDateParsing(WritableCluster cluster) => _cluster = cluster;

		[I] public void CovariantSearchDateParsingTest()
		{
			var client = _cluster.Client;
			var index = "convariantsearchconverstiontest";
			if (client.IndexExists(index).Exists)
				client.DeleteIndex(index);

			var date = "2013-06-24T00:00:00.000";
			var indexResult = client.Index(new SearchResult { DateAsString = date, Date = DateTime.Parse(date) }, i => i.Id(1).Index(index));
			indexResult.ShouldBeValid();
			client.Refresh(Indices.All);
			var response = client.Search<ISearchResult>(new SearchRequest<ISearchResult>(index, typeof(SearchResult)));
			response.ShouldBeValid();
			response.Documents.Count.Should().Be(1);
			var document = response.Documents.SingleOrDefault();
			document.Should().NotBeNull();
			document.DateAsString.Should().Be(date);
			document.Date.ShouldBeEquivalentTo(DateTime.Parse(date));
		}

		public interface ISearchResult
		{
			DateTime Date { get; set; }
			string DateAsString { get; set; }
		}

		public class SearchResult : ISearchResult
		{
			public DateTime Date { get; set; }
			public string DateAsString { get; set; }
		}
	}
}

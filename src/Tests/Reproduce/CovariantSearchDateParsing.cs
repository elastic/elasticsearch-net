using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Reproduce
{
	public class CovariantSearchDateParsing : IClusterFixture<WritableCluster>
	{
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

		private readonly WritableCluster _cluster;

		public CovariantSearchDateParsing(WritableCluster cluster)
		{
			_cluster = cluster;
		}

		[I] public void CovariantSearchDateParsingTest()
		{
			var client = _cluster.Client;
			var index = "convariantsearchconverstiontest";
			if (client.IndexExists(index).Exists)
				client.DeleteIndex(index);

			var date = "2013-06-24T00:00:00.000";
			var indexResult = client.Index(new SearchResult { DateAsString = date, Date = DateTime.Parse(date) }, i => i.Id(1).Index(index));
			indexResult.IsValid.Should().BeTrue();
			client.Refresh(Nest.Indices.All);
			var response = client.Search<ISearchResult>(new SearchRequest<ISearchResult>(index, typeof(SearchResult)));
			response.IsValid.Should().BeTrue();
			response.Documents.Count.Should().Be(1);
			var document = response.Documents.SingleOrDefault();
			document.Should().NotBeNull();
			document.DateAsString.Should().Be(date);
			document.Date.ShouldBeEquivalentTo(DateTime.Parse(date));
		}
	}
}

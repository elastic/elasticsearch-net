using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using Nest.Resolvers;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Core.MultiPercolate
{
	[TestFixture]
	public class MultiPercolateTests : IntegrationTests
	{
		[Test]
		public void MultiPercolate_ReturnsExpectedResults()
		{

			//lets start fresh using a new index
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			IntegrationSetup.CreateTestIndex(this.Client, indexName);
			
			// lets register several percolators in our new index that do a term match
			// on document name == indexname
			// we associate some metadata with the percolator so that we can later filter
			// the ones we want to execute easier
			foreach (var i in Enumerable.Range(0, 10))
			{
				var registerPercolator = this.Client.RegisterPercolator(new RegisterPercolatorRequest(indexName, "my-percolator-" + i)
				{
					Query = new TermQuery
					{
						Field = Property.Path<ElasticsearchProject>(p => p.Name.Suffix("sort")),
						Value = indexName
					},
					MetaData = new Dictionary<string, object>
					{
						{ "order", i}
					}
				});
				registerPercolator.IsValid.Should().BeTrue();
			}
			

			// Set up 2 projects to index both with indexName as Name
			var projects = Enumerable.Range(0, 2)
				.Select(i => new ElasticsearchProject { Id = 1337 + i, Name = indexName })
				.ToList();
			this.Client.IndexMany(projects, indexName);

			this.Client.Refresh(r => r.Index(indexName));
			

			//Now we kick of multiple percolations
			var multiPercolateResponse = this.Client.MultiPercolate(mp => mp
				//provding document in the percolate request 
				.Percolate<ElasticsearchProject>(perc=>perc
					.Index(indexName)
					.Document(projects.First())
				)
				//providing an existing document
				.Percolate<ElasticsearchProject>(perc=>perc
					.Index(indexName)
					.Id(projects.Last().Id)
				)
				//do a count percolation but force it to only
				//run on two of the 10 percolators
				.Count<ElasticsearchProject>(cp=>cp
					.Id(projects.Last().Id)
					.Index(indexName)
					.Query(ff=>
						ff.Term("order", 3)
						|| ff.Term("order", 4)
					)
				)
				//Force an error by providing a bogus type-name
				.Percolate<ElasticsearchProject>(perc=>perc
					.Index(indexName)
					.Type("bogus!")
					.Id(projects.Last().Id)
				)
			);

			multiPercolateResponse.IsValid.Should().BeTrue();

			var percolateResponses = multiPercolateResponse.Responses.ToList();
			percolateResponses.Should().NotBeEmpty().And.HaveCount(4);

			var percolateResponse = percolateResponses[0];
			percolateResponse.Total.Should().Be(10);
			percolateResponse.Matches.Should().HaveCount(10);

			var existingResponse = percolateResponses[1];
			existingResponse.Total.Should().Be(10);
			existingResponse.Matches.Should().HaveCount(10);

			var countResponse = percolateResponses[2];
			countResponse.Total.Should().Be(2);
			countResponse.Matches.Should().BeNull();

			var errorResponse = percolateResponses[3];
			errorResponse.Total.Should().Be(0);
			errorResponse.ServerError.Error.Should().NotBeNullOrWhiteSpace();

		}
	}
}

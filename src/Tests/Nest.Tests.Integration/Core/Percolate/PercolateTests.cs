using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Percolate
{
	[TestFixture]
	public class PercolateTests : IntegrationTests
	{
		[Test]
		public void Percolate_ReturnsExpectedResults()
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
				.Select(i => new ElasticsearchProject { Id = 1337 + i, Name = indexName, Content = "lorum ipsum"})
				.ToList();
			this.Client.IndexMany(projects, indexName);

			this.Client.Refresh(r => r.Index(indexName));
			

			//Now we kick of multiple percolations
				//provding document in the percolate request 
			var percolateResponse = this.Client.Percolate<ElasticsearchProject>(perc=>perc
				.Index(indexName)
				.Document(projects.First())
			);
				//providing an existing document
			var existingResponse = this.Client.Percolate<ElasticsearchProject>(perc=>perc
					.Index(indexName)
					.Id(projects.Last().Id)
			);
				//do a count percolation but force it to only
				//run on two of the 10 percolators
			var countResponse = this.Client.PercolateCount<ElasticsearchProject>(cp=>cp
				.Id(projects.Last().Id)
				.Index(indexName)
				.Query(ff=>
					ff.Term("order", 3)
					|| ff.Term("order", 4)
				)
			);
				//Force an error by providing a bogus indexname
			var errorResponse = this.Client.Percolate<ElasticsearchProject>(perc => perc
					.Index(indexName + "bogus!")
					.Id(projects.Last().Id)
			);


			percolateResponse.Total.Should().Be(10);
			percolateResponse.Matches.Should().HaveCount(10);

			existingResponse.Total.Should().Be(10);
			existingResponse.Matches.Should().HaveCount(10);

			countResponse.Total.Should().Be(2);

			errorResponse.Total.Should().Be(0);
			errorResponse.ServerError.Error.Should().NotBeNullOrWhiteSpace();

		}

		[Test]
		public void PercolateHighlighting()
		{

			//lets start fresh using a new index
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			IntegrationSetup.CreateTestIndex(this.Client, indexName);
			var property = Property.Path<ElasticsearchProject>(p => p.Content);
			// lets register several percolators in our new index that do a term match
			// on document name == indexname
			// we associate some metadata with the percolator so that we can later filter
			// the ones we want to execute easier
			foreach (var i in Enumerable.Range(0, 10))
			{
				var registerPercolator = this.Client.RegisterPercolator(new RegisterPercolatorRequest(indexName, "high-percolator-" + i)
				{
					Query = new MatchQuery
					{
						Field = property,
						Query = i % 2 == 0 ? "brown dog" : "lazy dog"
					}
				});
				registerPercolator.IsValid.Should().BeTrue();
			}

			var content = "The quick brown fox jumps over the lazy dog";

			var percolateResponse = this.Client.Percolate<ElasticsearchProject>(perc=>perc
				.Index(indexName)
				.Document(new ElasticsearchProject
				{
					Content = content
				})
				.Highlight(10, h=>h
					.OnFields(fd=>fd
						.OnField(p=>p.Content)
					)
				)
			);

			percolateResponse.Total.Should().Be(10);
			percolateResponse.Matches.Should().HaveCount(10);

			var field = percolateResponse.Infer.PropertyPath(property);
			foreach (var match in percolateResponse.Matches)
			{
				match.Highlight.Should().NotBeEmpty().And.ContainKey(field);
				match.Highlight[field].Should().NotBeEmpty()
					.And.OnlyContain(l => l.Any());
			}

			var multiPercolateResponse = this.Client.MultiPercolate(mp => mp
				.Percolate<ElasticsearchProject>(perc=>perc
					.Index(indexName)
					.Document(new ElasticsearchProject
					{
						Content = content
					})
					.Highlight(10, h=>h
						.OnFields(fd=>fd
							.OnField(p=>p.Content)
						)
					)
				)
			);
			multiPercolateResponse.IsValid.Should().BeTrue();
			multiPercolateResponse.Responses.Should().NotBeEmpty().And.HaveCount(1);
			percolateResponse = multiPercolateResponse.Responses.First();

			percolateResponse.Total.Should().Be(10);
			percolateResponse.Matches.Should().HaveCount(10);

			foreach (var match in percolateResponse.Matches)
			{
				match.Highlight.Should().NotBeEmpty().And.ContainKey(field);
				match.Highlight[field].Should().NotBeEmpty()
					.And.OnlyContain(l => l.Any());
			}

		}
	}
}

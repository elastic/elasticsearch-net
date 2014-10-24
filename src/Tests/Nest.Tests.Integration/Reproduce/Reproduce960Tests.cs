using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	public class Document
	{
		public string DocNumber { get; set;}
	}

	[TestFixture]
	public class Reproduce960Tests : IntegrationTests
	{
		[Test]
		public void PrefixQueryShouldReturnHits()
		{
			var settings = new IndexSettings();
			settings.NumberOfShards = 2;
			settings.Settings.Add("merge.policy.merge_factor", "5");
			settings.Settings.Add("search.slowlog.threshold.fetch.warn", "1s");

			var index = "issue960";

			if (this.Client.IndexExists(p => p.Index(index)).Exists)
			{
				this.Client.DeleteIndex(p => p.Index(index));
			}

			var createResponse = this.Client.CreateIndex(index, c => c
				.AddMapping<Document>(m => m
					.Properties(p => p
							.String(s => s.Name(d => d.DocNumber).Analyzer("keyword")
						)
					)
				)
			);
			createResponse.IsValid.Should().BeTrue();

			var document = new Document { DocNumber = "1.2345+t" };

			var indexResponse = this.Client.Index<Document>(document, i => i
				.Index(index)
				.Refresh()
			);
			indexResponse.IsValid.Should().BeTrue();

			var searchResponse = this.Client.Search<Document>(s => s
				.Index(index)
				.Type("document")
				.Query(q => q
					.Prefix(doc => doc.DocNumber, "1.2345+")
				)
			);

			searchResponse.IsValid.Should().BeTrue();
			searchResponse.Hits.Count().ShouldBeEquivalentTo(1);
		}
	}
}

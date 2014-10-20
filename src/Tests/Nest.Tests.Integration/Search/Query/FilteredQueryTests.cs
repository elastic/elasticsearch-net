using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class FilteredQueryTests : IntegrationTests
	{
		[Test]
		public void QueryIsConditionless()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Filtered(ff => ff
						.Query(qq => qq
							.Match(m => m
								.OnField(p => p.Name)
								.Query(null)
							)
						)
						.Filter(f => f
							.Term(p => p.Id, 1)
						)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.ShouldBeEquivalentTo(1);
		}

		[Test]
		public void FilterIsConditionless()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Filtered(ff => ff
						.Query(qq => qq
							.Match(m => m
								.OnField(p => p.Name)
								.Query("elasticsearch")
							)
						)
						.Filter(f => f
							.Term(p => p.Id, null)
						)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.Should().BeGreaterThan(1);
		}

		[Test]
		public void QueryAndFilterAreConditionless()
		{
			var response = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.Filtered(ff => ff
						.Query(qq => qq
							.Match(m => m
								.OnField(p => p.Name)
								.Query(null)
							)
						)
						.Filter(f => f
							.Term(p => p.Id, null)
						)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.Should().BeGreaterThan(10);
		}
	}
}

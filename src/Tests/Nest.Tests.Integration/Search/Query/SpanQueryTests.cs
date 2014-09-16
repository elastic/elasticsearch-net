using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class SpanQueryTests : IntegrationTests
	{
		[Test]
		public void SpanMultiTermQuery()
		{
			var r = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.SpanMultiTerm(sp => sp
						.Match(m => m
							.Prefix(p => p
								.OnField(ep => ep.Name)
								.Value("NEST")
							)
						)
					)
				)
			);

			r.IsValid.Should().BeTrue();
		}
	}
}

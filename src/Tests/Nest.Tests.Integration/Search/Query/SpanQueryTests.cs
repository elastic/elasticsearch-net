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

		[Test]
		public void SpanNearQuery()
		{
			var r = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.SpanNear(sn => sn
						.Clauses(cl => cl
							.SpanTerm(p => p.Name, "NEST")
							.SpanTerm(p => p.Name, "elasticsearch")
						)
						.Slop(12)
						.InOrder(false)
						.CollectPayloads(false)
						.Boost(1.1)
					)
				)
			);

			r.IsValid.Should().BeTrue();
		}

		[Test]
		public void SpanFirstQuery()
		{
			var r = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.SpanFirst(sf => sf
						.Match(m => m.SpanTerm(p => p.Name, "NEST"))
						.End(3)
						.Boost(1.1)
					)
				)
			);

			r.IsValid.Should().BeTrue();
		}

		[Test]
		public void SpanNotQuery()
		{
			var r = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.SpanNot(sn => sn
						.Include(i => i
							.SpanTerm(p => p.Name, "NEST")
						)
						.Exclude(e => e
							.SpanNear(sp => sp
								.Clauses(cl => cl
									.SpanTerm(p => p.Name, "NEST")
									.SpanTerm(p => p.Name, "elasticsearch")
								)
								.Slop(0)
								.InOrder(true)
								.Boost(1.2)
							)
						)
						.Boost(1.1)
					)
				)
			);

			r.IsValid.Should().BeTrue();
		}

		[Test]
		public void SpanOrQuery()
		{
			var r = Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.SpanOr(so => so
						.Clauses(cl => cl
							.SpanTerm(p => p.Name, "NEST")
							.SpanTerm(p => p.Name, "elasticsearch")
						)
						.Boost(1.1)
					)
				)
			);

			r.IsValid.Should().BeTrue();
		}
	}
}

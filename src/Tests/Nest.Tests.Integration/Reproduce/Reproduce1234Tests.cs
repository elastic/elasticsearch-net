using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1234Tests : IntegrationTests
	{
		[Test]
		public void BoostFieldPositiveDoesNotSerialize()
		{
			var result = _elasticClient.Search<SearchablePage>(new SearchDescriptor<SearchablePage>()
						.Query(q => q
							.Boosting(b => b
								.NegativeBoost(0.2)
								.Positive(p => p
									.Filtered(fi => fi
										.Filter(fq => fq
											.Term(t => t.Type, aggregation)
										)
										.Query(qq => qq
											.QueryString(qs => qs
												.Query(query)
												.OnFields(f => f.Name, f => f.Presentation, f => f.MainBody, f => f.SearchableBlocks.First().MainBody)
											)
										)
									)
								)
								.Negative(n => n
									.Term(f => f.OnField("type").Value("Press"))
								)
							)
						)
        }
	}
}

using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class SpanMultiTermQueryJson : BaseJsonTests
	{
		[Test]
		public void SpanMultiTermQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.SpanMultiTerm(sp => sp
						.Match(m => m
							.Prefix(p => p
								.OnField(ep => ep.Name)
								.Value("NEST")
							)
						)
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{
				span_multi:{
					match:{
						prefix : { name :  { value : ""NEST"" } }
					}
				}
			}}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}

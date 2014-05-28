using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.InitializerSyntax
{
	[TestFixture]
	public class InitializerExample : BaseJsonTests
	{
		[Test]
		public void RescoreSerializes()
		{
			QueryContainer query = new TermQuery()
			{
				Field = Property.Path<ElasticsearchProject>(p=>p.Name),
				Value = "value"
			} && new PrefixQuery()
			{
				Field = "prefix_field", 
				Value = "prefi", 
				Rewrite = RewriteMultiTerm.constant_score_boolean
			};
//Aggregations = new Dictionary<string, IAggregationContainer>
//				{
//					{ "my_agg", new AggregationContainer
//					{
//						Terms = new TermsAggregator
//						{
//							Field = Property.Path<ElasticsearchProject>(p=>p.Name),
//							Size = 10,
//							ExecutionHint = TermsAggregationExecutionHint.ordinals,
//							Aggregations = new Dictionary<string, IAggregationContainer>
//							{
//								{ "max_count", new AggregationContainer()
//								{
//									Max = new MaxAggregator()
//									{
//										Field = "loc"
//									}
//								}
//								}
//							}
//						}
//					}}
//				}
			var result = _client.Search<ElasticsearchProject>(new SearchRequest
			{
				From = 0,
				Size = 20,
				Explain = true,
				Fields = new[]
				{
					"field",
					Property.Path<ElasticsearchProject>(p=>p.Name)
				},
				Query = query,
				Filter = new FilterContainer
				{
					
				}
			});
		}

	}
}

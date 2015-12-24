using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1435Tests : BaseJsonTests
	{
		[Test]
		public void UsingExplicitCasts()
		{
			var aggregations = new Dictionary<string, IAggregationContainer>
			{
				{
					//Aggregators missing implicit casts to AggregationContainer
					"my_aggregation", new AggregationContainer
					{
						Filter = new FilterAggregator
						{
							Filter = (FilterContainer)new QueryFilter
							{
								Query = (QueryContainer)new QueryStringQuery
								{
									Query = "Just an example"
								}
							}
						}
					}
				}
			};

			var result = this._client.Search<ElasticsearchProject>(new SearchRequest<ElasticsearchProject>
			{
				Aggregations = aggregations
			});

			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod(), "OISAggs");
		}

		[Test]
		public void UsingConstructor()
		{
			var aggregations = new Dictionary<string, IAggregationContainer>
			{
				{
					//AggregationContaner should accept aggregators in constructor
					"my_aggregation", new AggregationContainer
					{
						Filter = new FilterAggregator
						{
							Filter = new FilterContainer(new QueryFilter
							{
								Query = new QueryContainer(new QueryStringQuery
								{
									Query = "Just an example"
								})
							})
						}
					}
				}
			};

			var result = this._client.Search<ElasticsearchProject>(new SearchRequest<ElasticsearchProject>
			{
				Aggregations = aggregations
			});

			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod(), "OISAggs");
		}

		[Test]
		public void UsingToContainer()
		{
			var aggregations = new Dictionary<string, IAggregationContainer>
			{
				{
					"my_aggregation", new AggregationContainer
					{
						Filter = new FilterAggregator
						{
							Filter = new QueryFilter
							{
								Query = new QueryStringQuery
								{
									Query = "Just an example"
								}.ToContainer()
							}.ToContainer()
						} //Aggregator is missing ToContainer()
					}
				}
			};

			var result = this._client.Search<ElasticsearchProject>(new SearchRequest<ElasticsearchProject>
			{
				Aggregations = aggregations
			});

			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod(), "OISAggs");
		}

	}
}

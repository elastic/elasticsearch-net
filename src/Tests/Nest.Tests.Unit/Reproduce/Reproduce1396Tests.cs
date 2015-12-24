using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1396Tests : BaseJsonTests
	{
		[JsonConverter(typeof(CustomJsonConverter))]
		public class TermOrder : ICustomJson
		{
			public string Key { get; set; }
			public SortOrder Order { get; set; }
			public object GetCustomJson()
			{
				return new Dictionary<string, SortOrder> {{this.Key, this.Order}};
			}
		}


		public class MyTermsAggregationFix : TermsAggregator
		{
			[JsonProperty("order")]
			public new List<TermOrder> TermsOrder { get; set; }
		}

		public class MyTermsAggregationsFluentFix<T> : TermsAggregationDescriptor<T>
			where T : class
		{
			[JsonProperty("order")]
			internal List<TermOrder> TermsOrder { get; set; }

			public MyTermsAggregationsFluentFix<T> PatchedOrder(List<TermOrder> order)
			{
				this.TermsOrder = order;
				return this;
			}

		}


		[Test]
		public void FixDslThroughCustomInterfaceImplementation()
		{
			var order = new List<TermOrder>()
			{
				new TermOrder { Key = "x", Order = SortOrder.Ascending},
				new TermOrder { Key = "y", Order = SortOrder.Descending}
			};
			var agg = new MyTermsAggregationFix
			{
				Field = "x",
				TermsOrder = order
			};
			var search = new SearchRequest
			{
				Aggregations = new Dictionary<string, IAggregationContainer>
				{
					{
						"my_terms_agg", new AggregationContainer
						{
							Terms = agg
						}
					}
				}
			};
			this.JsonEquals(search, MethodBase.GetCurrentMethod(), "PatchedTermsAggSort");
		}

		[Test]
		public void FixDslThroughCustomInterfaceImplementationUsingFluent()
		{
			var order = new List<TermOrder>()
			{
				new TermOrder { Key = "x", Order = SortOrder.Ascending},
				new TermOrder { Key = "y", Order = SortOrder.Descending}
			};
			var search = new SearchDescriptor<ElasticsearchProject>()
				.Aggregations(aggs=>aggs
					.Terms("my_terms_agg", t=> new MyTermsAggregationsFluentFix<ElasticsearchProject>()
						.PatchedOrder(order)
						.Field("x")
					)
				)
				
			;
			this.JsonEquals(search, MethodBase.GetCurrentMethod(), "PatchedTermsAggSort");
		}

	}
}

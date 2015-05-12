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
			var serialized = _client.Serializer.Serialize(search).Utf8String();
			this.JsonEquals(search, MethodBase.GetCurrentMethod());
		}
	}
}

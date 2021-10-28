// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl.TermLevel.Wildcard
{
	public class RangeSerialisationTests
	{
		[U]
		public void DeserialisesAndSerialises()
		{
			// This test validates that a response from SQL translate can be used in the seubsequent query
			// The RangeQueryBuilder currently returns from, to, include_lower and include_upper which are considered deprecated internally, but we have to handle them until the builder is revised.
			// See: https://github.com/elastic/elasticsearch/issues/48538

			var translateResponse = @"{""size"":1000,""query"":{""bool"":{""must"":[{""range"":{""customer_id"":{""from"":""1"",""to"":null,""include_lower"":false,""include_upper"":false,""boost"":1}}}],""adjust_pure_negative"":true,""boost"":1}}}";

			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(Encoding.UTF8.GetBytes(translateResponse)));
			var client = new ElasticClient(settings);

			var response = client.Sql.Translate();

			IQueryContainer queryContainer = response.Result.Query;

			queryContainer.Bool.Should().NotBeNull();
			var clauses = queryContainer.Bool.Must.ToList();
			queryContainer = clauses.Single();
			var rangeQuery = queryContainer.Range.Should().BeOfType<TermRangeQuery>().Subject; // Expecting term query since the `from` value is provided within quotes.

#pragma warning disable CS0618 // Type or member is obsolete
			rangeQuery.From.Should().Be("1");
			rangeQuery.To.Should().BeNull();
			rangeQuery.IncludeLower.Should().Be(false);
			rangeQuery.IncludeUpper.Should().Be(false);
#pragma warning restore CS0618 // Type or member is obsolete

			var stream = new MemoryStream();
			client.ConnectionSettings.RequestResponseSerializer.Serialize(response.Result, stream);
			stream.Position = 0;
			var reader = new StreamReader(stream);
			var json = reader.ReadToEnd();

			// note: adjust_pure_negative is not recommended
			json.Should().Be(@"{""query"":{""bool"":{""must"":[{""range"":{""customer_id"":{""from"":""1"",""include_lower"":false,""include_upper"":false,""boost"":1.0}}}],""boost"":1.0}},""size"":1000}");
		}
	}
}

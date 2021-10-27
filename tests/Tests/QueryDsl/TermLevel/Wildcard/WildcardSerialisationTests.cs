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
	public class WildcardSerialisationTests
	{
		[U]
		public void DeserialisesAndSerialises()
		{
			// This test validates that a response from SQL translate can be used in the seubsequent query
			// The WildcardQueryBuilder prefers the `wildcard` field over the `value` field.

			var translateResponse = @"{""size"":1000,""query"":{""bool"":{""must"":[{""wildcard"":{""customershortnm.keyword"":{""wildcard"":""*B*"",""boost"":1}}}],""adjust_pure_negative"":true,""boost"":1}}}";

			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(Encoding.UTF8.GetBytes(translateResponse)));
			var client = new ElasticClient(settings);

			var response = client.Sql.Translate();

			IQueryContainer queryContainer = response.Result.Query;

			queryContainer.Bool.Should().NotBeNull();
			var clauses = queryContainer.Bool.Must.ToList();
			queryContainer = clauses.Single();
			queryContainer.Wildcard.Wildcard.Should().Be("*B*");

			var stream = new MemoryStream();
			client.ConnectionSettings.RequestResponseSerializer.Serialize(response.Result, stream);
			stream.Position = 0;
			var reader = new StreamReader(stream);
			var json = reader.ReadToEnd();

			// note: adjust_pure_negative is not recommended
			json.Should().Be(@"{""query"":{""bool"":{""must"":[{""wildcard"":{""customershortnm.keyword"":{""wildcard"":""*B*"",""boost"":1.0}}}],""boost"":1.0}},""size"":1000}");
		}
	}
}

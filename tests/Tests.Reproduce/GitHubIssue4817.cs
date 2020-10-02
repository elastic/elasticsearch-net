// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4817
	{
		[U]
		public void DeserializeCaseInsensitiveEnum()
		{
			var json = "{\"default_operator\":\"AND\",\"query\":\"Hans Mueller\"}";
			var bytes = Encoding.UTF8.GetBytes(json);
			var elasticClient = new ElasticClient();

			Func<QueryStringQuery> func = () => elasticClient.RequestResponseSerializer.Deserialize<QueryStringQuery>(new MemoryStream(bytes));

			var query = func.Should().NotThrow().Subject;
			query.DefaultOperator.Should().Be(Operator.And);
		}
	}
}

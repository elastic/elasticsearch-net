// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue4703
	{
		[U]
		public void NullableValueTupleDoesNotThrow()
		{
			var connectionSettings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();
			var client = new ElasticClient(connectionSettings);

			Func<IndexResponse> action = () =>
				client.Index(
					new ExampleDoc
					{
						tupleNullable = ("somestring", 42),
					}, i => i.Index("index"));

			var a = action.Should().NotThrow();
			var response = a.Subject;

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Be(@"{""tupleNullable"":{""Item1"":""somestring"",""Item2"":42}}");
		}
	}

	public class ExampleDoc
	{
		// ReSharper disable once InconsistentNaming
		public (string info, int number)? tupleNullable { get; set; }
	}
}

using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit.ObjectInitializer.TermVector
{
	public class TermVectorWithDocRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public TermVectorWithDocRequestTests()
		{
			var response = this._client.TermVector<ElasticsearchProject>(t=>t
				.Offsets()
				.Payloads()
				.Positions()
				.TermStatistics()
				.FieldStatistics()
				.Document(new { world = "doc doc doc"})
			);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith(
				"/nest_test_data/elasticsearchprojects/_termvector?offsets=true&payloads=true&positions=true&term_statistics=true&field_statistics=true");
			this._status.RequestMethod.Should().Be("POST");
			var req  =this._status.Request.Utf8String();
			req.JsonEquals(@"{ doc: { world: ""doc doc doc"" } }")
				.Should().BeTrue("{0}", req);

		}
	}
}

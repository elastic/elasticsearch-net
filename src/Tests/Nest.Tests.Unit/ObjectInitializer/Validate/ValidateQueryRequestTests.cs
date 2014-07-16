using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nest.Tests.Unit.ObjectInitializer.Validate
{
	[TestFixture]
	public class ValidateQueryRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ValidateQueryRequestTests()
		{
			QueryContainer query = new MatchQuery
			{
				Field = "field",
				Query = "value"
			};

			var request = new ValidateQueryRequest
			{
				Query = query
			};

			var response = this._client.Validate(request);
			_status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_validate/query");
			this._status.RequestMethod.Should().Be("POST");
		}

		[Test]
		public void ValidateQueryBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}

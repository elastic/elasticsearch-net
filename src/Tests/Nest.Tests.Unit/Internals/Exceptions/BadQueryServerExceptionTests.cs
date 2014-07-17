using System.Reflection;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using System;
using Elasticsearch.Net;

namespace Nest.Tests.Unit.Internals.Exceptions
{
	[TestFixture]
	public class BadQueryServerExceptionTests : BaseJsonTests
	{
		[Test]
		public void ReturnedServerExceptionIsAvailableInFull()
		{
			var client = this.GetFixedReturnClient(MethodBase.GetCurrentMethod(), "BadQuery", 400);

			//this always returns as if the query was malfored because we inject the returned
			//response in the line above
			var result = client.Search<ElasticsearchProject>(q => q.MatchAll());

			var error = result.ServerError;

			error.Should().NotBeNull();

			error.ExceptionType.Should().Be("SearchPhaseExecutionException");
			error.Status.Should().Be(400);
			error.Error.Should().StartWith("Failed to execute phase [query]");
			error.Error.Should().EndWith("}");
		}

		[Test]
		public async void ReturnedServerExceptionIsAvailableInFull_Async()
		{
			var client = this.GetFixedReturnClient(MethodBase.GetCurrentMethod(), "BadQuery", 400);

			//this always returns as if the query was malfored because we inject the returned
			//response in the line above
			var result = await client.SearchAsync<ElasticsearchProject>(q => q.MatchAll());

			var error = result.ServerError;

			error.Should().NotBeNull();

			error.ExceptionType.Should().Be("SearchPhaseExecutionException");
			error.Status.Should().Be(400);
			error.Error.Should().StartWith("Failed to execute phase [query]");
			error.Error.Should().EndWith("}");
		}

		[Test]
		public void ReturnedServerExceptionIsAvailableInFull_NotExposingRawResponse()
		{
			var client = this.GetFixedReturnClient(MethodBase.GetCurrentMethod(), "BadQuery", 400, s=> s.ExposeRawResponse(false));

			//this always returns as if the query was malfored because we inject the returned
			//response in the line above
			var result = client.Search<ElasticsearchProject>(q => q.MatchAll());

			var error = result.ServerError;

			error.Should().NotBeNull();

			error.ExceptionType.Should().Be("SearchPhaseExecutionException");
			error.Status.Should().Be(400);
			error.Error.Should().StartWith("Failed to execute phase [query]");
			error.Error.Should().EndWith("}");
		}

		[Test]
		public async void ReturnedServerExceptionIsAvailableInFull_NotExposingRawResponse_Async()
		{
			var client = this.GetFixedReturnClient(MethodBase.GetCurrentMethod(), "BadQuery", 400, s=> s.ExposeRawResponse(false));

			//this always returns as if the query was malfored because we inject the returned
			//response in the line above
			var result = await client.SearchAsync<ElasticsearchProject>(q => q.MatchAll());

			var error = result.ServerError;

			error.Should().NotBeNull();

			error.ExceptionType.Should().Be("SearchPhaseExecutionException");
			error.Status.Should().Be(400);
			error.Error.Should().StartWith("Failed to execute phase [query]");
			error.Error.Should().EndWith("}");
		}

	}
}
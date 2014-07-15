using System;
using System.IO;
using System.Text;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class ElasticsearchServerExceptions
	{
		//we do not pass a Uri or IConnectionPool so this config
		//defaults to SingleNodeConnectionPool()
		private readonly ConnectionConfiguration _connectionConfig = new ConnectionConfiguration()
			.MaximumRetries(0);


		private MemoryStream CreateServerExceptionResponse(int status, string exceptionType, string exceptionMessage)
		{
			var format = @"{{ ""status"": {0}, ""error"" : ""{1}[{2}]"" }}";
			var bytes = Encoding.UTF8.GetBytes(string.Format(format, status, exceptionType, exceptionMessage));
			var stream = new MemoryStream(bytes);
			return stream;
		}

		[Test]
		[TestCase(505, "SomeException", "Some Error Message")]
		[TestCase(505, "", "")]
		public void ServerExceptionIsCaught_DiscardResponse(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionConfiguration = new ConnectionConfiguration()
					.ExposeRawResponse(false);

				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(connectionConfiguration, response: response));

				var client = fake.Resolve<ElasticsearchClient>();

				var result = client.Info();
				result.Success.Should().BeFalse();
				AssertServerErrorsOnResponse(result, status, exceptionType, exceptionMessage);

				result.ResponseRaw.Should().BeNull();

				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}

		[Test]
		[TestCase(505, "SomeException", "Some Error Message")]
		[TestCase(505, "", "")]
		public void ServerExceptionIsCaught_KeepResponse(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionConfiguration = new ConnectionConfiguration()
					.ExposeRawResponse(true);

				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Bad(connectionConfiguration, response: response));

				var client = fake.Resolve<ElasticsearchClient>();

				var result = client.Info();
				result.Success.Should().BeFalse();
				AssertServerErrorsOnResponse(result, status, exceptionType, exceptionMessage);

				result.ResponseRaw.Should().NotBeNull();

				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		private static void AssertServerErrorsOnResponse(
			ElasticsearchResponse<DynamicDictionary> result, int status, string exceptionType, string exceptionMessage)
		{
			var serverException = result.OriginalException as ElasticsearchServerException;
			serverException.Should().NotBeNull();
			serverException.ExceptionType.Should().Be(exceptionType);
			serverException.Message.Should().Be(exceptionMessage);
			serverException.Status.Should().Be(status);

			var serverError = result.ServerError;
			serverError.Should().NotBeNull();
			serverError.ExceptionType.Should().Be(exceptionType);
			serverError.Error.Should().Be(exceptionMessage);
			serverError.Status.Should().Be(status);

		}
	}
}
using System;
using System.IO;
using System.Text;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class NoRetryOnServerExceptionTests
	{
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
		[TestCase(404, "", "")]
		public void IfResponseIsKnowError_DoNotRetry_ThrowServerException(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionPool = new StaticConnectionPool(new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
				});
				var connectionConfiguration = new ConnectionConfiguration(connectionPool)
					.ThrowOnElasticsearchServerExceptions()
					.ExposeRawResponse(false);

				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevel(fake);
				pingCall.Returns(FakeResponse.Ok(connectionConfiguration));

				var getCall = FakeCalls.GetSyncCall(fake);
				getCall.Returns(FakeResponse.Any(connectionConfiguration, status, response: response));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(()=>client.Info());
				AssertServerErrorsOnResponse(e, status, exceptionType, exceptionMessage);
				
				//make sure a know ElasticsearchServerException does not cause a retry
				//In this case we want to fail early

				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}

		[Test]
		[TestCase(505, "SomeException", "Some Error Message")]
		[TestCase(505, "", "")]
		[TestCase(404, "", "")]
		public void IfResponseIsKnowError_DoNotRetry_ThrowServerException_Async(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				var connectionPool = new StaticConnectionPool(new[]
				{
					new Uri("http://localhost:9200"),
					new Uri("http://localhost:9201"),
				});
				var connectionConfiguration = new ConnectionConfiguration(connectionPool)
					.ThrowOnElasticsearchServerExceptions()
					.ExposeRawResponse(false);

				fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
				FakeCalls.ProvideDefaultTransport(fake);

				var pingCall = FakeCalls.PingAtConnectionLevelAsync(fake);
				pingCall.Returns(FakeResponse.OkAsync(connectionConfiguration));

				var getCall = FakeCalls.GetCall(fake);
				getCall.Returns(FakeResponse.AnyAsync(connectionConfiguration, status, response: response));

				var client = fake.Resolve<ElasticsearchClient>();

				var e = Assert.Throws<ElasticsearchServerException>(async ()=>await client.InfoAsync());
				AssertServerErrorsOnResponse(e, status, exceptionType, exceptionMessage);

				//make sure a know ElasticsearchServerException does not cause a retry
				//In this case we want to fail early

				getCall.MustHaveHappened(Repeated.Exactly.Once);

			}
		}
		
		private static void AssertServerErrorsOnResponse(
			ElasticsearchServerException serverException, int status, string exceptionType, string exceptionMessage)
		{
			serverException.Should().NotBeNull();
			serverException.ExceptionType.Should().Be(exceptionType);
			serverException.Message.Should().Be(exceptionMessage);
			serverException.Status.Should().Be(status);
		}
	}
}
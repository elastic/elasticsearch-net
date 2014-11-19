using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Exceptions
{
	[TestFixture]
	public class ThrownElasticsearchServerExceptions
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
		public void ServerExceptionIsCaught_DiscardResponse(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				this.Call(status, exceptionType, exceptionMessage, fake, response);
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
				this.Call(status, exceptionType, exceptionMessage, fake, response, exposeRawResponse: true);
			}
		}

		[Test]
		[TestCase(505, "SomeException", "Some Error Message")]
		[TestCase(505, "", "")]
		public void ServerExceptionIsCaught_DiscardResponse_Async(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				this.CallAsync(status, exceptionType, exceptionMessage, fake, response);
			}
		}

		[Test]
		[TestCase(505, "SomeException", "Some Error Message")]
		[TestCase(505, "", "")]
		public void ServerExceptionIsCaught_KeepResponse_Async(int status, string exceptionType, string exceptionMessage)
		{
			var response = CreateServerExceptionResponse(status, exceptionType, exceptionMessage);
			using (var fake = new AutoFake(callsDoNothing: true))
			{
				this.CallAsync(status, exceptionType, exceptionMessage, fake, response, exposeRawResponse: true);
			}
		}

		private void Call(int status, string exceptionType, string exceptionMessage, AutoFake fake, MemoryStream response, bool exposeRawResponse = false)
		{
			var connectionConfiguration = new ConnectionConfiguration()
				.ThrowOnElasticsearchServerExceptions()
				.ExposeRawResponse(exposeRawResponse);

			fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			FakeCalls.ProvideDefaultTransport(fake);

			var getCall = FakeCalls.GetSyncCall(fake);
			getCall.Returns(FakeResponse.Bad(connectionConfiguration, response: response));

			var client = fake.Resolve<ElasticsearchClient>();
			
			var e = Assert.Throws<ElasticsearchServerException>(()=>client.Info());
			AssertServerErrorsException(e, status, exceptionType, exceptionMessage);
			getCall.MustHaveHappened(Repeated.Exactly.Once);
		}

		private void CallAsync(int status, string exceptionType, string exceptionMessage, AutoFake fake, MemoryStream response, bool exposeRawResponse = false)
		{
			var connectionConfiguration = new ConnectionConfiguration()
				.ThrowOnElasticsearchServerExceptions()
				.ExposeRawResponse(exposeRawResponse);

			fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			FakeCalls.ProvideDefaultTransport(fake);

			var getCall = FakeCalls.GetCall(fake);
			getCall.Returns(FakeResponse.BadAsync(connectionConfiguration, response: response));

			var client = fake.Resolve<ElasticsearchClient>();

			var e = Assert.Throws<ElasticsearchServerException>(async ()=>await client.InfoAsync());
			AssertServerErrorsException(e, status, exceptionType, exceptionMessage);
			getCall.MustHaveHappened(Repeated.Exactly.Once);
		}

		private static void AssertServerErrorsException(ElasticsearchServerException serverException, int status, string exceptionType, string exceptionMessage)
		{
			serverException.Should().NotBeNull();
			serverException.ExceptionType.Should().Be(exceptionType);
			serverException.Message.Should().Be(exceptionMessage);
			serverException.Status.Should().Be(status);

		}
	}
}
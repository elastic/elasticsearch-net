using System;
using System.Globalization;
using System.IO;
using System.Text;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Responses.Helpers
{
	public class Requester<T> : IDisposable where T : class
	{
		public Requester(
			object responseValue,
			Func<ConnectionConfiguration, ConnectionConfiguration> configSetup,
			Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> responseSetup,
			Func<IElasticsearchClient, ElasticsearchResponse<T>> call = null
			)
		{
			var responseStream = CreateServerExceptionResponse(responseValue);
			this.Fake = new AutoFake(callsDoNothing: true);
			var connectionConfiguration = configSetup(new ConnectionConfiguration());
			var response = responseSetup(connectionConfiguration, responseStream);
			this.Fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			FakeCalls.ProvideDefaultTransport(this.Fake);

			this.GetCall = FakeCalls.GetSyncCall(this.Fake);
			this.GetCall.Returns(response);

			var client = this.Fake.Resolve<ElasticsearchClient>();
			this.Result = call != null ? call(client) : client.Info<T>();

			this.GetCall.MustHaveHappened(Repeated.Exactly.Once);

		}

		public ElasticsearchResponse<T> Result { get; set; }

		public IReturnValueConfiguration<ElasticsearchResponse<Stream>> GetCall { get; set; }

		public AutoFake Fake { get; set; }

		private MemoryStream CreateServerExceptionResponse(object responseValue)
		{
			if (responseValue is string)
				responseValue = string.Format(CultureInfo.InvariantCulture, @"""{0}""", responseValue);
			var format = @"{{ ""value"": {0} }}";
			this.ResponseBytes = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, format, responseValue));
			var stream = new MemoryStream(this.ResponseBytes);
			return stream;
		}

		public byte[] ResponseBytes { get; set; }

		public void Dispose()
		{
			if (this.Fake != null) this.Fake.Dispose();
		}
	}
}
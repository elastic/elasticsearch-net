using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Responses.Helpers
{
	public class AsyncRequester<T> : IDisposable where T : class
	{
		private readonly object _responseValue;
		private readonly Func<ConnectionConfiguration, ConnectionConfiguration> _configSetup;
		private readonly Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> _responseSetup;
		private readonly Func<IElasticsearchClient, Task<ElasticsearchResponse<T>>> _call;

		public AsyncRequester(
			object responseValue,
			Func<ConnectionConfiguration, ConnectionConfiguration> configSetup,
			Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> responseSetup,
			Func<IElasticsearchClient, Task<ElasticsearchResponse<T>>> call = null
			)
		{
			_responseValue = responseValue;
			_configSetup = configSetup;
			_responseSetup = responseSetup;
			_call = call;
		}

		public async Task Init() {
			var responseStream = CreateServerExceptionResponse(_responseValue);
			this.Fake = new AutoFake(callsDoNothing: true);
			var connectionConfiguration = _configSetup(new ConnectionConfiguration());
			var response = _responseSetup(connectionConfiguration, responseStream);
			this.Fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			FakeCalls.ProvideDefaultTransport(this.Fake);

			this.GetCall = FakeCalls.GetCall(this.Fake);
			this.GetCall.Returns(response);

			var client = this.Fake.Resolve<ElasticsearchClient>();
			this.Result = await (_call != null ? _call(client) : client.InfoAsync<T>());

			this.GetCall.MustHaveHappened(Repeated.Exactly.Once);

		}

		public ElasticsearchResponse<T> Result { get; set; }

		public IReturnValueConfiguration<Task<ElasticsearchResponse<Stream>>> GetCall { get; set; }

		public AutoFake Fake { get; set; }

		private MemoryStream CreateServerExceptionResponse(object responseValue)
		{
			if (responseValue is string)
				responseValue = string.Format(CultureInfo.InvariantCulture, @"""{0}""", responseValue);
			var format = @"{{ ""value"": {0} }}";
			var json = string.Format(CultureInfo.InvariantCulture, format, responseValue);
			this.ResponseBytes = Encoding.UTF8.GetBytes(json);
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
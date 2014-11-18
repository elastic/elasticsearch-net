using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Memory.Helpers
{
	public class AsyncMemorySetup<T> : IDisposable, IMemorySetup<T> where T : class
	{
		private readonly object _responseValue;
		private readonly Func<ConnectionConfiguration, ConnectionConfiguration> _configSetup;
		private readonly Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> _responseSetup;
		private readonly Func<IElasticsearchClient, Task<ElasticsearchResponse<T>>> _call;

		private readonly List<TrackableMemoryStream> _createdMemoryStreams = new List<TrackableMemoryStream>();
		public List<TrackableMemoryStream> CreatedMemoryStreams { get { return _createdMemoryStreams;  } }
		public TrackableMemoryStream ResponseStream { get; private set; }
		public IMemoryStreamProvider MemoryProvider { get; set; }
		public ElasticsearchResponse<T> Result { get; set; }
		public IReturnValueArgumentValidationConfiguration<Task<ElasticsearchResponse<Stream>>> GetCall { get; set; }
		public AutoFake Fake { get; set; }
		public byte[] ResponseBytes { get; set; }


		public AsyncMemorySetup(
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

		public async Task<AsyncMemorySetup<T>> Init()
		{
			this.Fake = new AutoFake(callsDoNothing: true);
			var connectionConfiguration = _configSetup(new ConnectionConfiguration());
			this.ResponseStream = CreateServerExceptionResponse(_responseValue);
			var response = _responseSetup(connectionConfiguration, this.ResponseStream);
			this.Fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			this.MemoryProvider = this.Fake.Resolve<IMemoryStreamProvider>();
			A.CallTo(() => this.MemoryProvider.New()).ReturnsLazily((o) =>
			{
				var memoryStream = new TrackableMemoryStream();
				this._createdMemoryStreams.Add(memoryStream);
				return memoryStream;
			});

			FakeCalls.ProvideDefaultTransport(this.Fake, memoryStreamProvider: this.MemoryProvider);

			this.GetCall = FakeCalls.GetCall(this.Fake);
			this.GetCall.Returns(response);

			var client = this.Fake.Resolve<ElasticsearchClient>();
			this.Result = await (_call != null ? _call(client) : client.InfoAsync<T>());

			this.GetCall.MustHaveHappened(Repeated.Exactly.Once);
			return this;
		} 

		protected TrackableMemoryStream CreateServerExceptionResponse(object responseValue)
		{
			if (responseValue is string)
				responseValue = string.Format(CultureInfo.InvariantCulture, @"""{0}""", responseValue);
			var format = @"{{ ""value"": {0} }}";
			this.ResponseBytes = Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, format, responseValue));
			var stream = new TrackableMemoryStream(this.ResponseBytes);
			return stream;
		}

		public void Dispose()
		{
			if (this.Fake != null) this.Fake.Dispose();
		}
	}
}
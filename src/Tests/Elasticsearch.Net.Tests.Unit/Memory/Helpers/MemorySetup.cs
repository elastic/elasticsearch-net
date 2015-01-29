using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Memory.Helpers
{
	public class MemorySetup<T> : IDisposable, IMemorySetup<T> where T : class
	{
		private readonly List<TrackableMemoryStream> _createdMemoryStreams = new List<TrackableMemoryStream>();
		public List<TrackableMemoryStream> CreatedMemoryStreams { get { return _createdMemoryStreams;  } }
		public TrackableMemoryStream ResponseStream { get; private set; }
		public IMemoryStreamProvider MemoryProvider { get; set; }
		public ElasticsearchResponse<T> Result { get; set; }
		public IReturnValueConfiguration<ElasticsearchResponse<Stream>> GetCall { get; set; }
		public AutoFake Fake { get; set; }
		public byte[] ResponseBytes { get; set; }


		public MemorySetup(
			object responseValue,
			Func<ConnectionConfiguration, ConnectionConfiguration> configSetup,
			Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> responseSetup,
			Func<IElasticsearchClient, ElasticsearchResponse<T>> call = null
			)
		{
			this.Fake = new AutoFake(callsDoNothing: true);
			var connectionConfiguration = configSetup(new ConnectionConfiguration());
			this.ResponseStream = CreateServerExceptionResponse(responseValue);
			var response = responseSetup(connectionConfiguration, this.ResponseStream);
			this.Fake.Provide<IConnectionConfigurationValues>(connectionConfiguration);
			this.MemoryProvider = this.Fake.Resolve<IMemoryStreamProvider>();
			A.CallTo(() => this.MemoryProvider.New()).ReturnsLazily((o) =>
			{
				var memoryStream = new TrackableMemoryStream();
				this._createdMemoryStreams.Add(memoryStream);
				return memoryStream;
			});

			FakeCalls.ProvideDefaultTransport(this.Fake, memoryStreamProvider: this.MemoryProvider);

			this.GetCall = FakeCalls.GetSyncCall(this.Fake);
			this.GetCall.Returns(response);

			var client = this.Fake.Resolve<ElasticsearchClient>();
			this.Result = call != null ? call(client) : client.Info<T>();

			this.GetCall.MustHaveHappened(Repeated.Exactly.Once);

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
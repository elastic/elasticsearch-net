using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.FakeItEasy;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FakeItEasy;
using FakeItEasy.Configuration;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Connection
{
	[TestFixture]
	public class BuildInResponseAsyncTests
	{
		private class Requester<T> : IDisposable where T : class
		{
			private readonly object _responseValue;
			private readonly Func<ConnectionConfiguration, ConnectionConfiguration> _configSetup;
			private readonly Func<ConnectionConfiguration, Stream, ElasticsearchResponse<Stream>> _responseSetup;
			private readonly Func<IElasticsearchClient, Task<ElasticsearchResponse<T>>> _call;

			public Requester(
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
					responseValue = string.Format(@"""{0}""", responseValue);
				var format = @"{{ ""value"": {0} }}";
				this.ResponseBytes = Encoding.UTF8.GetBytes(string.Format(format, responseValue));
				var stream = new MemoryStream(this.ResponseBytes);
				return stream;
			}

			public byte[] ResponseBytes { get; set; }

			public async void Dispose()
			{
				if (this.Fake != null) this.Fake.Dispose();
			}
		}

		private class Document
		{
			public object value { get; set; }
		}


		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void Typed_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<Document>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				object v = r.Response.value;

				v.ShouldBeEquivalentTo(responseValue);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void Typed_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<Document>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				object v = r.Response.value;

				v.ShouldBeEquivalentTo(responseValue);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void Typed_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<Document>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Assert.IsNull(r.Response);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void Typed_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<Document>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Assert.IsNull(r.Response);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void DynamicDictionary_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				client => client.InfoAsync()
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				object v = r.Response["value"];

				v.ShouldBeEquivalentTo(responseValue);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void DynamicDictionary_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				client => client.InfoAsync()
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				object v = r.Response["value"];

				v.ShouldBeEquivalentTo(responseValue);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void DynamicDictionary_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream),
				client => client.InfoAsync()
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Assert.IsNull(r.Response);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void DynamicDictionary_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream),
				client => client.InfoAsync()
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Assert.IsNull(r.Response);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);

			}
		}

		[Test, TestCase(505123)]
		public async void ByteArray_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				r.Response.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void ByteArray_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				r.Response.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
			}
		}



		[Test, TestCase(505123)]
		public async void ByteArray_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				r.Response.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void ByteArray_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				r.Response.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
			}
		}

		[Test, TestCase(505123)]
		public async void String_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				Encoding.UTF8.GetBytes(r.Response).Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void String_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				Encoding.UTF8.GetBytes(r.Response).Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
			}
		}



		[Test, TestCase(505123)]
		public async void String_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Encoding.UTF8.GetBytes(r.Response).Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void String_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				Encoding.UTF8.GetBytes(r.Response).Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				r.ResponseRaw.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
			}
		}
		
		[Test, TestCase(505123)]
		public async void Stream_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				using (r.Response)
				using (var ms = new MemoryStream())
				{
					r.Response.CopyTo(ms);
					var bytes = ms.ToArray();
					bytes.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				}
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void Stream_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				using (r.Response)
				using (var ms = new MemoryStream())
				{
					r.Response.CopyTo(ms);
					var bytes = ms.ToArray();
					bytes.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				}
				//raw response is ALWAYS null when requesting the stream directly
				//the client should not interfere with it
				r.ResponseRaw.Should().BeNull();
			}
		}



		[Test, TestCase(505123)]
		public async void Stream_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				using (r.Response)
				using (var ms = new MemoryStream())
				{
					r.Response.CopyTo(ms);
					var bytes = ms.ToArray();
					bytes.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				}
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void Stream_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				using (r.Response)
				using (var ms = new MemoryStream())
				{
					r.Response.CopyTo(ms);
					var bytes = ms.ToArray();
					bytes.Should().NotBeNull().And.BeEquivalentTo(request.ResponseBytes);
				}
				//raw response is ALWAYS null when requesting the stream directly
				//the client should not interfere with it
				r.ResponseRaw.Should().BeNull();
			}
		}
		
		[Test, TestCase(505123)]
		public async void VoidResponse_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				//Response and rawresponse should ALWAYS be null for VoidResponse responses
				r.Response.Should().BeNull();
				r.ResponseRaw.Should().BeNull();

			}
		}

		[Test, TestCase(505123)]
		public async void VoidResponse_Ok_KeepResponse(object responseValue)
		{
			using (var request = new Requester<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeTrue();
				//Response and rawresponse should ALWAYS be null for VoidResponse responses
				r.Response.Should().BeNull();
				r.ResponseRaw.Should().BeNull();
			}
		}



		[Test, TestCase(505123)]
		public async void VoidResponse_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new Requester<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				//Response and rawresponse should ALWAYS be null for VoidResponse responses
				r.Response.Should().BeNull();
				r.ResponseRaw.Should().BeNull();
			}
		}

		[Test, TestCase(505123)]
		public async void VoidResponse_Bad_KeepResponse(object responseValue)
		{
			using (var request = new Requester<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			{
				await request.Init();
				var r = request.Result;
				r.Success.Should().BeFalse();
				//Response and rawresponse should ALWAYS be null for VoidResponse responses
				r.Response.Should().BeNull();
				r.ResponseRaw.Should().BeNull();
			}
		}
	}
}
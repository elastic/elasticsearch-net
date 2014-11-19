using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Tests.Unit.Responses.Helpers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Responses
{
	[TestFixture]
	public class BuildInResponseAsyncTests
	{
		[Test]
		[TestCase(505)]
		[TestCase(10.2)]
		[TestCase("hello world")]
		public async void Typed_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new AsyncRequester<StandardResponse>(
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
			using (var request = new AsyncRequester<StandardResponse>(
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
			using (var request = new AsyncRequester<StandardResponse>(
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
			using (var request = new AsyncRequester<StandardResponse>(
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
			using (var request = new AsyncRequester<DynamicDictionary>(
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
			using (var request = new AsyncRequester<DynamicDictionary>(
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
		public async Task DynamicDictionary_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new AsyncRequester<DynamicDictionary>(
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
			using (var request = new AsyncRequester<DynamicDictionary>(
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
			using (var request = new AsyncRequester<byte[]>(
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
			using (var request = new AsyncRequester<byte[]>(
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
			using (var request = new AsyncRequester<byte[]>(
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
			using (var request = new AsyncRequester<byte[]>(
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
			using (var request = new AsyncRequester<string>(
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
			using (var request = new AsyncRequester<string>(
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
			using (var request = new AsyncRequester<string>(
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
			using (var request = new AsyncRequester<string>(
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
			using (var request = new AsyncRequester<Stream>(
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
			using (var request = new AsyncRequester<Stream>(
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
			using (var request = new AsyncRequester<Stream>(
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
			using (var request = new AsyncRequester<Stream>(
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
			using (var request = new AsyncRequester<VoidResponse>(
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
			using (var request = new AsyncRequester<VoidResponse>(
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
			using (var request = new AsyncRequester<VoidResponse>(
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
			using (var request = new AsyncRequester<VoidResponse>(
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
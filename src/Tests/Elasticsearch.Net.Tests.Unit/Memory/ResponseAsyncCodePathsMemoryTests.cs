using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Tests.Unit.Connection;
using Elasticsearch.Net.Tests.Unit.Requesters;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Memory
{
	[TestFixture]
	public class ResponseAsyncCodePathsMemoryTests : ResponseCodePathsMemoryTestsBase
	{
		[Test]
		[TestCase("hello world")]
		public async Task Typed_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request);
		}

		[Test]
		[TestCase("hello world")]
		public async Task Typed_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())

				this.ShouldStreamOfCopy(request);
		}

		[Test]
		[TestCase("hello world")]
		public async Task Typed_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request, false);
		}

		[Test]
		[TestCase("hello world")]
		public async Task Typed_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, false);	
		}

		[Test]
		[TestCase("hello world")]
		public async Task DynamicDictionary_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				c => c.InfoAsync()
			).Init())
				this.ShouldDirectlyStream(request);
		}

		[Test]
		[TestCase("hello world")]
		public async Task DynamicDictionary_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				c => c.InfoAsync()
			).Init())
				this.ShouldStreamOfCopy(request);
		}

		[Test]
		[TestCase("hello world")]
		public async Task DynamicDictionary_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request, false);
		}

		[Test]
		[TestCase("hello world")]
		public async Task DynamicDictionary_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public async Task ByteArray_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, keepRaw: false);
			
		}

		[Test, TestCase(505123)]
		public async Task ByteArray_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request);
			
		}

		[Test, TestCase(505123)]
		public async Task ByteArray_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
			this.ShouldStreamOfCopy(request, success: false, keepRaw: false);
		}

		[Test, TestCase(505123)]
		public async Task ByteArray_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, success: false);
		}

		[Test, TestCase(505123)]
		public async Task String_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, keepRaw: false);
		
		}

		[Test, TestCase(505123)]
		public async Task String_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request);
			
		}

		[Test, TestCase(505123)]
		public async Task String_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, success: false, keepRaw: false);
			
		}

		[Test, TestCase(505123)]
		public async Task String_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public async Task Stream_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request);
		}

		[Test, TestCase(505123)]
		public async Task Stream_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request, success: true);
		}

		[Test, TestCase(505123)]
		public async Task Stream_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public async Task Stream_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public async Task VoidResponse_Ok_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request);	
		}

		[Test, TestCase(505123)]
		public async Task VoidResponse_Ok_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			).Init())
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request);	
		}

		[Test, TestCase(505123)]
		public async Task VoidResponse_Bad_DiscardResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				//voidResponse NEVER reads the body so Raw is always false
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public async Task VoidResponse_Bad_KeepResponse(object responseValue)
		{
			using (var request = await new AsyncMemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			).Init())
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request, success: false);
		}
	}
}
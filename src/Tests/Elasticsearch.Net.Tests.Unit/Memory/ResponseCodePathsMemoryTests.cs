using System;
using System.IO;
using System.Text;
using Elasticsearch.Net.Tests.Unit.Memory.Helpers;
using Elasticsearch.Net.Tests.Unit.Responses.Helpers;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Memory
{
	[TestFixture]
	public class ResponseCodePathsMemoryTests : ResponseCodePathsMemoryTestsBase
	{
		[Test]
		[TestCase("hello world")]
		public void Typed_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldDirectlyStream(request);
		}

		[Test]
		[TestCase("hello world")]
		public void Typed_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request);
		}

		[Test]
		[TestCase("hello world")]
		public void Typed_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, false);
		}

		[Test]
		[TestCase("hello world")]
		public void Typed_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<StandardResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, false);	
		}

		[Test]
		[TestCase("hello world")]
		public void DynamicDictionary_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				client => client.Info()
			))
				this.ShouldDirectlyStream(request);
		}

		[Test]
		[TestCase("hello world")]
		public void DynamicDictionary_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream),
				client => client.Info()
			))
				this.ShouldStreamOfCopy(request);
		}

		[Test]
		[TestCase("hello world")]
		public void DynamicDictionary_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream),
				client => client.Info()
			))
				this.ShouldDirectlyStream(request, false);
		}

		[Test]
		[TestCase("hello world")]
		public void DynamicDictionary_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<DynamicDictionary>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream),
				client => client.Info()
			))
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public void ByteArray_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, keepRaw: false);
			
		}

		[Test, TestCase(505123)]
		public void ByteArray_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request);
			
		}

		[Test, TestCase(505123)]
		public void ByteArray_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
			this.ShouldStreamOfCopy(request, success: false, keepRaw: false);
		}

		[Test, TestCase(505123)]
		public void ByteArray_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, success: false);
		}

		[Test, TestCase(505123)]
		public void String_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, keepRaw: false);
		
		}

		[Test, TestCase(505123)]
		public void String_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request);
			
		}

		[Test, TestCase(505123)]
		public void String_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, success: false, keepRaw: false);
			
		}

		[Test, TestCase(505123)]
		public void String_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public void Stream_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldDirectlyStream(request);
		}

		[Test, TestCase(505123)]
		public void Stream_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, success: true);
		}

		[Test, TestCase(505123)]
		public void Stream_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public void Stream_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request);	
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request);	
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				//voidResponse NEVER reads the body so Raw is always false
				this.ShouldDirectlyStream(request, success: false);
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				//voidResponse NEVER reads the body so Raw is always false
				//and no intermediate stream should be created
				this.ShouldDirectlyStream(request, success: false);
		}
	}
}
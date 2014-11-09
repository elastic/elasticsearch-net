using System.IO;
using System.Text;
using Elasticsearch.Net.Tests.Unit.Connection;
using Elasticsearch.Net.Tests.Unit.Requesters;
using Elasticsearch.Net.Tests.Unit.Stubs;
using FluentAssertions;
using NUnit.Framework;

namespace Elasticsearch.Net.Tests.Unit.Memory
{
	[TestFixture]
	public class ResponseCodePathsMemoryTests
	{

		private void ShouldDirectlyStream<T>(MemorySetup<T> request, bool success = true, bool streamIsDisposed = true) where T : class
		{
			request.Result.Success.Should().Be(success);
			request.CreatedMemoryStreams.Should()
				.BeEmpty(reason: "No intermediate memory streams should have been created");
			request.ResponseStream.IsClosedOrDisposed.Should().Be(streamIsDisposed);
			if (!streamIsDisposed) request.ResponseStream.Dispose();
		}

		private void ShouldStreamOfCopy<T>(MemorySetup<T> request, bool success = true, bool streamIsDisposed = true) where T : class
		{
			request.Result.Success.Should().Be(success);
			request.Result.ResponseRaw.Should().NotBeNull();
			request.CreatedMemoryStreams.Should()
				.HaveCount(1, reason:"An intermediate memorystream should have been created to hold the raw response")
				.And.OnlyContain(m => m.IsClosedOrDisposed, reason: "Intermediate stream should still be closed or disposed");
			request.ResponseStream.IsClosedOrDisposed.Should().Be(streamIsDisposed);
			if (!streamIsDisposed) request.ResponseStream.Dispose();
		}

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
				this.ShouldDirectlyStream(request);
			
		}

		[Test, TestCase(505123)]
		public void ByteArray_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, false);
			
		}

		[Test, TestCase(505123)]
		public void ByteArray_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, false);
		}

		[Test, TestCase(505123)]
		public void ByteArray_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<byte[]>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public void String_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<string>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldDirectlyStream(request);
		
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
				this.ShouldDirectlyStream(request, false);
			
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
				this.ShouldDirectlyStream(request, streamIsDisposed: false);
		}

		[Test, TestCase(505123)]
		public void Stream_Ok_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, false);
		}

		[Test, TestCase(505123)]
		public void Stream_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldDirectlyStream(request, success: false, streamIsDisposed: false);
		}

		[Test, TestCase(505123)]
		public void Stream_Bad_KeepResponse(object responseValue)
		{
			using (var request = new MemorySetup<Stream>(
				responseValue,
				settings => settings.ExposeRawResponse(true),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
				this.ShouldStreamOfCopy(request, success: false, streamIsDisposed: false);
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Ok_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Ok(settings, response: stream)
			))
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
				this.ShouldStreamOfCopy(request);	
		}

		[Test, TestCase(505123)]
		public void VoidResponse_Bad_DiscardResponse(object responseValue)
		{
			using (var request = new MemorySetup<VoidResponse>(
				responseValue,
				settings => settings.ExposeRawResponse(false),
				(settings, stream) => FakeResponse.Bad(settings, response: stream)
			))
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
				this.ShouldStreamOfCopy(request, success: false);
		}
	}
}
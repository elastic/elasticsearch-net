using System.IO;
using Elasticsearch.Net.Tests.Unit.Memory.Helpers;
using FluentAssertions;

namespace Elasticsearch.Net.Tests.Unit.Memory
{
	public class ResponseCodePathsMemoryTestsBase
	{
		protected void ShouldDirectlyStream<T>(IMemorySetup<T> request, bool success = true) where T : class
		{
			var streamIsDisposed = !(typeof (Stream).IsAssignableFrom(typeof (T)));
			request.Result.Success.Should()
				.Be(success, reason: success 
					? "Bad response should not result in success response" 
					: "Healty response should not result in an error response");
			request.Result.ResponseRaw.Should()
				.BeNull(reason: "When streaming directly raw response should not have been possible to set");
			request.CreatedMemoryStreams.Should()
				.BeEmpty(reason: "No intermediate memory streams should have been created");
			request.ResponseStream.IsClosedOrDisposed.Should()
				.Be(streamIsDisposed, reason: streamIsDisposed 
					? "Response stream should have been closed or disposed" 
					: "Response stream should not have been closed or disposed when returning a Stream");
			if (!streamIsDisposed) request.ResponseStream.Dispose();
		}

		protected void ShouldStreamOfCopy<T>(IMemorySetup<T> request, bool success = true, bool keepRaw = true) where T : class
		{
			var streamIsDisposed = !(typeof (Stream).IsAssignableFrom(typeof (T)));
			var isForcedRead = typeof (T) == typeof (string) || typeof (T) == typeof (byte[]);
			request.Result.Success.Should()
				.Be(success, reason: success 
					? "Bad response should not result in success response" 
					: "Healty response should not result in an error response");
			//when reading to byte or string a copy is forced but ResponseRaw can still be null
			if (keepRaw)
				request.Result.ResponseRaw.Should()
					.NotBeNull(reason: "We expect raw response to be set on response");
			else
				request.Result.ResponseRaw.Should()
					.BeNull(reason: "We expect raw response to still be null even if we had to force read the stream to string or byte[]");

			if (!success && (isForcedRead || !streamIsDisposed))
				request.Result.Response.Should()
					.NotBeNull(reason: "Eventhough success is false when reading to string, byte[] or Stream we always expose .Response.");

			request.CreatedMemoryStreams.Should()
				.HaveCount(1, reason:"An intermediate memorystream should have been created to hold the raw response")
				.And.OnlyContain(m => m.IsClosedOrDisposed, reason: "Intermediate stream should still be closed or disposed");
			request.ResponseStream.IsClosedOrDisposed.Should()
				.Be(streamIsDisposed, reason: streamIsDisposed 
					? "Response stream should have been closed or disposed" 
					: "Response stream should not have been closed or disposed when returning a Stream");
			if (!streamIsDisposed) request.ResponseStream.Dispose();
		}
	}
}
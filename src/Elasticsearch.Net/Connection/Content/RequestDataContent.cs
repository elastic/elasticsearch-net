// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// modified to be dedicated for RequestData only

#if DOTNETCORE
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Provides an <see cref="HttpContent"/> implementation that exposes an output <see cref="Stream"/>
	/// which can be written to directly. The ability to push data to the output stream differs from the
	/// <see cref="StreamContent"/> where data is pulled and not pushed.
	/// </summary>
	internal class RequestDataContent : HttpContent
	{
		private readonly RequestData _requestData;
		private readonly Func<RequestData, CompleteTaskOnCloseStream, RequestDataContent, TransportContext, Task> _onStreamAvailable;

		public RequestDataContent(RequestData requestData)
		{
			_requestData = requestData;
			Headers.ContentType = new MediaTypeHeaderValue(requestData.RequestMimeType);
			if (requestData.HttpCompression)
				Headers.ContentEncoding.Add("gzip");

			Task OnStreamAvailable(RequestData data, Stream stream, HttpContent content, TransportContext context)
			{
				if (data.HttpCompression)
					stream = new GZipStream(stream, CompressionMode.Compress, false);

				using(stream)
					data.PostData.Write(stream, data.ConnectionSettings);

				return Task.CompletedTask;
			}

			_onStreamAvailable = OnStreamAvailable;
		}
		public RequestDataContent(RequestData requestData, CancellationToken token)
		{
			_requestData = requestData;
			Headers.ContentType = new MediaTypeHeaderValue(requestData.RequestMimeType);
			if (requestData.HttpCompression)
				Headers.ContentEncoding.Add("gzip");

			async Task OnStreamAvailable(RequestData data, Stream stream, HttpContent content, TransportContext context)
			{
				if (data.HttpCompression)
					stream = new GZipStream(stream, CompressionMode.Compress, false);

				using (stream)
					await data.PostData.WriteAsync(stream, data.ConnectionSettings, token).ConfigureAwait(false);
			}

			_onStreamAvailable = OnStreamAvailable;
		}

		/// <summary>
		/// When this method is called, it calls the action provided in the constructor with the output
		/// stream to write to. Once the action has completed its work it closes the stream which will
		/// close this content instance and complete the HTTP request or response.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to which to write.</param>
		/// <param name="context">The associated <see cref="TransportContext"/>.</param>
		/// <returns>A <see cref="Task"/> instance that is asynchronously serializing the object's content.</returns>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exception is passed as task result.")]
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			var serializeToStreamTask = new TaskCompletionSource<bool>();
			var wrappedStream = new CompleteTaskOnCloseStream(stream, serializeToStreamTask);
            await _onStreamAvailable(_requestData, wrappedStream, this, context).ConfigureAwait(false);
            await serializeToStreamTask.Task.ConfigureAwait(false);
		}

		/// <summary>
		/// Computes the length of the stream if possible.
		/// </summary>
		/// <param name="length">The computed length of the stream.</param>
		/// <returns><c>true</c> if the length has been computed; otherwise <c>false</c>.</returns>
		protected override bool TryComputeLength(out long length)
		{
			// We can't know the length of the content being pushed to the output stream.
			length = -1;
			return false;
		}

		internal class CompleteTaskOnCloseStream : DelegatingStream
		{
			private readonly TaskCompletionSource<bool> _serializeToStreamTask;

			public CompleteTaskOnCloseStream(Stream innerStream, TaskCompletionSource<bool> serializeToStreamTask)
				: base(innerStream)
			{
				Contract.Assert(serializeToStreamTask != null);
				_serializeToStreamTask = serializeToStreamTask;
			}

			protected override void Dispose(bool disposing)
			{
				_serializeToStreamTask.TrySetResult(true);
				base.Dispose();
			}

			public override void Close() => _serializeToStreamTask.TrySetResult(true);
		}

		/// <summary>
		/// Stream that delegates to inner stream.
		/// This is taken from System.Net.Http
		/// </summary>
		internal abstract class DelegatingStream : Stream
		{
			private readonly Stream _innerStream;

			protected DelegatingStream(Stream innerStream) => _innerStream = innerStream ?? throw new ArgumentNullException(nameof(innerStream));

			public override bool CanRead => _innerStream.CanRead;

			public override bool CanSeek => _innerStream.CanSeek;

			public override bool CanWrite => _innerStream.CanWrite;

			public override long Length => _innerStream.Length;

			public override long Position
			{
				get => _innerStream.Position;
				set => _innerStream.Position = value;
			}

			public override int ReadTimeout
			{
				get => _innerStream.ReadTimeout;
				set => _innerStream.ReadTimeout = value;
			}

			public override bool CanTimeout => _innerStream.CanTimeout;

			public override int WriteTimeout
			{
				get => _innerStream.WriteTimeout;
				set => _innerStream.WriteTimeout = value;
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					_innerStream.Dispose();
				}
				base.Dispose(disposing);
			}

			public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);

			public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
				_innerStream.ReadAsync(buffer, offset, count, cancellationToken);

			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) =>
				_innerStream.BeginRead(buffer, offset, count, callback, state);

			public override int EndRead(IAsyncResult asyncResult) => _innerStream.EndRead(asyncResult);

			public override int ReadByte() => _innerStream.ReadByte();

			public override void Flush() => _innerStream.Flush();

			public override Task FlushAsync(CancellationToken cancellationToken) => _innerStream.FlushAsync(cancellationToken);

			public override void SetLength(long value) => _innerStream.SetLength(value);

			public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);

			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
				_innerStream.WriteAsync(buffer, offset, count, cancellationToken);

			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) =>
				_innerStream.BeginWrite(buffer, offset, count, callback, state);

			public override void EndWrite(IAsyncResult asyncResult) => _innerStream.EndWrite(asyncResult);

			public override void WriteByte(byte value) => _innerStream.WriteByte(value);

			public override void Close() => _innerStream.Close();
		}
	}
}
#endif

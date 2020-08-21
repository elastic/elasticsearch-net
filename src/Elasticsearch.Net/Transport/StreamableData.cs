// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Represents an instance of <see cref="PostData"/> that can handle <see cref="PostType.StreamHandler"/>.
	/// Allows users full control over how they want to write data to the stream to Elasticsearch
	/// </summary>
	/// <typeparam name="T">The data or a state object used during writing, passed to the handlers to avoid boxing</typeparam>
	public class StreamableData<T> : PostData, IPostData<T>
	{
		private readonly T _state;
		private readonly Action<T, Stream> _syncWriter;
		private readonly Func<T, Stream, CancellationToken, Task> _asyncWriter;

		public StreamableData(T state, Action<T, Stream> syncWriter, Func<T, Stream, CancellationToken, Task> asyncWriter)
		{
			_state = state;
			const string message = "PostData.StreamHandler needs to handle both synchronous and async paths";
			_syncWriter = syncWriter ?? throw new ArgumentNullException(nameof(syncWriter), message);
			_asyncWriter = asyncWriter ?? throw new ArgumentNullException(nameof(asyncWriter), message);
			if (_syncWriter == null || _asyncWriter == null)
				throw new ArgumentNullException();
			Type = PostType.StreamHandler;
		}

		public override void Write(Stream writableStream, IConnectionConfigurationValues settings)
		{
			MemoryStream buffer = null;
			var stream = writableStream;
			BufferIfNeeded(settings, ref buffer, ref stream);
			_syncWriter(_state, stream);
			FinishStream(writableStream, buffer, settings);
		}

		public override async Task WriteAsync(Stream writableStream, IConnectionConfigurationValues settings, CancellationToken cancellationToken)
		{
			MemoryStream buffer = null;
			var stream = writableStream;
			BufferIfNeeded(settings, ref buffer, ref stream);
			await _asyncWriter(_state, stream, cancellationToken).ConfigureAwait(false);
			await FinishStreamAsync(writableStream, buffer, settings, cancellationToken).ConfigureAwait(false);
		}
	}
}

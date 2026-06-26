// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Core.MSearch;
using Elastic.Clients.Elasticsearch.Core.MSearchTemplate;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Builds the NDJSON request types (<see cref="BulkRequest"/>, <see cref="MultiSearchRequest"/>,
/// <see cref="MultiSearchTemplateRequest"/>) by consuming the body one top-level value at a time from
/// <see cref="NdjsonValueReader"/>. A logical unit is one value (a bulk <c>delete</c>) or two (an action header plus its
/// source, or a multi-search header plus its body); each value is deserialized into a managed object the moment it
/// arrives, so no raw byte slice is held across a refill. The per-value/per-operation parsing is the same code the
/// buffered converters use, so the streamed result is identical to the buffered one.
/// </summary>
internal static class NdjsonStreamAssembler
{
	public static object AssembleBulk(Stream stream, JsonSerializerOptions options)
	{
		var assembly = new BulkAssembly(options);
		NdjsonValueReader.DriveStream(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit);
		return assembly.Build();
	}

	public static async ValueTask<object> AssembleBulkAsync(Stream stream, JsonSerializerOptions options, CancellationToken cancellationToken)
	{
		var assembly = new BulkAssembly(options);
		await NdjsonValueReader.DriveStreamAsync(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit, cancellationToken).ConfigureAwait(false);
		return assembly.Build();
	}

	public static object AssembleMultiSearch(Stream stream, JsonSerializerOptions options)
	{
		var assembly = new MultiSearchAssembly(options);
		NdjsonValueReader.DriveStream(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit);
		return assembly.Build();
	}

	public static async ValueTask<object> AssembleMultiSearchAsync(Stream stream, JsonSerializerOptions options, CancellationToken cancellationToken)
	{
		var assembly = new MultiSearchAssembly(options);
		await NdjsonValueReader.DriveStreamAsync(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit, cancellationToken).ConfigureAwait(false);
		return assembly.Build();
	}

	public static object AssembleMultiSearchTemplate(Stream stream, JsonSerializerOptions options)
	{
		var assembly = new MultiSearchTemplateAssembly(options);
		NdjsonValueReader.DriveStream(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit);
		return assembly.Build();
	}

	public static async ValueTask<object> AssembleMultiSearchTemplateAsync(Stream stream, JsonSerializerOptions options, CancellationToken cancellationToken)
	{
		var assembly = new MultiSearchTemplateAssembly(options);
		await NdjsonValueReader.DriveStreamAsync(stream, NdjsonValueReader.BuildReaderOptions(options), assembly.Visit, cancellationToken).ConfigureAwait(false);
		return assembly.Build();
	}

	private sealed class BulkAssembly(JsonSerializerOptions options)
	{
		private readonly BulkOperationsCollection _operations = new();
		private BulkActionHeader? _pending;

		public void Visit(ReadOnlySequence<byte> value, int index)
		{
			if (_pending is null)
			{
				var header = ReadBulkHeader(value, options);

				// 'delete' is header-only; every other operation pairs the header with the next value (its source).
				if (header.OperationType is "delete")
					_operations.Add(CompleteBulk(in header, default, options));
				else
					_pending = header;

				return;
			}

			var pending = _pending.Value;
			_operations.Add(CompleteBulk(in pending, value, options));
			_pending = null;
		}

		public BulkRequest Build()
		{
			if (_pending is not null)
				throw new JsonException($"Expected a source line following the '{_pending.Value.OperationType}' bulk action header.");

			return new BulkRequest(JsonConstructorSentinel.Instance) { Operations = _operations };
		}

		private static BulkActionHeader ReadBulkHeader(in ReadOnlySequence<byte> value, JsonSerializerOptions options)
		{
			if (value.IsSingleSegment)
				return BulkRequestConverter.ReadActionHeader(value.First.Span, options);

			var length = (int)value.Length;
			var rented = ArrayPool<byte>.Shared.Rent(length);
			try
			{
				value.CopyTo(rented);
				return BulkRequestConverter.ReadActionHeader(rented.AsSpan(0, length), options);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(rented);
			}
		}

		private static IBulkOperation CompleteBulk(in BulkActionHeader header, in ReadOnlySequence<byte> source, JsonSerializerOptions options)
		{
			if (source.IsSingleSegment)
				return BulkRequestConverter.CompleteOperation(in header, source.First.Span, options);

			var length = (int)source.Length;
			var rented = ArrayPool<byte>.Shared.Rent(length);
			try
			{
				source.CopyTo(rented);
				return BulkRequestConverter.CompleteOperation(in header, rented.AsSpan(0, length), options);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(rented);
			}
		}
	}

	private sealed class MultiSearchAssembly(JsonSerializerOptions options)
	{
		private readonly List<SearchRequestItem> _searches = new();
		private MultisearchHeader? _pendingHeader;

		public void Visit(ReadOnlySequence<byte> value, int index)
		{
			if (_pendingHeader is null)
			{
				_pendingHeader = ReadSlice<MultisearchHeader>(value, options);
				return;
			}

			var body = ReadSlice<MultisearchBody>(value, options);
			_searches.Add(new SearchRequestItem(_pendingHeader!, body!));
			_pendingHeader = null;
		}

		public MultiSearchRequest Build()
		{
			if (_pendingHeader is not null)
				throw new JsonException("Expected a search body line following the header in the multi-search NDJSON body.");

			return new MultiSearchRequest(JsonConstructorSentinel.Instance) { Searches = _searches };
		}
	}

	private sealed class MultiSearchTemplateAssembly(JsonSerializerOptions options)
	{
		private readonly List<SearchTemplateRequestItem> _searchTemplates = new();
		private MultisearchHeader? _pendingHeader;

		public void Visit(ReadOnlySequence<byte> value, int index)
		{
			if (_pendingHeader is null)
			{
				_pendingHeader = ReadSlice<MultisearchHeader>(value, options);
				return;
			}

			var body = ReadSlice<TemplateConfig>(value, options);
			_searchTemplates.Add(new SearchTemplateRequestItem(_pendingHeader!, body!));
			_pendingHeader = null;
		}

		public MultiSearchTemplateRequest Build()
		{
			if (_pendingHeader is not null)
				throw new JsonException("Expected a template body line following the header in the multi-search-template NDJSON body.");

			return new MultiSearchTemplateRequest(JsonConstructorSentinel.Instance) { SearchTemplates = _searchTemplates };
		}
	}

	private static T? ReadSlice<T>(in ReadOnlySequence<byte> value, JsonSerializerOptions options)
	{
		// MaxDepth must mirror the serializer here too; the inner reader has its own options.
		var readerOptions = new JsonReaderOptions { MaxDepth = options.MaxDepth };

		if (value.IsSingleSegment)
			return ReadSlice<T>(value.First.Span, options, readerOptions);

		var length = (int)value.Length;
		var rented = ArrayPool<byte>.Shared.Rent(length);
		try
		{
			value.CopyTo(rented);
			return ReadSlice<T>(rented.AsSpan(0, length), options, readerOptions);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(rented);
		}
	}

	private static T? ReadSlice<T>(System.ReadOnlySpan<byte> span, JsonSerializerOptions options, JsonReaderOptions readerOptions)
	{
		var reader = new Utf8JsonReader(span, readerOptions);
		reader.Read();
		return reader.ReadValue<T>(options);
	}
}

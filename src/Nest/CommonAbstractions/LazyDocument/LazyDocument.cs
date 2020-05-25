// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A lazily deserialized document
	/// </summary>
	[JsonFormatter(typeof(LazyDocumentInterfaceFormatter))]
	public interface ILazyDocument
	{
		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		T As<T>();

		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		object As(Type objectType);

		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		Task<T> AsAsync<T>(CancellationToken ct = default);
		
		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		Task<object> AsAsync(Type objectType, CancellationToken ct = default);
	}

	/// <inheritdoc />
	[JsonFormatter(typeof(LazyDocumentFormatter))]
	public class LazyDocument : ILazyDocument
	{
		private readonly IElasticsearchSerializer _sourceSerializer;
		private readonly IElasticsearchSerializer _requestResponseSerializer;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		internal LazyDocument(byte[] bytes, IJsonFormatterResolver formatterResolver)
		{
			Bytes = bytes;
			var settings = formatterResolver.GetConnectionSettings();
			_sourceSerializer = settings.SourceSerializer;
			_requestResponseSerializer = settings.RequestResponseSerializer;
			_memoryStreamFactory = settings.MemoryStreamFactory;
		}

		internal byte[] Bytes { get; }

		internal T AsUsingRequestResponseSerializer<T>()
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _requestResponseSerializer.Deserialize<T>(ms);
		}

		/// <inheritdoc />
		public T As<T>()
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _sourceSerializer.Deserialize<T>(ms);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _sourceSerializer.Deserialize(objectType, ms);
		}

		/// <inheritdoc />
		public Task<T> AsAsync<T>(CancellationToken ct = default)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _sourceSerializer.DeserializeAsync<T>(ms, ct);
		}

		/// <inheritdoc />
		public Task<object> AsAsync(Type objectType, CancellationToken ct = default)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _sourceSerializer.DeserializeAsync(objectType, ms, ct);
		}
	}
}

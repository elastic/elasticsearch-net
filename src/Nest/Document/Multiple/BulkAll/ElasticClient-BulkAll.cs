// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to Elasticsearch as concurrent bulk requests.
		/// <para />
		/// The index to target will be inferred from <typeparamref name="T" />. If no default index has been mapped for <typeparamref name="T" />
		/// using <see cref="ConnectionSettingsBase{TConnectionSettings}.DefaultMappingFor{TDocument}"/> on <see cref="Nest.ConnectionSettings"/>, an exception will be thrown.
		/// Inference can be overridden using <see cref="BulkAllDescriptor{T}.Index"/>, and in addition,
		/// an index can be specified for each document using <see cref="BulkAllDescriptor{T}.BufferToBulk"/>.
		/// </summary>
		/// <param name="documents">The lazy stream of documents</param>
		BulkAllObservable<T> BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default
		)
			where T : class;

		/// <summary>
		/// BulkAll is a generic helper that will partition any lazy stream of documents and send them to Elasticsearch as concurrent bulk requests
		/// <para />
		/// The index to target will be inferred from <typeparamref name="T" />. If no default index has been mapped for <typeparamref name="T" />
		/// using <see cref="ConnectionSettingsBase{TConnectionSettings}.DefaultMappingFor{TDocument}"/> on <see cref="Nest.ConnectionSettings"/>, an exception will be thrown.
		/// Inference can be overridden using <see cref="IBulkAllRequest{T}.Index"/>, and in addition,
		/// an index can be specified for each document using <see cref="IBulkAllRequest{T}.BufferToBulk"/>.
		/// </summary>
		BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default) where T : class;
	}

	public partial class ElasticClient
	{
		///<inheritdoc />
		public BulkAllObservable<T> BulkAll<T>(
			IEnumerable<T> documents,
			Func<BulkAllDescriptor<T>, IBulkAllRequest<T>> selector,
			CancellationToken cancellationToken = default
		)
			where T : class =>
			BulkAll(selector.InvokeOrDefault(new BulkAllDescriptor<T>(documents)), cancellationToken);

		///<inheritdoc />
		public BulkAllObservable<T> BulkAll<T>(IBulkAllRequest<T> request, CancellationToken cancellationToken = default)
			where T : class =>
			new BulkAllObservable<T>(this, request, cancellationToken);
	}
}

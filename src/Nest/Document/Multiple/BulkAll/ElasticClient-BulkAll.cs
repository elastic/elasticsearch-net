/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
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

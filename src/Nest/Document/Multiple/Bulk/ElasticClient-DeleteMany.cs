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

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides DeleteMany extensions that make it easier to get many documents given a list of ids
	/// </summary>
	public static class DeleteManyExtensions
	{
		/// <summary>
		/// Shortcut into the Bulk call that deletes the specified objects
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="client"></param>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="objects">List of objects to delete</param>
		/// <param name="index">Override the inferred indexname for T</param>
		/// <param name="type">Override the inferred typename for T</param>
		public static BulkResponse DeleteMany<T>(this IElasticClient client, IEnumerable<T> @objects, IndexName index = null)
			where T : class
		{
			var bulkRequest = CreateDeleteBulkRequest(objects, index);
			return client.Bulk(bulkRequest);
		}


		/// <summary>
		/// Shortcut into the Bulk call that deletes the specified objects
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="client"></param>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="objects">List of objects to delete</param>
		/// <param name="index">Override the inferred indexname for T</param>
		/// <param name="type">Override the inferred typename for T</param>
		public static Task<BulkResponse> DeleteManyAsync<T>(this IElasticClient client, IEnumerable<T> objects, IndexName index = null,
			 CancellationToken cancellationToken = default
		)
			where T : class
		{
			var bulkRequest = CreateDeleteBulkRequest(objects, index);
			return client.BulkAsync(bulkRequest, cancellationToken);
		}

		private static BulkRequest CreateDeleteBulkRequest<T>(IEnumerable<T> objects, IndexName index) where T : class
		{
			// ReSharper disable once PossibleMultipleEnumeration
			objects.ThrowIfEmpty(nameof(objects));
			var bulkRequest = new BulkRequest(index);
			// ReSharper disable once PossibleMultipleEnumeration
			var deletes = objects
				.Select(o => new BulkDeleteOperation<T>(o))
				.Cast<IBulkOperation>()
				.ToList();

			bulkRequest.Operations = new BulkOperationsCollection<IBulkOperation>(deletes);

			return bulkRequest;
		}
	}
}

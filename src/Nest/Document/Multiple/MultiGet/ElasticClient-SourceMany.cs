// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Provides convenience extension methods to get many _source's given a list of ids.
	/// </summary>
	public static class SourceManyExtensions
	{

		private static Func<MultiGetOperationDescriptor<T>, string, IMultiGetOperation> Lookup<T>(IndexName index)
			where T : class
		{
			if (index == null) return null;

			return (d, id) => d.Index(index);
		}

		/// <summary>
		/// SourceMany allows you to get a list of T documents out of Elasticsearch, internally it calls into MultiGet()
		/// <para>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// </para>
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">A list of ids as string</param>
		/// <param name="index">Optionally override the default inferred indexname for T</param>
		public static IEnumerable<T> SourceMany<T>(this IElasticClient client, IEnumerable<string> ids, string index = null)
			where T : class
		{
			var result = client.MultiGet(s => s
				.Index(index)
				.RequestConfiguration(r => r.ThrowExceptions())
				.GetMany<T>(ids, Lookup<T>(index))
			);
			return result.SourceMany<T>(ids);
		}

		/// <summary>
		/// SourceMany allows you to get a list of T documents out of Elasticsearch, internally it calls into MultiGet()
		/// <para>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// </para>
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">A list of ids as int</param>
		/// <param name="index">Optionally override the default inferred indexname for T</param>
		public static IEnumerable<T> SourceMany<T>(this IElasticClient client, IEnumerable<long> ids, string index = null)
			where T : class => client.SourceMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index);

		/// <summary>
		/// SourceMany allows you to get a list of T documents out of Elasticsearch, internally it calls into MultiGet()
		/// <para>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// </para>
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">A list of ids as string</param>
		/// <param name="index">Optionally override the default inferred indexname for T</param>
		public static async Task<IEnumerable<T>> SourceManyAsync<T>(
			this IElasticClient client, IEnumerable<string> ids, string index = null, CancellationToken cancellationToken = default
		)
			where T : class
		{
			var response = await client.MultiGetAsync(s => s
					.Index(index)
					.RequestConfiguration(r => r.ThrowExceptions())
					.GetMany<T>(ids, Lookup<T>(index)), cancellationToken)
				.ConfigureAwait(false);
			return response.SourceMany<T>(ids);
		}

		/// <summary>
		/// SourceMany allows you to get a list of T documents out of Elasticsearch, internally it calls into MultiGet()
		/// <para>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing).
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document
		/// provided by the get API.
		/// </para>
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="client"></param>
		/// <param name="ids">A list of ids as int</param>
		/// <param name="index">Optionally override the default inferred indexname for T</param>
		public static Task<IEnumerable<T>> SourceManyAsync<T>(
			this IElasticClient client, IEnumerable<long> ids, string index = null,
			CancellationToken cancellationToken = default
		)
			where T : class => client.SourceManyAsync<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, cancellationToken);
	}
}

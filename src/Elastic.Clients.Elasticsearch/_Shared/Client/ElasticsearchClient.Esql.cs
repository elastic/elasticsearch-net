// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Elastic.Esql;
using Elastic.Esql.Core;
using Elastic.Esql.Execution;
using Elastic.Esql.Extensions;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Esql;

public partial class EsqlNamespacedClient
{
#pragma warning disable IL2026, IL3050

	private static readonly JsonSerializerOptions EsqlJsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
	{
		TypeInfoResolver = EsqlJsonSerializerContext.Default
	};

#pragma warning restore IL2026, IL3050

	private EsqlQueryProvider? _queryProvider;

	#region Stream response ES|QL query methods

	/// <summary>
	/// Executes an ES|QL request and returns the response as a stream.
	/// </summary>
	/// <returns>The ES|QL query result as a generic stream response.</returns>
	/// <remarks>The response must be disposed after use.</remarks>
	public virtual Task<StreamResponse> QueryAsStreamAsync<TDocument>(
		Action<EsqlQueryRequestDescriptor<TDocument>> configureRequest,
		CancellationToken cancellationToken = default)
	{
		var descriptor = new EsqlQueryRequestDescriptor<TDocument>();
		configureRequest?.Invoke(descriptor);
		var request = descriptor.Instance;
		request.BeforeRequest();
		return DoRequestAsync<EsqlQueryRequest, StreamResponse, EsqlQueryRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// Executes an ES|QL request and returns the response as a stream.
	/// </summary>
	/// <returns>The ES|QL query result as a generic stream response.</returns>
	/// <remarks>The response must be disposed after use.</remarks>
	public virtual Task<StreamResponse> QueryAsStreamAsync(
		Action<EsqlQueryRequestDescriptor> configureRequest,
		CancellationToken cancellationToken = default)
	{
		var descriptor = new EsqlQueryRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		var request = descriptor.Instance;
		request.BeforeRequest();
		return DoRequestAsync<EsqlQueryRequest, StreamResponse, EsqlQueryRequestParameters>(request, cancellationToken);
	}

	#endregion

	#region LINQ to ES|QL query methods

	/// <summary>
	/// Creates a new LINQ-to-ES|QL queryable for the specified entity type.
	/// </summary>
	/// <typeparam name="T">The entity type to query.</typeparam>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <returns>An <see cref="IEsqlQueryable{T}"/> that can be used to build and execute ES|QL queries via LINQ.</returns>
	public IEsqlQueryable<T> CreateQuery<T>(EsqlQueryOptions? queryOptions = null) where T : class
		=> (IEsqlQueryable<T>)ApplyOptions(new EsqlQueryable<T>(GetOrCreateQueryProvider()), queryOptions);

	/// <summary>
	/// Executes a synchronous LINQ-to-ES|QL query and returns the results.
	/// </summary>
	/// <typeparam name="T">The entity type to query.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <returns>An <see cref="IEnumerable{T}"/> containing the query results.</returns>
	public IEnumerable<T> Query<T>(
		Func<IQueryable<T>, IQueryable<T>> query,
		EsqlQueryOptions? queryOptions = null) where T : class =>
		query(CreateQuery<T>(queryOptions));

	/// <summary>
	/// Executes a synchronous LINQ-to-ES|QL query with a projection and returns the results.
	/// </summary>
	/// <typeparam name="T">The source entity type.</typeparam>
	/// <typeparam name="TResult">The projected result type.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <returns>An <see cref="IEnumerable{TResult}"/> containing the query results.</returns>
	public IEnumerable<TResult> Query<T, TResult>(
		Func<IQueryable<T>, IQueryable<TResult>> query,
		EsqlQueryOptions? queryOptions = null) where T : class =>
		query(CreateQuery<T>(queryOptions));

	/// <summary>
	/// Asynchronously executes a LINQ-to-ES|QL query and returns the results as an async stream.
	/// </summary>
	/// <typeparam name="T">The entity type to query.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerable{T}"/> containing the query results.</returns>
	/// <exception cref="InvalidOperationException">Thrown if the query does not return an <see cref="IEsqlQueryable{T}"/>.</exception>
	public IAsyncEnumerable<T> QueryAsync<T>(
		Func<IQueryable<T>, IQueryable<T>> query,
		EsqlQueryOptions? queryOptions = null,
		CancellationToken cancellationToken = default) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().AsAsyncEnumerable(cancellationToken);

	/// <summary>
	/// Asynchronously executes a LINQ-to-ES|QL query with a projection and returns the results as an async stream.
	/// </summary>
	/// <typeparam name="T">The source entity type.</typeparam>
	/// <typeparam name="TResult">The projected result type.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerable{TResult}"/> containing the query results.</returns>
	/// <exception cref="InvalidOperationException">Thrown if the query does not return an <see cref="IEsqlQueryable{TResult}"/>.</exception>
	public IAsyncEnumerable<TResult> QueryAsync<T, TResult>(
		Func<IQueryable<T>, IQueryable<TResult>> query,
		EsqlQueryOptions? queryOptions = null,
		CancellationToken cancellationToken = default) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().AsAsyncEnumerable(cancellationToken);

	/// <summary>
	/// Submits a LINQ-to-ES|QL query as a server-side async query.
	/// </summary>
	/// <typeparam name="T">The entity type to query.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <param name="asyncQueryOptions">Optional async query options.</param>
	/// <returns>An <see cref="EsqlAsyncQuery{T}"/> that can be used to poll and retrieve results.</returns>
	/// <exception cref="InvalidOperationException">Thrown if the query does not return an <see cref="IEsqlQueryable{T}"/>.</exception>
	public EsqlAsyncQuery<T> SubmitAsyncQuery<T>(
		Func<IQueryable<T>, IQueryable<T>> query,
		EsqlQueryOptions? queryOptions = null,
		EsqlAsyncQueryOptions? asyncQueryOptions = null) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().ToAsyncQuery(asyncQueryOptions);

	/// <summary>
	/// Submits a LINQ-to-ES|QL query with a projection as a server-side async query.
	/// </summary>
	public EsqlAsyncQuery<TResult> SubmitAsyncQuery<T, TResult>(
		Func<IQueryable<T>, IQueryable<TResult>> query,
		EsqlQueryOptions? queryOptions = null,
		EsqlAsyncQueryOptions? asyncQueryOptions = null) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().ToAsyncQuery(asyncQueryOptions);

	/// <summary>
	/// Asynchronously submits a LINQ-to-ES|QL query as a server-side async query.
	/// </summary>
	/// <typeparam name="T">The entity type to query.</typeparam>
	/// <param name="query">A function that builds the query from an <see cref="IQueryable{T}"/>.</param>
	/// <param name="queryOptions">Optional per-query options (filter, timeouts, parameters, etc.).</param>
	/// <param name="asyncQueryOptions">Optional async query options.</param>
	/// <param name="cancellationToken">A cancellation token.</param>
	/// <returns>An <see cref="EsqlAsyncQuery{T}"/> that can be used to poll and retrieve results.</returns>
	/// <exception cref="InvalidOperationException">Thrown if the query does not return an <see cref="IEsqlQueryable{T}"/>.</exception>
	public Task<EsqlAsyncQuery<T>> SubmitAsyncQueryAsync<T>(
		Func<IQueryable<T>, IQueryable<T>> query,
		EsqlQueryOptions? queryOptions = null,
		EsqlAsyncQueryOptions? asyncQueryOptions = null,
		CancellationToken cancellationToken = default) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().ToAsyncQueryAsync(asyncQueryOptions, cancellationToken);

	/// <summary>
	/// Asynchronously submits a LINQ-to-ES|QL query with a projection as a server-side async query.
	/// </summary>
	public Task<EsqlAsyncQuery<TResult>> SubmitAsyncQueryAsync<T, TResult>(
		Func<IQueryable<T>, IQueryable<TResult>> query,
		EsqlQueryOptions? queryOptions = null,
		EsqlAsyncQueryOptions? asyncQueryOptions = null,
		CancellationToken cancellationToken = default) where T : class =>
		query(CreateQuery<T>(queryOptions)).AsEsqlQueryable().ToAsyncQueryAsync(asyncQueryOptions, cancellationToken);

	private static IQueryable<T> ApplyOptions<T>(IQueryable<T> queryable, EsqlQueryOptions? queryOptions)
	{
		if (queryOptions is null)
			return queryable;

		if (queryable is not IEsqlQueryable<T> esqlQueryable)
			throw new InvalidOperationException("Query must return an IEsqlQueryable.");

		return esqlQueryable.WithOptions(queryOptions);
	}

	private EsqlQueryProvider GetOrCreateQueryProvider()
	{
		if (_queryProvider is not null)
			return _queryProvider;

		if (!Client.SourceSerializer.TryGetJsonSerializerOptions(out var options))
		{
			throw new InvalidOperationException(
				"The SourceSerializer does not support JsonSerializerOptions. An EsqlQueryProvider cannot be created.");
		}

		var executor = new EsqlQueryExecutor(this);
		var provider = new EsqlQueryProvider(options, executor)
		{
			Interceptor = new EsqlSourceInferenceInterceptor(Client.Infer)
		};
		Interlocked.CompareExchange(ref _queryProvider, provider, null);
		return _queryProvider!;
	}

	#endregion

	#region Legacy ES|QL query methods

	[Obsolete("Use CreateQuery<T>() for LINQ-based ES|QL queries.")]
	public virtual async Task<IEnumerable<TDocument>> QueryAsObjectsAsync<TDocument>(
		Action<EsqlQueryRequestDescriptor<TDocument>> configureRequest,
		CancellationToken cancellationToken = default)
	{
		if (configureRequest is null)
			throw new ArgumentNullException(nameof(configureRequest));

		var response = await QueryAsync<TDocument>(Configure, cancellationToken).ConfigureAwait(false);

		return EsqlToObject<TDocument>(Client, response);

		void Configure(EsqlQueryRequestDescriptor<TDocument> descriptor)
		{
			configureRequest(descriptor);
			descriptor.Format(EsqlFormat.Json);
			descriptor.Columnar(false);
		}
	}

	[Obsolete("Use CreateQuery<T>() for LINQ-based ES|QL queries.")]
	private static IEnumerable<T> EsqlToObject<T>(ElasticsearchClient client, EsqlQueryResponse response)
	{
#pragma warning disable IL2026, IL3050
		using var doc = JsonSerializer.Deserialize<JsonDocument>(response.Data, EsqlJsonSerializerOptions) ?? throw new JsonException();
#pragma warning restore IL2026, IL3050

		if (!doc.RootElement.TryGetProperty("columns"u8, out var columns) || (columns.ValueKind is not JsonValueKind.Array))
			throw new JsonException("");

		if (!doc.RootElement.TryGetProperty("values"u8, out var values) || (values.ValueKind is not JsonValueKind.Array))
			yield break;

		var names = columns.EnumerateArray()
			.Select(x =>
			{
				if (!x.TryGetProperty("name"u8, out var prop))
				{
					throw new JsonException();
				}

				var result = prop.GetString() ?? throw new JsonException();

				return result;
			})
			.ToArray();

		var obj = new JsonObject();
		using var ms = new MemoryStream();
		using var writer = new Utf8JsonWriter(ms);

		foreach (var document in values.EnumerateArray())
		{
			obj.Clear();
			ms.SetLength(0);
			writer.Reset();

			var properties = names.Zip(document.EnumerateArray(),
				(key, value) => new KeyValuePair<string, JsonNode?>(key, value.ValueKind switch
				{
					JsonValueKind.Object => JsonObject.Create(value),
					JsonValueKind.Array => JsonArray.Create(value),
					_ => JsonValue.Create(value)
				}));

			foreach (var property in properties)
			{
				obj.Add(property);
			}

			obj.WriteTo(writer);
			writer.Flush();
			ms.Position = 0;

			var result = client.SourceSerializer.Deserialize<T>(ms) ?? throw new JsonException("");

			yield return result;
		}
	}

	#endregion

	[JsonSerializable(typeof(JsonDocument))]
	internal sealed partial class EsqlJsonSerializerContext :
		JsonSerializerContext;
}

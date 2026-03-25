// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Esql;
using Elastic.Esql.Execution;
using Elastic.Esql.QueryModel;
using Elastic.Transport;

#if NET10_0_OR_GREATER
using System.IO.Pipelines;
#endif

namespace Elastic.Clients.Elasticsearch.Esql;

/// <summary>
/// Implements <see cref="IEsqlQueryExecutor"/> by delegating to the native
/// <see cref="EsqlNamespacedClient"/> typed request/response pipeline.
/// </summary>
internal sealed class EsqlQueryExecutor : IEsqlQueryExecutor
{
	private readonly EsqlNamespacedClient _client;

	public EsqlQueryExecutor(EsqlNamespacedClient client)
	{
		_client = client ?? throw new ArgumentNullException(nameof(client));
	}

	public IEsqlResponse ExecuteQuery(string esql, EsqlParameters? parameters, object? options)
	{
		var queryOptions = ResolveOptions(options);
		var request = BuildQueryRequest(esql, parameters, queryOptions);
		request.BeforeRequest();
		var response = _client.DoRequest<EsqlQueryRequest, StreamResponse, EsqlQueryRequestParameters>(request);
		return new EsqlTransportResponse(response);
	}

	public async Task<IEsqlAsyncResponse> ExecuteQueryAsync(string esql, EsqlParameters? parameters, object? options, CancellationToken cancellationToken)
	{
		var queryOptions = ResolveOptions(options);
		var request = BuildQueryRequest(esql, parameters, queryOptions);
		request.BeforeRequest();
		var response = await _client.DoRequestAsync<EsqlQueryRequest, StreamResponse, EsqlQueryRequestParameters>(request, cancellationToken)
			.ConfigureAwait(false);
		return new EsqlTransportAsyncResponse(response);
	}

	public IEsqlResponse SubmitAsyncQuery(string esql, EsqlParameters? parameters, object? options, EsqlAsyncQueryOptions? asyncOptions)
	{
		var queryOptions = ResolveOptions(options);
		var request = BuildAsyncQueryRequest(esql, parameters, queryOptions, asyncOptions);
		request.BeforeRequest();
		var response = _client.DoRequest<AsyncQueryRequest, StreamResponse, AsyncQueryRequestParameters>(request);
		return new EsqlTransportResponse(response);
	}

	public async Task<IEsqlAsyncResponse> SubmitAsyncQueryAsync(string esql, EsqlParameters? parameters, object? options, EsqlAsyncQueryOptions? asyncOptions, CancellationToken cancellationToken)
	{
		var queryOptions = ResolveOptions(options);
		var request = BuildAsyncQueryRequest(esql, parameters, queryOptions, asyncOptions);
		request.BeforeRequest();
		var response = await _client.DoRequestAsync<AsyncQueryRequest, StreamResponse, AsyncQueryRequestParameters>(request, cancellationToken)
			.ConfigureAwait(false);
		return new EsqlTransportAsyncResponse(response);
	}

	public IEsqlResponse PollAsyncQuery(string queryId, object? options)
	{
		var queryOptions = ResolveOptions(options);
		var request = new AsyncQueryGetRequest(queryId) { Format = EsqlFormat.Json };
		if (queryOptions?.RequestConfiguration is not null)
			request.RequestConfiguration = queryOptions.RequestConfiguration;
		request.BeforeRequest();
		var response = _client.DoRequest<AsyncQueryGetRequest, StreamResponse, AsyncQueryGetRequestParameters>(request);
		return new EsqlTransportResponse(response);
	}

	public async Task<IEsqlAsyncResponse> PollAsyncQueryAsync(string queryId, object? options, CancellationToken cancellationToken)
	{
		var queryOptions = ResolveOptions(options);
		var request = new AsyncQueryGetRequest(queryId) { Format = EsqlFormat.Json };
		if (queryOptions?.RequestConfiguration is not null)
			request.RequestConfiguration = queryOptions.RequestConfiguration;
		request.BeforeRequest();
		var response = await _client.DoRequestAsync<AsyncQueryGetRequest, StreamResponse, AsyncQueryGetRequestParameters>(request, cancellationToken)
			.ConfigureAwait(false);
		return new EsqlTransportAsyncResponse(response);
	}

	public void DeleteAsyncQuery(string queryId, object? options)
	{
		var queryOptions = ResolveOptions(options);
		var request = new AsyncQueryDeleteRequest(queryId);
		if (queryOptions?.RequestConfiguration is not null)
			request.RequestConfiguration = queryOptions.RequestConfiguration;
		request.BeforeRequest();
		_client.DoRequest<AsyncQueryDeleteRequest, AsyncQueryDeleteResponse, AsyncQueryDeleteRequestParameters>(request);
	}

	public async Task DeleteAsyncQueryAsync(string queryId, object? options, CancellationToken cancellationToken)
	{
		var queryOptions = ResolveOptions(options);
		var request = new AsyncQueryDeleteRequest(queryId);
		if (queryOptions?.RequestConfiguration is not null)
			request.RequestConfiguration = queryOptions.RequestConfiguration;
		request.BeforeRequest();
		await _client.DoRequestAsync<AsyncQueryDeleteRequest, AsyncQueryDeleteResponse, AsyncQueryDeleteRequestParameters>(request, cancellationToken)
			.ConfigureAwait(false);
	}

	private static EsqlQueryOptions? ResolveOptions(object? options) =>
		options as EsqlQueryOptions;

	private static EsqlQueryRequest BuildQueryRequest(string esql, EsqlParameters? parameters, EsqlQueryOptions? options)
	{
		var request = new EsqlQueryRequest(esql)
		{
			Format = EsqlFormat.Json,
			Columnar = false,
			Params = MergeAndConvertParams(parameters, options?.NamedParameters)
		};

		ApplyQueryOptions(request, options);
		return request;
	}

	private static AsyncQueryRequest BuildAsyncQueryRequest(string esql, EsqlParameters? parameters, EsqlQueryOptions? queryOptions, EsqlAsyncQueryOptions? asyncOptions)
	{
		var request = new AsyncQueryRequest(esql)
		{
			Format = EsqlFormat.Json,
			Columnar = false,
			Params = MergeAndConvertParams(parameters, queryOptions?.NamedParameters)
		};

		ApplyQueryOptions(request, queryOptions);

		if (asyncOptions is not null)
		{
			if (asyncOptions.WaitForCompletionTimeout is { } waitTimeout)
				request.WaitForCompletionTimeout = new Duration(waitTimeout);

			if (asyncOptions.KeepAlive is { } keepAlive)
				request.KeepAlive = new Duration(keepAlive);

			request.KeepOnCompletion = asyncOptions.KeepOnCompletion;
		}

		return request;
	}

	private static void ApplyQueryOptions(EsqlQueryRequest request, EsqlQueryOptions? options)
	{
		if (options is null)
			return;

		request.Locale = options.Locale;
		request.Filter = options.Filter;
		request.AllowPartialResults = options.AllowPartialResults;
		request.DropNullColumns = options.DropNullColumns;

		if (options.RequestConfiguration is not null)
			request.RequestConfiguration = options.RequestConfiguration;
	}

	private static void ApplyQueryOptions(AsyncQueryRequest request, EsqlQueryOptions? options)
	{
		if (options is null)
			return;

		request.Locale = options.Locale;
		request.Filter = options.Filter;
		request.AllowPartialResults = options.AllowPartialResults;
		request.DropNullColumns = options.DropNullColumns;

		if (options.RequestConfiguration is not null)
			request.RequestConfiguration = options.RequestConfiguration;
	}

	private static Union<ICollection<ICollection<FieldValue>>, ICollection<KeyValuePair<string, ICollection<FieldValue>>>>?
		MergeAndConvertParams(EsqlParameters? translated, Dictionary<string, FieldValue>? userParams)
	{
		var hasTranslated = translated is not null && translated.HasParameters;
		var hasUser = userParams is { Count: > 0 };

		if (!hasTranslated && !hasUser)
			return null;

		var merged = new Dictionary<string, FieldValue>();

		if (hasTranslated)
		{
			foreach (var kvp in translated!.Parameters)
				merged[kvp.Key] = ConvertJsonElement(kvp.Value);
		}

		if (hasUser)
		{
			foreach (var kvp in userParams!)
				merged[kvp.Key] = kvp.Value;
		}

		var namedParams = new List<KeyValuePair<string, ICollection<FieldValue>>>(merged.Count);
		foreach (var kvp in merged)
		{
			namedParams.Add(new KeyValuePair<string, ICollection<FieldValue>>(
				kvp.Key, [kvp.Value]));
		}

		return new Union<ICollection<ICollection<FieldValue>>, ICollection<KeyValuePair<string, ICollection<FieldValue>>>>(namedParams);

		static FieldValue ConvertJsonElement(JsonElement element) =>
			element.ValueKind switch
			{
				JsonValueKind.String => FieldValue.String(element.GetString()!),
				JsonValueKind.Number when element.TryGetInt64(out var l) => FieldValue.Long(l),
				JsonValueKind.Number => FieldValue.Double(element.GetDouble()),
				JsonValueKind.True => FieldValue.True,
				JsonValueKind.False => FieldValue.False,
				JsonValueKind.Null or JsonValueKind.Undefined => FieldValue.Null,
				_ => FieldValue.String(element.GetRawText())
			};
	}
}

internal sealed class EsqlTransportResponse : IEsqlResponse
{
	private readonly StreamResponse _response;

	public EsqlTransportResponse(StreamResponse response) => _response = response;

	public Stream Body => _response.Body;

	public void Dispose() => _response.Dispose();
}

#if NET10_0_OR_GREATER
internal sealed class EsqlTransportAsyncResponse : IEsqlAsyncResponse
{
	private readonly StreamResponse _response;

	public EsqlTransportAsyncResponse(StreamResponse response)
	{
		_response = response;
		Body = PipeReader.Create(response.Body);
	}

	public PipeReader Body { get; }

	public async ValueTask DisposeAsync()
	{
		await Body.CompleteAsync().ConfigureAwait(false);
		_response.Dispose();
	}
}
#else
internal sealed class EsqlTransportAsyncResponse : IEsqlAsyncResponse
{
	private readonly StreamResponse _response;

	public EsqlTransportAsyncResponse(StreamResponse response) => _response = response;

	public Stream Body => _response.Body;

	public ValueTask DisposeAsync()
	{
		_response.Dispose();
		return default;
	}
}
#endif

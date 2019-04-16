using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary> The SQL Translate API accepts SQL in a JSON document and translates it into a native Elasticsearch search request</summary>
		ITranslateSqlResponse TranslateSql(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		ITranslateSqlResponse TranslateSql(ITranslateSqlRequest request);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		Task<ITranslateSqlResponse> TranslateSqlAsync(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		Task<ITranslateSqlResponse> TranslateSqlAsync(ITranslateSqlRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public ITranslateSqlResponse TranslateSql(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null) =>
			TranslateSql(selector.InvokeOrDefault(new TranslateSqlDescriptor()));

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public ITranslateSqlResponse TranslateSql(ITranslateSqlRequest request)
		{
			request.RequestParameters.DeserializationOverride = ToTranslateSqlResponse;
			return DoRequest<ITranslateSqlRequest, TranslateSqlResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public Task<ITranslateSqlResponse> TranslateSqlAsync(
			Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null,
			CancellationToken ct = default
		) => TranslateSqlAsync(selector.InvokeOrDefault(new TranslateSqlDescriptor()), ct);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public Task<ITranslateSqlResponse> TranslateSqlAsync(ITranslateSqlRequest request, CancellationToken ct = default)
		{
			request.RequestParameters.DeserializationOverride = ToTranslateSqlResponse;
			return DoRequestAsync<ITranslateSqlRequest, ITranslateSqlResponse, TranslateSqlResponse>
				(request, request.RequestParameters, ct);
		}

		private TranslateSqlResponse ToTranslateSqlResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			var result = RequestResponseSerializer.Deserialize<ISearchRequest>(stream);
			return new TranslateSqlResponse { Result = result };
		}
	}
}

using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary> The SQL Translate API accepts SQL in a JSON document and translates it into a native Elasticsearch search request</summary>
		///
		ITranslateSqlResponse TranslateSql(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		ITranslateSqlResponse TranslateSql(ITranslateSqlRequest request);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		Task<ITranslateSqlResponse> TranslateSqlAsync(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		Task<ITranslateSqlResponse> TranslateSqlAsync(ITranslateSqlRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		public ITranslateSqlResponse TranslateSql(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null) =>
			this.TranslateSql(selector.InvokeOrDefault(new TranslateSqlDescriptor()));

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		public ITranslateSqlResponse TranslateSql(ITranslateSqlRequest request) =>
			this.Dispatcher.Dispatch<ITranslateSqlRequest, TranslateSqlRequestParameters, TranslateSqlResponse>(
				request,
				this.ToTranslateSqlResponse,
				(p, d) =>this.LowLevelDispatch.XpackSqlTranslateDispatch<TranslateSqlResponse>(p, d)
			);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		public Task<ITranslateSqlResponse> TranslateSqlAsync(Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.TranslateSqlAsync(selector.InvokeOrDefault(new TranslateSqlDescriptor()), cancellationToken);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})"/>
		public Task<ITranslateSqlResponse> TranslateSqlAsync(ITranslateSqlRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ITranslateSqlRequest, TranslateSqlRequestParameters, TranslateSqlResponse, ITranslateSqlResponse>(
				request,
				cancellationToken,
				this.ToTranslateSqlResponse,
				(p, d, c) => this.LowLevelDispatch.XpackSqlTranslateDispatchAsync<TranslateSqlResponse>(p, d, c)
			);

		private TranslateSqlResponse ToTranslateSqlResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			var result = this.SourceSerializer.Deserialize<ISearchRequest>(stream);
			return new TranslateSqlResponse { Result = result };
		}
	}

}

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document,
		/// without any additional content around it.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="document">The document path</param>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class;

		/// <inheritdoc/>
		T Source<T>(ISourceRequest request) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(
			DocumentPath<T> document,
			Func<SourceDescriptor<T>, ISourceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(ISourceRequest request, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class =>
			this.Source<T>(selector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public T Source<T>(ISourceRequest request) where T : class
		{
			request.RouteValues.Resolve(ConnectionSettings);
			return this.Dispatcher.Dispatch<ISourceRequest, SourceRequestParameters, SourceResponse<T>>(
				request,
				this.ToSourceResponse<T>,
				(p, d) => this.LowLevelDispatch.GetSourceDispatch<SourceResponse<T>>(p)
			).Body;
		}

		/// <inheritdoc/>
		public Task<T> SourceAsync<T>(
			DocumentPath<T> document,
			Func<SourceDescriptor<T>, ISourceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class => this.SourceAsync<T>(selector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)), cancellationToken);

		/// <inheritdoc/>
		public async Task<T> SourceAsync<T>(ISourceRequest request, CancellationToken cancellationToken = default(CancellationToken)) where T : class
		{
			request.RouteValues.Resolve(ConnectionSettings);
			var result = await this.Dispatcher.DispatchAsync<ISourceRequest, SourceRequestParameters, SourceResponse<T>, ISourceResponse<T>>(
				request,
				cancellationToken,
				this.ToSourceResponse<T>,
				(p, d, c) => this.LowLevelDispatch.GetSourceDispatchAsync<SourceResponse<T>>(p, c)
			).ConfigureAwait(false);
			return result.Body;
		}

		private SourceResponse<T> ToSourceResponse<T>(IApiCallDetails apiCallDetails, Stream stream) where T : class
		{
			var source = this.SourceSerializer.Deserialize<T>(stream);
			return new SourceResponse<T>
			{
				Body = source,
			};
		}
	}
}

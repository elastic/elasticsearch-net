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
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="document">The document path</param>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		TDocument Source<TDocument>(DocumentPath<TDocument> document, Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		TDocument Source<TDocument>(ISourceRequest request) where TDocument : class;

		/// <inheritdoc />
		Task<TDocument> SourceAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class;

		/// <inheritdoc />
		Task<TDocument> SourceAsync<TDocument>(ISourceRequest request, CancellationToken cancellationToken = default(CancellationToken)) where TDocument : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public TDocument Source<TDocument>(DocumentPath<TDocument> document, Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null) where TDocument : class =>
			Source<TDocument>(selector.InvokeOrDefault(new SourceDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public TDocument Source<TDocument>(ISourceRequest request) where TDocument : class
		{
			request.RouteValues.Resolve(ConnectionSettings);
			return Dispatcher.Dispatch<ISourceRequest, SourceRequestParameters, SourceResponse<TDocument>>(
					request,
					ToSourceResponse<TDocument>,
					(p, d) => LowLevelDispatch.GetSourceDispatch<SourceResponse<TDocument>>(p)
				)
				.Body;
		}

		/// <inheritdoc />
		public Task<TDocument> SourceAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<SourceDescriptor<TDocument>, ISourceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class =>
			SourceAsync<TDocument>(selector.InvokeOrDefault(new SourceDescriptor<TDocument>(document.Self.Index, document.Self.Id)),
				cancellationToken);

		/// <inheritdoc />
		public async Task<TDocument> SourceAsync<TDocument>(ISourceRequest request, CancellationToken cancellationToken = default) where TDocument : class
		{
			request.RouteValues.Resolve(ConnectionSettings);
			var result = await Dispatcher.DispatchAsync<ISourceRequest, SourceRequestParameters, SourceResponse<TDocument>, ISourceResponse<TDocument>>(
					request,
					cancellationToken,
					ToSourceResponse<TDocument>,
					(p, d, c) => LowLevelDispatch.GetSourceDispatchAsync<SourceResponse<TDocument>>(p, c)
				)
				.ConfigureAwait(false);
			return result.Body;
		}

		private SourceResponse<TDocument> ToSourceResponse<TDocument>(IApiCallDetails apiCallDetails, Stream stream) where TDocument : class
		{
			var source = SourceSerializer.Deserialize<TDocument>(stream);
			return new SourceResponse<TDocument>
			{
				Body = source,
			};
		}
	}
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeleteScriptResponse DeleteScript(this IElasticClient client,Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null);

		/// <inheritdoc />
		public static DeleteScriptResponse DeleteScript(this IElasticClient client,IDeleteScriptRequest request);

		/// <inheritdoc />
		public static Task<DeleteScriptResponse> DeleteScriptAsync(this IElasticClient client,Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteScriptResponse> DeleteScriptAsync(this IElasticClient client,IDeleteScriptRequest request, CancellationToken ct = default);
	}

}

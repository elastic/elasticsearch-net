using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetScriptResponse GetScript(this IElasticClient client,Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null);

		/// <inheritdoc />
		public static GetScriptResponse GetScript(this IElasticClient client,IGetScriptRequest request);

		/// <inheritdoc />
		public static Task<GetScriptResponse> GetScriptAsync(this IElasticClient client,Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetScriptResponse> GetScriptAsync(this IElasticClient client,IGetScriptRequest request, CancellationToken ct = default);
	}


}

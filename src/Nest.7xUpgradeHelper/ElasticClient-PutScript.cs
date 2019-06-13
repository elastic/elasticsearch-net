using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PutScriptResponse PutScript(this IElasticClient client,Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector);

		/// <inheritdoc />
		public static PutScriptResponse PutScript(this IElasticClient client,IPutScriptRequest request);

		/// <inheritdoc />
		public static Task<PutScriptResponse> PutScriptAsync(this IElasticClient client,Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutScriptResponse> PutScriptAsync(this IElasticClient client,IPutScriptRequest request, CancellationToken ct = default);
	}

}

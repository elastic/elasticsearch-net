using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static XPackInfoResponse XPackInfo(this IElasticClient client,Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null);

		/// <inheritdoc />
		public static XPackInfoResponse XPackInfo(this IElasticClient client,IXPackInfoRequest request);

		/// <inheritdoc />
		public static Task<XPackInfoResponse> XPackInfoAsync(this IElasticClient client,Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<XPackInfoResponse> XPackInfoAsync(this IElasticClient client,IXPackInfoRequest request, CancellationToken ct = default);
	}

}

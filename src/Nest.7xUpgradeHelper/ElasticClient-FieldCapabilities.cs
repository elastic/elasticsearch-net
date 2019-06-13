using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static FieldCapabilitiesResponse FieldCapabilities(this IElasticClient client,Indices indices, Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null);

		/// <inheritdoc />
		public static FieldCapabilitiesResponse FieldCapabilities(this IElasticClient client,IFieldCapabilitiesRequest request);

		/// <inheritdoc />
		public static Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(this IElasticClient client,Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(this IElasticClient client,IFieldCapabilitiesRequest request,
			CancellationToken ct = default
		);
	}

}

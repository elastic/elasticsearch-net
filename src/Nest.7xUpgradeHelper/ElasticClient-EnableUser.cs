using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static EnableUserResponse EnableUser(this IElasticClient client,Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null);

		/// <inheritdoc />
		public static EnableUserResponse EnableUser(this IElasticClient client,IEnableUserRequest request);

		/// <inheritdoc />
		public static Task<EnableUserResponse> EnableUserAsync(this IElasticClient client,Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<EnableUserResponse> EnableUserAsync(this IElasticClient client,IEnableUserRequest request, CancellationToken ct = default);
	}

}

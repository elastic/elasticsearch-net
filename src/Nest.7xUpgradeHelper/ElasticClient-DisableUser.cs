using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DisableUserResponse DisableUser(this IElasticClient client,Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null);

		/// <inheritdoc />
		public static DisableUserResponse DisableUser(this IElasticClient client,IDisableUserRequest request);

		/// <inheritdoc />
		public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client,Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DisableUserResponse> DisableUserAsync(this IElasticClient client,IDisableUserRequest request, CancellationToken ct = default);
	}

}

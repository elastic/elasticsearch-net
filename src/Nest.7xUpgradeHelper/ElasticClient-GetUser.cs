using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetUserResponse GetUser(this IElasticClient client,Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc />
		public static GetUserResponse GetUser(this IElasticClient client,IGetUserRequest request);

		/// <inheritdoc />
		public static Task<GetUserResponse> GetUserAsync(this IElasticClient client,Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetUserResponse> GetUserAsync(this IElasticClient client,IGetUserRequest request, CancellationToken ct = default);
	}

}

using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeleteUserResponse DeleteUser(this IElasticClient client,Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc />
		public static DeleteUserResponse DeleteUser(this IElasticClient client,IDeleteUserRequest request);

		/// <inheritdoc />
		public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client,Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteUserResponse> DeleteUserAsync(this IElasticClient client,IDeleteUserRequest request, CancellationToken ct = default);
	}

}

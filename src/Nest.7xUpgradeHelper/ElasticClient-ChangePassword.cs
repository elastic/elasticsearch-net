using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static ChangePasswordResponse ChangePassword(this IElasticClient client,Func<ChangePasswordDescriptor, IChangePasswordRequest> selector);

		/// <inheritdoc />
		public static ChangePasswordResponse ChangePassword(this IElasticClient client,IChangePasswordRequest request);

		/// <inheritdoc />
		public static Task<ChangePasswordResponse> ChangePasswordAsync(this IElasticClient client,Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ChangePasswordResponse> ChangePasswordAsync(this IElasticClient client,IChangePasswordRequest request,
			CancellationToken ct = default
		);
	}

}

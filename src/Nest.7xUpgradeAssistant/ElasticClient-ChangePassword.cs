using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.ChangePassword(), please update this usage.")]
		public static ChangePasswordResponse ChangePassword(this IElasticClient client,
			Func<ChangePasswordDescriptor, IChangePasswordRequest> selector
		)
			=> client.Security.ChangePassword(selector);

		[Obsolete("Moved to client.Security.ChangePassword(), please update this usage.")]
		public static ChangePasswordResponse ChangePassword(this IElasticClient client, IChangePasswordRequest request)
			=> client.Security.ChangePassword(request);

		[Obsolete("Moved to client.Security.ChangePasswordAsync(), please update this usage.")]
		public static Task<ChangePasswordResponse> ChangePasswordAsync(this IElasticClient client,
			Func<ChangePasswordDescriptor, IChangePasswordRequest> selector,
			CancellationToken ct = default
		)
			=> client.Security.ChangePasswordAsync(selector, ct);

		[Obsolete("Moved to client.Security.ChangePasswordAsync(), please update this usage.")]
		public static Task<ChangePasswordResponse> ChangePasswordAsync(this IElasticClient client, IChangePasswordRequest request,
			CancellationToken ct = default
		)
			=> client.Security.ChangePasswordAsync(request, ct);
	}
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Nodes.ReloadSecureSettings(), please update this usage.")]
		public static ReloadSecureSettingsResponse ReloadSecureSettings(this IElasticClient client,
			Func<ReloadSecureSettingsDescriptor, IReloadSecureSettingsRequest> selector = null
		)
			=> client.Nodes.ReloadSecureSettings(selector);

		[Obsolete("Moved to client.Nodes.ReloadSecureSettings(), please update this usage.")]
		public static ReloadSecureSettingsResponse ReloadSecureSettings(this IElasticClient client, IReloadSecureSettingsRequest request)
			=> client.Nodes.ReloadSecureSettings(request);

		[Obsolete("Moved to client.Nodes.ReloadSecureSettingsAsync(), please update this usage.")]
		public static Task<ReloadSecureSettingsResponse> ReloadSecureSettingsAsync(this IElasticClient client,
			Func<ReloadSecureSettingsDescriptor, IReloadSecureSettingsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.ReloadSecureSettingsAsync(selector, ct);

		[Obsolete("Moved to client.Nodes.ReloadSecureSettingsAsync(), please update this usage.")]
		public static Task<ReloadSecureSettingsResponse> ReloadSecureSettingsAsync(this IElasticClient client, IReloadSecureSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Nodes.ReloadSecureSettingsAsync(request, ct);
	}
}

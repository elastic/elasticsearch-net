using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.UpdateSettings(), please update this usage.")]
		public static UpdateIndexSettingsResponse UpdateIndexSettings(this IElasticClient client, Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector
		)
			=> client.Indices.UpdateSettings(indices, selector);

		[Obsolete("Moved to client.Indices.UpdateSettings(), please update this usage.")]
		public static UpdateIndexSettingsResponse UpdateIndexSettings(this IElasticClient client, IUpdateIndexSettingsRequest request)
			=> client.Indices.UpdateSettings(request);

		[Obsolete("Moved to client.Indices.UpdateSettingsAsync(), please update this usage.")]
		public static Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(this IElasticClient client,
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.UpdateSettingsAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.UpdateSettingsAsync(), please update this usage.")]
		public static Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(this IElasticClient client, IUpdateIndexSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.UpdateSettingsAsync(request, ct);
	}
}

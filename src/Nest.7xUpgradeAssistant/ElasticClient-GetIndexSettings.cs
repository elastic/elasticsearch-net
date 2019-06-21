using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.GetSettings(), please update this usage.")]
		public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client,
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector
		)
			=> client.Indices.GetSettings(AllIndices, selector);

		[Obsolete("Moved to client.Indices.GetSettings(), please update this usage.")]
		public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client, IGetIndexSettingsRequest request)
			=> client.Indices.GetSettings(request);

		[Obsolete("Moved to client.Indices.GetSettingsAsync(), please update this usage.")]
		public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client,
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.GetSettingsAsync(AllIndices, selector, ct);

		[Obsolete("Moved to client.Indices.GetSettingsAsync(), please update this usage.")]
		public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client, IGetIndexSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.GetSettingsAsync(request, ct);
	}
}

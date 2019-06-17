using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Change specific index level settings in real time. Note not all index settings CAN be updated.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that strongly types all the updateable settings</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateIndexSettingsResponse UpdateIndexSettings(this IElasticClient client, Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector
		)
			=> client.Indices.UpdateSettings(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateIndexSettingsResponse UpdateIndexSettings(this IElasticClient client, IUpdateIndexSettingsRequest request)
			=> client.Indices.UpdateSettings(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(this IElasticClient client,
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.UpdateSettingsAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(this IElasticClient client, IUpdateIndexSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.UpdateSettingsAsync(request, ct);
	}
}

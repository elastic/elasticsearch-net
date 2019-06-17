using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client,
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector
		)
			=> client.Indices.GetSettings(AllIndices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client, IGetIndexSettingsRequest request)
			=> client.Indices.GetSettings(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client,
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.GetSettingsAsync(AllIndices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client, IGetIndexSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.GetSettingsAsync(request);
	}
}

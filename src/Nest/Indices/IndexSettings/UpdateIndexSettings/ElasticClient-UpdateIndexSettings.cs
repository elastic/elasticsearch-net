using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Change specific index level settings in real time. Note not all index settings CAN be updated.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that strongly types all the updateable settings</param>
		UpdateIndexSettingsResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector);

		/// <inheritdoc />
		UpdateIndexSettingsResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request);

		/// <inheritdoc />
		Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateIndexSettingsResponse UpdateIndexSettings(Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector
		) =>
			UpdateIndexSettings(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc />
		public UpdateIndexSettingsResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request) =>
			DoRequest<IUpdateIndexSettingsRequest, UpdateIndexSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken ct = default
		) => UpdateIndexSettingsAsync(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<UpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateIndexSettingsRequest, UpdateIndexSettingsResponse>(request, request.RequestParameters, ct);
	}
}

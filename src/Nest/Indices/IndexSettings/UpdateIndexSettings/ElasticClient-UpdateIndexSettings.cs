using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Change specific index level settings in real time. Note not all index settings CAN be updated.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that strongly types all the updateable settings</param>
		IAcknowledgedResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector) => 
			this.UpdateIndexSettings(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request) => 
			this.Dispatcher.Dispatch<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector)=>
			this.UpdateIndexSettingsAsync(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request) => 
			this.Dispatcher.DispatchAsync<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>
			);
	}
}
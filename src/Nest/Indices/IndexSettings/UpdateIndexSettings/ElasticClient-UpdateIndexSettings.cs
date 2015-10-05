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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html
		/// </summary>
		/// <param name="updateSettingsSelector">A descriptor that strongly types all the updateable settings</param>
		IAcknowledgedResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> updateSettingsSelector);

		/// <inheritdoc/>
		IAcknowledgedResponse UpdateIndexSettings(IUpdateIndexSettingsRequest updateSettingsRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> updateSettingsSelector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest updateSettingsRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> updateSettingsSelector) => 
			this.UpdateIndexSettings(updateSettingsSelector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(IUpdateIndexSettingsRequest updateSettingsRequest) => 
			this.Dispatcher.Dispatch<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsRequest,
				this.LowLevelDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> updateSettingsSelector)=>
			this.UpdateIndexSettingsAsync(updateSettingsSelector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest updateSettingsRequest) => 
			this.Dispatcher.DispatchAsync<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				updateSettingsRequest,
				this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>
			);
	}
}
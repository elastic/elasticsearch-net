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
		IAcknowledgedResponse UpdateIndexSettings(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector);

		/// <inheritdoc/>
		IAcknowledgedResponse UpdateIndexSettings(IUpdateSettingsRequest updateSettingsRequest);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateSettingsRequest updateSettingsRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector) => 
			this.UpdateIndexSettings(updateSettingsSelector?.Invoke(new UpdateSettingsDescriptor()));

		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(IUpdateSettingsRequest updateSettingsRequest) => 
			this.Dispatcher.Dispatch<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsRequest,
				this.LowLevelDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector)=>
			this.UpdateIndexSettingsAsync(updateSettingsSelector?.Invoke(new UpdateSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateSettingsRequest updateSettingsRequest) => 
			this.Dispatcher.DispatchAsync<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				updateSettingsRequest,
				this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>
			);
	}
}
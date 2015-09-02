using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector)
		{
			return this.Dispatcher.Dispatch<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsSelector?.Invoke(new UpdateSettingsDescriptor()),
				(p, d) => this.LowLevelDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IAcknowledgedResponse UpdateIndexSettings(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(Func<UpdateSettingsDescriptor, IUpdateSettingsRequest> updateSettingsSelector)
		{
			return this.Dispatcher.DispatchAsync<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsSelector?.Invoke(new UpdateSettingsDescriptor()),
					(p, d) => this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> UpdateIndexSettingsAsync(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsRequest,
					(p, d) => this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

	}
}
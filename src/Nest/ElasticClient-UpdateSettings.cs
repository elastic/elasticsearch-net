using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse UpdateSettings(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector)
		{
			return this.Dispatch<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IAcknowledgedResponse UpdateSettings(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.Dispatch<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsRequest,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> UpdateSettingsAsync(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector)
		{
			return this.DispatchAsync<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsSelector,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> UpdateSettingsAsync(IUpdateSettingsRequest updateSettingsRequest)
		{
			return this.DispatchAsync<IUpdateSettingsRequest, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsRequest,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}

	}
}
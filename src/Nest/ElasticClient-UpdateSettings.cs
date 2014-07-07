using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse UpdateSettings(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
			)
		{
			return this.Dispatch<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> UpdateSettingsAsync(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
			)
		{
			return this.DispatchAsync
				<UpdateSettingsDescriptor, UpdateSettingsRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
					updateSettingsSelector,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<AcknowledgedResponse>(p, d)
				);
		}
	}
}
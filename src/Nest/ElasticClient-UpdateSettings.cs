using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISettingsOperationResponse UpdateSettings(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
			)
		{
			return this.Dispatch<UpdateSettingsDescriptor, UpdateSettingsQueryString, SettingsOperationResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch<SettingsOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<ISettingsOperationResponse> UpdateSettingsAsync(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
			)
		{
			return this.DispatchAsync
				<UpdateSettingsDescriptor, UpdateSettingsQueryString, SettingsOperationResponse, ISettingsOperationResponse>(
					updateSettingsSelector,
					(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync<SettingsOperationResponse>(p, d)
				);
		}
	}
}
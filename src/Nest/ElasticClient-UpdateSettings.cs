using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{
		public ISettingsOperationResponse UpdateSettings(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
		)
		{
			return this.Dispatch<UpdateSettingsDescriptor, UpdateSettingsQueryString, SettingsOperationResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatch(p, d)
			);
		}

		public Task<ISettingsOperationResponse> UpdateSettingsAsync(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
		)
		{
			return this.DispatchAsync<UpdateSettingsDescriptor, UpdateSettingsQueryString, SettingsOperationResponse, ISettingsOperationResponse>(
				updateSettingsSelector,
				(p, d) => this.RawDispatch.IndicesPutSettingsDispatchAsync(p, d)
			);
		}

	}
}
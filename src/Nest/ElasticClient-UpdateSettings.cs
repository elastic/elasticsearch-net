using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// Update index settings
		/// </summary>
		public ISettingsOperationResponse UpdateSettings(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
		)
		{
			updateSettingsSelector.ThrowIfNull("updateSettingsSelector");
			var descriptor = updateSettingsSelector(new UpdateSettingsDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesPutSettingsDispatch(pathInfo, descriptor)
				.Deserialize<SettingsOperationResponse>();
		}

		/// <summary>
		/// Update index settings
		/// </summary>
		public Task<ISettingsOperationResponse> UpdateSettingsAsync(
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector
		)
		{
			updateSettingsSelector.ThrowIfNull("updateSettingsSelector");
			var descriptor = updateSettingsSelector(new UpdateSettingsDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesPutSettingsDispatchAsync(pathInfo, descriptor)
				.ContinueWith<ISettingsOperationResponse>(
					t => t.Result.Deserialize<SettingsOperationResponse>()
				);
		}


	}
}
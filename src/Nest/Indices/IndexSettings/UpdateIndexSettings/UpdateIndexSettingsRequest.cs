using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexSettingsConverter))]
	public partial interface IUpdateIndexSettingsRequest
	{
		IDynamicIndexSettings IndexSettings { get; set; }
	}

	public partial class UpdateIndexSettingsRequest 
	{
		public IDynamicIndexSettings IndexSettings { get; set; }
	}

	[DescriptorFor("IndicesPutSettings")]
	public partial class UpdateIndexSettingsDescriptor 
	{
		IDynamicIndexSettings IUpdateIndexSettingsRequest.IndexSettings { get; set; }

		/// <inheritdoc/>
		public UpdateIndexSettingsDescriptor IndexSettings(Func<DynamicIndexSettingsDescriptor, IPromise<IDynamicIndexSettings>> settings) =>
			Assign(a => a.IndexSettings = settings?.Invoke(new DynamicIndexSettingsDescriptor())?.Value);

	}
}

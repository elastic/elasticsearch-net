using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClusterPutSettingsRequest 
	{
		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }

	}
	
	public partial class ClusterPutSettingsRequest 
	{
		public IDictionary<string, object> Persistent { get; set; }

		public IDictionary<string, object> Transient { get; set; }
	}

	[DescriptorFor("ClusterPutSettings")]
	public partial class ClusterPutSettingsDescriptor 
	{
		IDictionary<string, object> IClusterPutSettingsRequest.Persistent { get; set; }

		IDictionary<string, object> IClusterPutSettingsRequest.Transient { get; set; }

		public ClusterPutSettingsDescriptor Persistent(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Persistent = selector?.Invoke(new FluentDictionary<string, object>()));

		public ClusterPutSettingsDescriptor Transient(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Transient = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}

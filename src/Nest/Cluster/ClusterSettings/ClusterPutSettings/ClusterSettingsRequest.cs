using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClusterSettingsRequest 
	{
		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }

	}
	
	public partial class ClusterSettingsRequest 
	{
		public IDictionary<string, object> Persistent { get; set; }

		public IDictionary<string, object> Transient { get; set; }
	}

	[DescriptorFor("ClusterPutSettings")]
	public partial class ClusterSettingsDescriptor 
	{
		IDictionary<string, object> IClusterSettingsRequest.Persistent { get; set; }

		IDictionary<string, object> IClusterSettingsRequest.Transient { get; set; }

		public ClusterSettingsDescriptor Persistent(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Persistent = selector?.Invoke(new FluentDictionary<string, object>()));

		public ClusterSettingsDescriptor Transient(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Transient = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}

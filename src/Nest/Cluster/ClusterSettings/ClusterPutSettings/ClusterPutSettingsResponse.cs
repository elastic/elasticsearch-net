using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterPutSettingsResponse : IResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		bool Acknowledged { get; }

		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }
	}

	public class ClusterPutSettingsResponse : ResponseBase, IClusterPutSettingsResponse
	{
		public bool Acknowledged { get; internal set; }
		public IDictionary<string, object> Persistent { get; set; }
		public IDictionary<string, object> Transient { get; set; }
	}
}
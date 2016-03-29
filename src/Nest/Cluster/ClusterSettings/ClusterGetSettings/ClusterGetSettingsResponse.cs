using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterGetSettingsResponse : IResponse
	{
		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }
	}

	public class ClusterGetSettingsResponse : ResponseBase, IClusterGetSettingsResponse
	{
		public IDictionary<string, object> Persistent { get; set; }
		public IDictionary<string, object> Transient { get; set; }
	}
}
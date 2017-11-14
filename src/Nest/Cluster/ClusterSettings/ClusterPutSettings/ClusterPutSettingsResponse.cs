using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterPutSettingsResponse : IResponse
	{
		[JsonProperty("acknowledged")]
		bool Acknowledged { get; }

		[JsonProperty("persistent")]
		IReadOnlyDictionary<string, object> Persistent { get; }

		[JsonProperty("transient")]
		IReadOnlyDictionary<string, object> Transient { get; }
	}

	public class ClusterPutSettingsResponse : ResponseBase, IClusterPutSettingsResponse
	{
		public bool Acknowledged { get; internal set; }
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

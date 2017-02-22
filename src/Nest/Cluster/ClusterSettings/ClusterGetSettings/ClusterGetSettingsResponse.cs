using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IClusterGetSettingsResponse : IResponse
	{
		[JsonProperty(PropertyName = "persistent")]
		IReadOnlyDictionary<string, object> Persistent { get; }

		[JsonProperty(PropertyName = "transient")]
		IReadOnlyDictionary<string, object> Transient { get; }
	}

	public class ClusterGetSettingsResponse : ResponseBase, IClusterGetSettingsResponse
	{
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

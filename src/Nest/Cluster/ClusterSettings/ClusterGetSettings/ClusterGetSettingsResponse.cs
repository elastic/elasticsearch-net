using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterGetSettingsResponse : IResponse
	{
		[JsonProperty("persistent")]
		IReadOnlyDictionary<string, object> Persistent { get; }

		[JsonProperty("transient")]
		IReadOnlyDictionary<string, object> Transient { get; }
	}

	public class ClusterGetSettingsResponse : ResponseBase, IClusterGetSettingsResponse
	{
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

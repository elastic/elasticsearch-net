using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClusterGetSettingsResponse : IResponse
	{
		[DataMember(Name ="persistent")]
		IReadOnlyDictionary<string, object> Persistent { get; }

		[DataMember(Name ="transient")]
		IReadOnlyDictionary<string, object> Transient { get; }
	}

	public class ClusterGetSettingsResponse : ResponseBase, IClusterGetSettingsResponse
	{
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

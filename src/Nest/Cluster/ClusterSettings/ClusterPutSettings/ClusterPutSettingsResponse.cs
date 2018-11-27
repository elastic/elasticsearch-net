using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClusterPutSettingsResponse : IResponse
	{
		[DataMember(Name ="acknowledged")]
		bool Acknowledged { get; }

		[DataMember(Name ="persistent")]
		IReadOnlyDictionary<string, object> Persistent { get; }

		[DataMember(Name ="transient")]
		IReadOnlyDictionary<string, object> Transient { get; }
	}

	public class ClusterPutSettingsResponse : ResponseBase, IClusterPutSettingsResponse
	{
		public bool Acknowledged { get; internal set; }
		public IReadOnlyDictionary<string, object> Persistent { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
		public IReadOnlyDictionary<string, object> Transient { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}

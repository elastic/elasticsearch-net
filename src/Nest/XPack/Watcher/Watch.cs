using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class Watch
	{
		[DataMember(Name ="actions")]
		[JsonFormatter(typeof(ActionsFormatter))]
		public Actions Actions { get; internal set; }

		[DataMember(Name ="condition")]
		public IConditionContainer Condition { get; internal set; }

		[DataMember(Name ="input")]
		public IInputContainer Input { get; internal set; }

		[DataMember(Name ="metadata")]
		public IReadOnlyDictionary<string, object> Meta { get; internal set; }

		[DataMember(Name ="status")]
		public WatchStatus Status { get; internal set; }

		[DataMember(Name ="throttle_period")]
		public string ThrottlePeriod { get; internal set; }

		[DataMember(Name ="transform")]
		public ITransformContainer Transform { get; internal set; }

		[DataMember(Name ="trigger")]
		public ITriggerContainer Trigger { get; internal set; }
	}
}

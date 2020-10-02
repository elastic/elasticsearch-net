// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class TaskInfo
	{
		[DataMember(Name ="action")]
		public string Action { get; internal set; }

		[DataMember(Name ="cancellable")]
		public bool Cancellable { get; internal set; }

		[DataMember(Name ="children")]
		public IReadOnlyCollection<TaskInfo> Children { get; internal set; } = EmptyReadOnly<TaskInfo>.Collection;

		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="headers")]
		public IReadOnlyDictionary<string, string> Headers { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name ="id")]
		public long Id { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="running_time_in_nanos")]
		public long RunningTimeInNanoseconds { get; internal set; }

		[DataMember(Name ="start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="status")]
		public TaskStatus Status { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}

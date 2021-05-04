// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class CatTasksRecord : ICatRecord
	{
		[DataMember(Name ="action")]
		public string Action { get; internal set; }

		[DataMember(Name ="ip")]
		public string Ip { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="parent_task_id")]
		public string ParentTaskId { get; internal set; }

		[DataMember(Name ="running_time")]
		public string RunningTime { get; internal set; }

		[DataMember(Name ="start_time")]
		public string StartTime { get; internal set; }

		[DataMember(Name ="task_id")]
		public string TaskId { get; internal set; }

		[DataMember(Name ="timestamp")]
		public string Timestamp { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}

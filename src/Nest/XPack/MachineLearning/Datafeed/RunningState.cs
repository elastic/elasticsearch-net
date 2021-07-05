// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The running state for a datafeed once started.
	/// </summary>
	[DataContract]
	public class RunningState
	{
		[DataMember(Name = "is_real_time")]
		public bool IsRealTime { get; internal set; }

		[DataMember(Name = "look_back_finished")]
		public bool LoopBackFinished { get; internal set; }
	}
}

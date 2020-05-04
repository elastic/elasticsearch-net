// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum ActionExecutionMode
	{
		/// <summary>
		/// The action will be simulated (not actually executed)
		///  and it will be throttled if needed.
		/// </summary>
		[EnumMember(Value = "simulate")]
		Simulate,

		/// <summary>
		/// The action will be simulated (not actually executed) and it will
		/// not be throttled.
		/// </summary>
		[EnumMember(Value = "force_simulate")]
		ForceSimulate,

		/// <summary>
		/// The action will be executed and it will be throttled if needed.
		/// </summary>
		[EnumMember(Value = "execute")]
		Execute,

		/// <summary>
		/// The action will be executed and it will not be throttled.
		/// </summary>
		[EnumMember(Value = "force_execute")]
		ForceExecute,

		/// <summary>
		/// The action will be skipped (it won't be executed nor simulated)
		/// - effectively it will be forcefully throttled
		/// </summary>
		[EnumMember(Value = "skip")]
		Skip
	}
}

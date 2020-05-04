// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// The status of the datafeed
	/// </summary>
	[StringEnum]
	public enum DatafeedState
	{
		/// <summary>
		/// The datafeed is actively receiving data.
		/// </summary>
		[EnumMember(Value = "started")]
		Started,

		/// <summary>
		/// The datafeed is stopped and will not receive data until it is re-started.
		/// </summary>
		[EnumMember(Value = "stopped")]
		Stopped,

		/// <summary>
		/// The datafeed has been requested to start but has not yet started.
		/// </summary>
		[EnumMember(Value = "starting")]
		Starting,

		/// <summary>
		/// The datafeed has been requested to stop gracefully and is completing its final action.
		/// </summary>
		[EnumMember(Value = "stopping")]
		Stopping
	}
}

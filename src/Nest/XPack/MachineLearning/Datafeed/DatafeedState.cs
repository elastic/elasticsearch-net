/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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

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
	/// The state of a machine learning job
	/// </summary>
	[StringEnum]
	public enum JobState
	{
		/// <summary>
		/// The job close action is in progress and has not yet completed. A closing job cannot accept further data.
		/// </summary>
		[EnumMember(Value = "closing")]
		Closing,

		/// <summary>
		/// The job finished successfully with its model state persisted.
		/// The job must be opened before it can accept further data.
		/// </summary>
		[EnumMember(Value = "closed")]
		Closed,

		/// <summary>
		/// The job is available to receive and process data.
		/// </summary>
		[EnumMember(Value = "opened")]
		Opened,

		/// <summary>
		/// The job did not finish successfully due to an error. This situation can occur due to invalid input data.
		/// If the job had irrevocably failed, it must be force closed and then deleted.
		/// If the datafeed can be corrected, the job can be closed and then re-opened.
		/// </summary>
		[EnumMember(Value = "failed")]
		Failed,

		/// <summary>
		/// The job open action is in progress and has not yet completed.
		/// </summary>
		[EnumMember(Value = "opening")]
		Opening
	}
}

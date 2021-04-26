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

namespace Nest
{
	/// <summary>
	/// Gets data stream statistics
	/// </summary>
	[MapsApi("indices.data_streams_stats.json")]
	[ReadAs(typeof(DataStreamsStatsRequest))]
	public partial interface IDataStreamsStatsRequest
	{
	}

	/// <inheritdoc cref="IDataStreamsStatsRequest"/>
	public partial class DataStreamsStatsRequest
	{
	}

	/// <inheritdoc cref="IDataStreamsStatsRequest"/>
	public partial class DataStreamsStatsDescriptor
	{
	}
}

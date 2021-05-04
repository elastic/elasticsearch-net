// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Converts an index alias to a data stream.
	/// </summary>
	[MapsApi("indices.migrate_to_data_stream.json")]
	[ReadAs(typeof(MigrateToDataStreamRequest))]
	public partial interface IMigrateToDataStreamRequest
	{
	}

	/// <inheritdoc cref="IMigrateToDataStreamRequest"/>
	public partial class MigrateToDataStreamRequest : IMigrateToDataStreamRequest
	{
	}

	/// <inheritdoc cref="IMigrateToDataStreamRequest"/>
	public partial class MigrateToDataStreamDescriptor
	{
	}
}

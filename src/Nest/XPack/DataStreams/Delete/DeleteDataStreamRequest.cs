// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Deletes a data stream
	/// </summary>
	[MapsApi("indices.delete_data_stream.json")]
	[ReadAs(typeof(DeleteDataStreamRequest))]
	public partial interface IDeleteDataStreamRequest
	{
	}

	/// <inheritdoc cref="IDeleteDataStreamRequest"/>
	public partial class DeleteDataStreamRequest
	{
	}

	/// <inheritdoc cref="IDeleteDataStreamRequest"/>
	public partial class DeleteDataStreamDescriptor
	{
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Gets a data stream
	/// </summary>
	[MapsApi("indices.get_data_stream.json")]
	[ReadAs(typeof(GetDataStreamRequest))]
	public partial interface IGetDataStreamRequest
	{
	}

	/// <inheritdoc cref="IGetDataStreamRequest"/>
	public partial class GetDataStreamRequest
	{
	}

	/// <inheritdoc cref="IGetDataStreamRequest"/>
	public partial class GetDataStreamDescriptor
	{
	}
}

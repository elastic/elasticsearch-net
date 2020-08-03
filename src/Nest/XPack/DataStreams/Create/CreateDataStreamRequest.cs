// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Creates a data stream
	/// </summary>
	[MapsApi("indices.create_data_stream.json")]
	[ReadAs(typeof(CreateDataStreamRequest))]
	public partial interface ICreateDataStreamRequest
	{
	}

	/// <inheritdoc cref="ICreateDataStreamRequest"/>
	public partial class CreateDataStreamRequest
	{
	}

	/// <inheritdoc cref="ICreateDataStreamRequest"/>
	public partial class CreateDataStreamDescriptor
	{
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// The purpose of the promote data stream api is to turn a data stream that is replicated by CCR into a regular data stream.
	/// </summary>
	[MapsApi("indices.promote_data_stream.json")]
	[ReadAs(typeof(PromoteDataStreamRequest))]
	public partial interface IPromoteDataStreamRequest
	{
	}

	/// <inheritdoc cref="IPromoteDataStreamRequest"/>
	public partial class PromoteDataStreamRequest : IPromoteDataStreamRequest
	{
	}

	/// <inheritdoc cref="IPromoteDataStreamRequest"/>
	public partial class PromoteDataStreamDescriptor
	{
	}
}

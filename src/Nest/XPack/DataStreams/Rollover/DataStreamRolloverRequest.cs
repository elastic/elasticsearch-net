using System;
using System.Collections.Generic;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Rolls over a data stream
	/// </summary>
	[MapsApi("indices.data_streams_rollover.json")]
	[ReadAs(typeof(DataStreamRolloverRequest))]
	public partial interface IDataStreamRolloverRequest
	{
	}

	/// <inheritdoc cref="IDataStreamRolloverRequest"/>
	public partial class DataStreamRolloverRequest
	{
	}

	/// <inheritdoc cref="IDataStreamRolloverRequest"/>
	public partial class DataStreamRolloverDescriptor
	{
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Retrieve configuration information for machine learning datafeeds.
	/// </summary>
	[MapsApi("ml.get_datafeeds.json")]
	public partial interface IGetDatafeedsRequest { }

	/// <inheritdoc />
	public partial class GetDatafeedsRequest { }

	/// <inheritdoc />
	public partial class GetDatafeedsDescriptor { }
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("ml.preview_datafeed.json")]
	[ResponseBuilderWithGeneric("PreviewDatafeedResponseBuilder<TDocument>.Instance")]
	public partial interface IPreviewDatafeedRequest { }

	/// <inheritdoc />
	public partial class PreviewDatafeedRequest { }

	/// <inheritdoc />
	public partial class PreviewDatafeedDescriptor { }
}

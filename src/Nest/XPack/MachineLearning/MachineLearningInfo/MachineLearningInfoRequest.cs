// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Returns defaults and limits used by machine learning.
	/// </summary>
	[MapsApi("ml.info")]
	public partial interface IMachineLearningInfoRequest { }

	/// <inheritdoc cref="IMachineLearningInfoRequest" />
	public partial class MachineLearningInfoRequest { }

	/// <inheritdoc cref="IMachineLearningInfoRequest" />
	public partial class MachineLearningInfoDescriptor { }
}

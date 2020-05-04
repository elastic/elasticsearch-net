// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Retrieve usage information for machine learning jobs.
	/// </summary>
	[MapsApi("ml.get_jobs.json")]
	public partial interface IGetJobsRequest { }

	/// <inheritdoc />
	public partial class GetJobsRequest { }

	/// <inheritdoc />
	public partial class GetJobsDescriptor { }
}

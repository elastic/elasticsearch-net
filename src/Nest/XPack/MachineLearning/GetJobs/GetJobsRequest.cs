using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A request to retrieve usage information for Machine Learning jobs.
	/// </summary>
	public partial interface IGetJobsRequest {}

	/// <inheritdoc />
	public partial class GetJobsRequest {}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetJobs")]
	public partial class GetJobsDescriptor {}
}

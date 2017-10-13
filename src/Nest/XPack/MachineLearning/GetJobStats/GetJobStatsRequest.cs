using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A request to retrieve usage information for Machine Learning jobs.
	/// </summary>
	public partial interface IGetJobStatsRequest {}

	/// <inheritdoc />
	public partial class GetJobStatsRequest {}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetJobStats")]
	public partial class GetJobStatsDescriptor {}
}

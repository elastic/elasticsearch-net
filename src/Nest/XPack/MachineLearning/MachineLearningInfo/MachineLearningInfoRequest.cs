using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieve job results for one or more categories.
	/// </summary>
	public partial interface IMachineLearningInfoRequest { }

	/// <inheritdoc />
	public partial class MachineLearningInfoRequest { }

	/// <inheritdoc />
	[DescriptorFor("XpackMlInfo")]
	public partial class MachineLearningInfoDescriptor { }
}

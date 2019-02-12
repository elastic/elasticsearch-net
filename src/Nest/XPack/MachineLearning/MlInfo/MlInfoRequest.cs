using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieve job results for one or more categories.
	/// </summary>
	public partial interface IMlInfoRequest { }

	/// <inheritdoc />
	public partial class MlInfoRequest { }

	/// <inheritdoc />
	[DescriptorFor("XpackMlInfo")]
	public partial class MlInfoDescriptor { }
}

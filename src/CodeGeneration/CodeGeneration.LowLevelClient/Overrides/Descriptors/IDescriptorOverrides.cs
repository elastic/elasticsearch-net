using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	/// <summary>
	/// Tweaks the generated descriptors
	/// </summary>
	public interface IDescriptorOverrides
	{
		/// <summary>
		/// Sometimes params can be defined on the body as well as on the querystring
		/// We favor specifying params on the body so here we can specify params we don't want on the querystring.
		/// </summary>
		IEnumerable<string> SkipQueryStringParams { get; }

		/// <summary>
		/// Override how the query param name is exposed to the client.
		/// </summary>
		IDictionary<string, string> RenameQueryStringParams { get; } 
	}
}
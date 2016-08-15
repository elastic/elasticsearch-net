using System.Collections.Generic;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
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

		/// <summary>
		/// Patch the CSharp method
		/// </summary>
		/// <param name="method"></param>
		/// <returns></returns>
		CsharpMethod PatchMethod(CsharpMethod method);
	}
}

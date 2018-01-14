using System.Collections;
using System.Collections.Generic;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	/// <summary>
	/// Tweaks the generated descriptors
	/// </summary>
	public interface IEndpointOverrides
	{
		/// <summary>
		/// Sometimes params can be defined on the body as well as on the querystring
		/// We favor specifying params on the body so here we can specify params we don't want on the querystring.
		/// </summary>
		IEnumerable<string> SkipQueryStringParams { get; }

		/// <summary>
		/// Force these be rendered as interface properties only, so that they'd have to be implemented manually
		/// and become part of the body. This only takes affect on requests that take a body (e.g not GET or HEAD).
		/// </summary>
		IEnumerable<string> RenderPartial { get; }

		/// <summary>
		/// Override how the query param name is exposed to the client.
		/// </summary>
		IDictionary<string, string> RenameQueryStringParams { get; }

		/// <summary>
		/// A map of key -> obsolete message for properties in the spec that should not be used any longer
		/// </summary>
		IDictionary<string, string>  ObsoleteQueryStringParams { get; set; }

		/// <summary>
		/// Patch the CSharp method
		/// </summary>
		/// <param name="method"></param>
		/// <returns></returns>
		CsharpMethod PatchMethod(CsharpMethod method);
	}
}

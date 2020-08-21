// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides
{
	/// <summary>
	/// Tweaks the generated descriptors
	/// </summary>
	public interface IEndpointOverrides
	{
		/// <summary>
		/// A map of key -> obsolete message for properties in the spec that should not be used any longer
		/// </summary>
		IDictionary<string, string> ObsoleteQueryStringParams { get; set; }

		/// <summary>
		/// Override how the query param name is exposed to the client.
		/// </summary>
		IDictionary<string, string> RenameQueryStringParams { get; }

		/// <summary>
		/// Force these be rendered as interface properties only, so that they'd have to be implemented manually
		/// and become part of the body. This only takes affect on requests that take a body (e.g not GET or HEAD).
		/// </summary>
		IEnumerable<string> RenderPartial { get; }

		/// <summary>
		/// Sometimes params can be defined on the body as well as on the querystring
		/// We favor specifying params on the body so here we can specify params we don't want on the querystring.
		/// </summary>
		IEnumerable<string> SkipQueryStringParams { get; }
	}
}

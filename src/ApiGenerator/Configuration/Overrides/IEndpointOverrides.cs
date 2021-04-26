/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

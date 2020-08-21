// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Configuration.Overrides
{
	public abstract class EndpointOverridesBase : IEndpointOverrides
	{
		public virtual IDictionary<string, string> ObsoleteQueryStringParams { get; set; } = new SortedDictionary<string, string>();

		public virtual IDictionary<string, string> RenameQueryStringParams { get; } = new SortedDictionary<string, string>();

		public virtual IEnumerable<string> RenderPartial { get; } = Enumerable.Empty<string>();

		public virtual IEnumerable<string> SkipQueryStringParams { get; } = Enumerable.Empty<string>();
	}
}

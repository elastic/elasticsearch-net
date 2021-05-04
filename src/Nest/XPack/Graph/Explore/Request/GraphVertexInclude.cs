// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	public class GraphVertexInclude
	{
		[DataMember(Name ="boost")]
		public double? Boost { get; set; }

		[DataMember(Name ="term")]
		public string Term { get; set; }
	}

	public class GraphVertexIncludeDescriptor : DescriptorPromiseBase<GraphVertexIncludeDescriptor, List<GraphVertexInclude>>
	{
		public GraphVertexIncludeDescriptor() : base(new List<GraphVertexInclude>()) { }

		public GraphVertexIncludeDescriptor Include(string term, double? boost = null) =>
			Assign(new GraphVertexInclude { Term = term, Boost = boost }, (a, v) => a.Add(v));

		public GraphVertexIncludeDescriptor IncludeRange(params string[] terms) =>
			Assign(terms.Select(t => new GraphVertexInclude { Term = t }), (a, v) => a.AddRange(v));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<string> terms) =>
			Assign(terms.Select(t => new GraphVertexInclude { Term = t }), (a, v) => a.AddRange(v));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<GraphVertexInclude> includes) =>
			Assign(includes, (a, v) => a.AddRange(v));
	}
}

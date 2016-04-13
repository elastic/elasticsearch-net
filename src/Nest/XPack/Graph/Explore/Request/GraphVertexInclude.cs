using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class GraphVertexInclude
	{
		[JsonProperty("term")]
		public string Term { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }
	}

	public class GraphVertexIncludeDescriptor : DescriptorPromiseBase<GraphVertexIncludeDescriptor, List<GraphVertexInclude>>
	{
		public GraphVertexIncludeDescriptor() : base(new List<GraphVertexInclude>()) { }

		public GraphVertexIncludeDescriptor Include(string term, double? boost = null) =>
			Assign(a => a.Add(new GraphVertexInclude { Term = term, Boost = boost }));

		public GraphVertexIncludeDescriptor IncludeRange(params string[] terms) =>
			Assign(a => a.AddRange(terms.Select(t=>new GraphVertexInclude { Term = t })));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<string> terms) =>
			Assign(a => a.AddRange(terms.Select(t=>new GraphVertexInclude { Term = t })));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<GraphVertexInclude> includes) =>
			Assign(a => a.AddRange(includes));

	}
}

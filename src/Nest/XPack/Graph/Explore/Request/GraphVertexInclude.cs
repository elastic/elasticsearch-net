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

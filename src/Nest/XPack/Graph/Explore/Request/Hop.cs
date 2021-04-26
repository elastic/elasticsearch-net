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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IHop
	{
		[DataMember(Name ="connections")]
		IHop Connections { get; set; }

		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		[DataMember(Name ="vertices")]
		IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
	}

	public class Hop : IHop
	{
		public IHop Connections { get; set; }
		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
	}

	public class HopDescriptor<T> : DescriptorBase<HopDescriptor<T>, IHop>, IHop
		where T : class
	{
		IHop IHop.Connections { get; set; }
		QueryContainer IHop.Query { get; set; }
		IEnumerable<IGraphVertexDefinition> IHop.Vertices { get; set; }

		public HopDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public HopDescriptor<T> Vertices(Func<GraphVerticesDescriptor<T>, IPromise<IList<IGraphVertexDefinition>>> selector) =>
			Assign(selector, (a, v) => a.Vertices = v?.Invoke(new GraphVerticesDescriptor<T>())?.Value);

		public HopDescriptor<T> Connections(Func<HopDescriptor<T>, IHop> selector) =>
			Assign(selector, (a, v) => a.Connections = v?.Invoke(new HopDescriptor<T>()));
	}
}

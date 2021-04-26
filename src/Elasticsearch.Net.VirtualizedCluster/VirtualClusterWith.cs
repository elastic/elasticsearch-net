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
using System.Linq;

namespace Elasticsearch.Net.VirtualizedCluster
{
	public static class VirtualClusterWith
	{
		public static VirtualCluster Nodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}")))
			);

		public static VirtualCluster MasterOnlyNodes(int numberOfNodes, int startFrom = 9200) =>
			new VirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes)
					.Select(n => new Node(new Uri($"http://localhost:{n}")) { HoldsData = false, MasterEligible = true })
			);


		public static VirtualCluster Nodes(IEnumerable<Node> nodes) =>
			new VirtualCluster(nodes);

	}
}

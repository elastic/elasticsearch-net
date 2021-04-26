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

using System.Runtime.Serialization;

namespace Nest
{
	public interface ICancelClusterRerouteCommand : IClusterRerouteCommand
	{
		[DataMember(Name ="allow_primary")]
		bool? AllowPrimary { get; set; }

		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		[DataMember(Name ="node")]
		string Node { get; set; }

		[DataMember(Name ="shard")]
		int? Shard { get; set; }
	}

	public class CancelClusterRerouteCommand : ICancelClusterRerouteCommand
	{
		public bool? AllowPrimary { get; set; }

		public IndexName Index { get; set; }
		public string Name => "cancel";

		public string Node { get; set; }

		public int? Shard { get; set; }
	}

	public class CancelClusterRerouteCommandDescriptor
		: DescriptorBase<CancelClusterRerouteCommandDescriptor, ICancelClusterRerouteCommand>, ICancelClusterRerouteCommand
	{
		bool? ICancelClusterRerouteCommand.AllowPrimary { get; set; }

		IndexName ICancelClusterRerouteCommand.Index { get; set; }
		string IClusterRerouteCommand.Name => "cancel";

		string ICancelClusterRerouteCommand.Node { get; set; }

		int? ICancelClusterRerouteCommand.Shard { get; set; }

		public CancelClusterRerouteCommandDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public CancelClusterRerouteCommandDescriptor Index<T>() where T : class => Assign(typeof(T), (a, v) => a.Index = v);

		public CancelClusterRerouteCommandDescriptor Shard(int? shard) => Assign(shard, (a, v) => a.Shard = v);

		public CancelClusterRerouteCommandDescriptor Node(string node) => Assign(node, (a, v) => a.Node = v);

		public CancelClusterRerouteCommandDescriptor AllowPrimary(bool? allowPrimary = true) => Assign(allowPrimary, (a, v) => a.AllowPrimary = v);
	}
}

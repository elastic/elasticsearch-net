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
	/// <summary>
	/// This action shrinks an existing index into a new index with fewer primary shards. It calls the Shrink API to shrink the
	/// index. Since allocating all the primary shards of the index to one node is a prerequisite, this action will first
	/// allocate the primary shards to a valid node. After shrinking, it will swap aliases pointing to the original index into
	/// the new shrunken index. The new index will also have a new name: "shrink-[origin-index-name]". So if the original index
	/// was
	/// called "logs", then the new index will be named "shrink-logs".
	/// </summary>
	public interface IShrinkLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// The number of shards to shrink to. must be a factor of the number of shards in the source index.
		/// </summary>
		[DataMember(Name = "number_of_shards")]
		int? NumberOfShards { get; set; }
	}

	public class ShrinkLifecycleAction : IShrinkLifecycleAction
	{
		/// <inheritdoc />
		public int? NumberOfShards { get; set; }
	}

	public class ShrinkLifecycleActionDescriptor : DescriptorBase<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction>, IShrinkLifecycleAction
	{
		/// <inheritdoc cref="IShrinkLifecycleAction.NumberOfShards" />
		int? IShrinkLifecycleAction.NumberOfShards { get; set; }

		/// <inheritdoc cref="IShrinkLifecycleAction.NumberOfShards" />
		public ShrinkLifecycleActionDescriptor NumberOfShards(int? numberOfShards) =>
			Assign(numberOfShards, (a, v) => a.NumberOfShards = numberOfShards);
	}
}

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

namespace Nest
{
	/// <summary>
	/// This action turns a ccr follower index into a regular index. This can be desired when moving follower indices into the
	/// next phase. Also certain actions like shrink and rollover can then be performed safely on follower indices.
	/// </summary>
	public interface IUnfollowLifecycleAction : ILifecycleAction { }

	public class UnfollowLifecycleAction : IUnfollowLifecycleAction { }

	public class UnfollowLifecycleActionDescriptor
		: DescriptorBase<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction>, IUnfollowLifecycleAction { }
}

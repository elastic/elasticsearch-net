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
	/// This action will freeze the index by calling the Freeze Index API.
	/// Note: Freezing an index will close the index and reopen it within the same API call.
	/// This causes primaries to not be allocated for a short amount of time and
	/// causes the cluster to go red until the primaries are allocated again.
	/// This limitation might be removed in the future.
	/// </summary>
	/// <remarks>
	/// Phases allowed: cold.
	/// </remarks>
	public interface IFreezeLifecycleAction : ILifecycleAction { }

	public class FreezeLifecycleAction : IFreezeLifecycleAction { }

	public class FreezeLifecycleActionDescriptor : DescriptorBase<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction>, IFreezeLifecycleAction { }
}

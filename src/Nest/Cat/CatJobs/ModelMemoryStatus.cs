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
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The status of the mathematical models
	/// </summary>
	[StringEnum]
	public enum ModelMemoryStatus
	{
		/// <summary>
		/// The models stayed below the configured value.
		/// </summary>
		[EnumMember(Value = "ok")]
		// ReSharper disable once InconsistentNaming
		OK,

		/// <summary>
		/// The models used more than 60% of the configured memory limit and older unused models will be pruned to free up space.
		/// </summary>
		[EnumMember(Value = "soft_limit")]
		SoftLimit,

		/// <summary>
		/// The models used more space than the configured memory limit. As a result, not all incoming data was processed.
		/// </summary>
		[EnumMember(Value = "hard_limit")]
		HardLimit
	}
}

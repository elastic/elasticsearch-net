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
using Nest.Utf8Json;

namespace Nest
{
	public class GetApiKeyResponse : ResponseBase
	{
		/// <inheritdoc />
		[DataMember(Name = "api_keys")]
		public IReadOnlyCollection<ApiKeys> ApiKeys { get; internal set; } = EmptyReadOnly<ApiKeys>.Collection;
	}

	public class ApiKeys
	{
		/// <summary>
		/// Creation time for the API key
		/// </summary>
		[DataMember(Name = "creation")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset Creation { get; set; }

		/// <summary>
		/// Optional expiration time for the API key in milliseconds
		/// </summary>
		[DataMember(Name = "expiration")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? Expiration { get; set; }

		/// <summary>
		/// Id for the API key
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Invalidation status for the API key. If the key has been invalidated, it has a value of true. Otherwise, it is false.
		/// </summary>
		[DataMember(Name = "invalidated")]
		public bool Invalidated { get; set; }

		/// <summary>
		/// Name of the API key
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Realm name of the principal for which this API key was created
		/// </summary>
		[DataMember(Name = "realm")]
		public string Realm { get; set; }

		/// <summary>
		/// Principal for which this API key was created
		/// </summary>
		[DataMember(Name = "username")]
		public string Username { get; set; }
	}
}

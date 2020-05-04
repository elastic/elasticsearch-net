// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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

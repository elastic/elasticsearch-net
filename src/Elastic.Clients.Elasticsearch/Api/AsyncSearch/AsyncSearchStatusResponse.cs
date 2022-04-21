// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public partial class AsyncSearchStatusResponse
{
	[JsonIgnore]
	public DateTimeOffset StartTime => StartTimeInMillis.DateTimeOffset;

	[JsonIgnore]
	public DateTimeOffset ExpirationTime => ExpirationTimeInMillis.DateTimeOffset;
}

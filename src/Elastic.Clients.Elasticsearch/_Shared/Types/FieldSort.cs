// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Mapping;

namespace Elastic.Clients.Elasticsearch;

public partial class FieldSort
{
	public static FieldSort Empty { get; } = new();
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal class CustomizedNamingPolicy : JsonNamingPolicy
{
	private readonly Func<string, string> _namingAction;

	public CustomizedNamingPolicy(Func<string, string> namingAction) => _namingAction = namingAction;

	public override string ConvertName(string name) => _namingAction(name);
}

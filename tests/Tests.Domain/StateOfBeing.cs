// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json.Serialization;

namespace Tests.Domain;

//the first applies when using internal source serializer the latter when using JsonNetSourceSerializer
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StateOfBeing
{
	BellyUp,
	Stable,
	VeryActive
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Serialization;

[AttributeUsage(AttributeTargets.Interface)]
internal class FieldNameQueryAttribute : Attribute
{
	public FieldNameQueryAttribute(Type convertType) => ConvertType = convertType;

	public Type ConvertType { get; }
}

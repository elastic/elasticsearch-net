// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Allows a property to be marked such that it is not mapped.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnoreForMappingAttribute : Attribute { }

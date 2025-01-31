// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Fluent;

/// <summary>
/// Used to mark descriptors which can be used to build the object they describe.
/// </summary>
/// <remarks>
/// This is most useful for descriptors of variants for internally-tagged unions where an IsADictionary is used
/// </remarks>
/// <typeparam name="T"></typeparam>
internal interface IBuildableDescriptor<T>
{
	T Build();
}

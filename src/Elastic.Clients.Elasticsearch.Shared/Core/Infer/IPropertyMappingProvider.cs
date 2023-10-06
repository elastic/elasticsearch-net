// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Reflection;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

/// <summary>
/// Provides mappings for CLR types.
/// </summary>
public interface IPropertyMappingProvider
{
	/// <summary>
	/// Creates an <see cref="PropertyMapping" /> for a <see cref="MemberInfo" />.
	/// </summary>
	PropertyMapping CreatePropertyMapping(MemberInfo memberInfo);
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif
//internal static class PropertyNameExtensions
//{
//	internal static bool IsConditionless(this PropertyName property) =>
//		property == null || property.Name.IsNullOrEmpty() && property.Expression == null &&
//		property.Property == null;
//}

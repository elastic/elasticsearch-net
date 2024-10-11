// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif
//internal static class FieldExtensions
//{
//	internal static bool IsConditionless(this Field field) =>
//		field == null || field.Name.IsNullOrEmpty() && field.Expression == null && field.Property == null;

//	internal static bool IsConditionless(this Fields field) =>
//		field?.ListOfFields == null || field.ListOfFields.All(l => l.IsConditionless());
//}

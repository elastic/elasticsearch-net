// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	internal static class FieldLookupExtensions
	{
		internal static bool IsConditionless(this IFieldLookup fieldLookup) =>
			fieldLookup == null ||
			fieldLookup.Id == null ||
			fieldLookup.Index == null ||
			fieldLookup.Path == null;
	}
}

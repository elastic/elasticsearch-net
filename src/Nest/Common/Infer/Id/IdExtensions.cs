// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	internal static class IdExtensions
	{
		internal static bool IsConditionless(this Id id) => id == null || id.StringOrLongValue == null && id.Document == null;
	}
}

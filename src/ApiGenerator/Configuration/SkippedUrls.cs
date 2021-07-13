// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration
{
	public static class GeneralSkipList
	{
		public static IReadOnlyCollection<string> SkippedUrls => new[] { "/_search/scroll/{scroll_id}" };
	}
}

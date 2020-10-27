// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest.Utf8Json
{
	internal static class ErrorCauseFormatterStatics
	{
		public static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "bytes_limit", 0 },
			{ "bytes_wanted", 1 },
			{ "caused_by", 2 },
			{ "col", 3 },
			{ "failed_shards", 4 },
			{ "grouped", 5 },
			{ "index", 6 },
			{ "index_uuid", 7 },
			{ "lang", 8 },
			{ "license.expired.feature", 9 },
			{ "line", 10 },
			{ "phase", 11 },
			{ "reason", 12 },
			{ "resource.id", 13 },
			{ "resource.type", 14 },
			{ "script", 15 },
			{ "script_stack", 16 },
			{ "shard", 17 },
			{ "stack_trace", 18 },
			{ "type", 19 }
		};

		public static readonly NullableStringIntFormatter ShardFormatter = new NullableStringIntFormatter();

		public static readonly InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<string> SingleOrEnumerableFormatter =
			new InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<string>();

		public static readonly ErrorCauseFormatter<ErrorCause> ErrorCausePropertyFormatter = new ErrorCauseFormatter<ErrorCause>();
	}
}

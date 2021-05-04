// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum TextQueryType
	{
		[EnumMember(Value = "best_fields")]
		BestFields,

		[EnumMember(Value = "most_fields")]
		MostFields,

		[EnumMember(Value = "cross_fields")]
		CrossFields,

		[EnumMember(Value = "phrase")]
		Phrase,

		[EnumMember(Value = "phrase_prefix")]
		PhrasePrefix,

		[EnumMember(Value = "bool_prefix")]
		BoolPrefix
	}
}

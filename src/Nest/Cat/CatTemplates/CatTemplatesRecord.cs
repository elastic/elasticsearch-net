// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatTemplatesRecord : ICatRecord
	{
		[DataMember(Name ="index_patterns")]
		public string IndexPatterns { get; set; }

		[DataMember(Name ="name")]
		public string Name { get; set; }

		[DataMember(Name ="order")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long Order { get; set; }

		[DataMember(Name ="version")]
		[JsonFormatter(typeof(NullableStringLongFormatter))]
		public long? Version { get; set; }
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(InlineGet<>))]
	public interface IInlineGet<out TDocument> where TDocument : class
	{
		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		[DataMember(Name = "found")]
		bool Found { get; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }
	}


	public class InlineGet<TDocument> : IInlineGet<TDocument>
		where TDocument : class
	{
		public FieldValues Fields { get; internal set; }

		public bool Found { get; internal set; }

		public TDocument Source { get; internal set; }
	}
}

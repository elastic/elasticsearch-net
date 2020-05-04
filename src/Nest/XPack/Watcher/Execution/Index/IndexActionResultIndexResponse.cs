// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IndexActionResultIndexResponse
	{
		[DataMember(Name ="created")]
		public bool? Created { get; set; }

		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="index")]
		public IndexName Index { get; set; }

		[DataMember(Name ="result")]
		public Result Result { get; set; }

		[DataMember(Name = "version")]
		public int Version { get; set; }
	}
}

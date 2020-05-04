// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatCountRecord : ICatRecord
	{
		[DataMember(Name ="count")]
		public string Count { get; set; }

		[DataMember(Name ="epoch")]
		public string Epoch { get; set; }

		[DataMember(Name ="timestamp")]
		public string Timestamp { get; set; }
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatPendingTasksRecord : ICatRecord
	{
		[DataMember(Name ="insertOrder")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? InsertOrder { get; set; }

		[DataMember(Name ="priority")]
		public string Priority { get; set; }

		[DataMember(Name ="source")]
		public string Source { get; set; }

		[DataMember(Name ="timeInQueue")]
		public string TimeInQueue { get; set; }
	}
}

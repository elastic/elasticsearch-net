// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	public class NestedIdentity
	{
		[DataMember(Name ="field")]
		public Field Field { get; internal set; }

		[DataMember(Name ="_nested")]
		public NestedIdentity Nested { get; internal set; }

		[DataMember(Name ="offset")]
		public int Offset { get; internal set; }
	}
}

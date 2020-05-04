// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum Result
	{
		Error,

		[EnumMember(Value = "created")]
		Created,

		[EnumMember(Value = "updated")]
		Updated,

		[EnumMember(Value = "deleted")]
		Deleted,

		[EnumMember(Value = "not_found")]
		NotFound,

		[EnumMember(Value = "noop")]
		Noop
	}
}

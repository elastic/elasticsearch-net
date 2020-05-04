// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum HttpInputMethod
	{
		[EnumMember(Value = "head")]
		Head,

		[EnumMember(Value = "get")]
		Get,

		[EnumMember(Value = "post")]
		Post,

		[EnumMember(Value = "put")]
		Put,

		[EnumMember(Value = "delete")]
		Delete
	}
}

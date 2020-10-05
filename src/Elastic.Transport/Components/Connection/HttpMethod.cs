// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming

namespace Elastic.Transport
{
	public enum HttpMethod
	{
		[EnumMember(Value = "GET")]
		GET,

		[EnumMember(Value = "POST")]
		POST,

		[EnumMember(Value = "PUT")]
		PUT,

		[EnumMember(Value = "DELETE")]
		DELETE,

		[EnumMember(Value = "HEAD")]
		HEAD
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum LicenseStatus
	{
		[EnumMember(Value = "active")]
		Active,

		[EnumMember(Value = "valid")]
		Valid,

		[EnumMember(Value = "invalid")]
		Invalid,

		[EnumMember(Value = "expired")]
		Expired
	}
}

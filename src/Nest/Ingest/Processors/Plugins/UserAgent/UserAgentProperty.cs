// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum UserAgentProperty
	{
		[EnumMember(Value = "NAME")] Name,
		[EnumMember(Value = "MAJOR")] Major,
		[EnumMember(Value = "MINOR")] Minor,
		[EnumMember(Value = "PATCH")] Patch,
		[EnumMember(Value = "OS")] Os,
		[EnumMember(Value = "OS_NAME")] OsName,
		[EnumMember(Value = "OS_MAJOR")] OsMajor,
		[EnumMember(Value = "OS_MINOR")] OsMinor,
		[EnumMember(Value = "DEVICE")] Device,
		[EnumMember(Value = "BUILD")] Build
	}
}

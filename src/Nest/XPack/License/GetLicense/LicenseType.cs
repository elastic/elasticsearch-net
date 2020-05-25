// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum LicenseType
	{
		[EnumMember(Value = "missing")]
		Missing,

		[EnumMember(Value = "trial")]
		Trial,

		[EnumMember(Value = "basic")]
		Basic,

		[EnumMember(Value = "standard")]
		Standard,

		[EnumMember(Value = "dev")] //bwc
		Dev,

		[EnumMember(Value = "silver")] //bwc
		Silver,

		[EnumMember(Value = "gold")]
		Gold,

		[EnumMember(Value = "platinum")]
		Platinum,

		[EnumMember(Value = "enterprise")]
		Enterprise,
	}
}

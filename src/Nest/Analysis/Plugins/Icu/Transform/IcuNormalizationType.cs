// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Forward (default) for LTR and reverse for RTL
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuTransformDirection
	{
		/// <summary>LTR</summary>
		[EnumMember(Value = "forward")]
		Forward,

		/// <summary> RTL</summary>
		[EnumMember(Value = "reverse")]
		Reverse,
	}
}

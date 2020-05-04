// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Controls which case is sorted first when case is not ignored for
	/// strength tertiary. The default depends on the collation.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[StringEnum]
	public enum IcuCollationCaseFirst
	{
		[EnumMember(Value = "lower")] Lower,
		[EnumMember(Value = "upper")] Upper
	}
}

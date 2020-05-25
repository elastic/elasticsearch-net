// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// Sorts highlighted fragments
	/// </summary>
	[StringEnum]
	public enum HighlighterOrder
	{
		/// <summary>
		/// Sorts highlighted fragments by score. Only valid for the <see cref="HighlighterType.Unified" /> highlighter
		/// </summary>
		[EnumMember(Value = "score")]
		Score
	}
}

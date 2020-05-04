// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// Indicates if the highlighted text should be HTML encoded
	/// </summary>
	[StringEnum]
	public enum HighlighterEncoder
	{
		/// <summary>
		/// No encoding
		/// </summary>
		[EnumMember(Value = "default")]
		Default,

		/// <summary>
		/// Escapes HTML highlighting tags
		/// </summary>
		[EnumMember(Value = "html")]
		Html
	}
}

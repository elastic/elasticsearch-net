// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Snowball compatible languages
	/// </summary>
	[StringEnum]
	public enum SnowballLanguage
	{
		[EnumMember(Value = "Armenian")]
		Armenian,

		[EnumMember(Value = "Basque")]
		Basque,

		[EnumMember(Value = "Catalan")]
		Catalan,

		[EnumMember(Value = "Danish")]
		Danish,

		[EnumMember(Value = "Dutch")]
		Dutch,

		[EnumMember(Value = "English")]
		English,

		[EnumMember(Value = "Finnish")]
		Finnish,

		[EnumMember(Value = "French")]
		French,

		[EnumMember(Value = "German")]
		German,

		[EnumMember(Value = "German2")]
		German2,

		[EnumMember(Value = "Hungarian")]
		Hungarian,

		[EnumMember(Value = "Italian")]
		Italian,

		[EnumMember(Value = "Kp")]
		Kp,

		[EnumMember(Value = "Lovins")]
		Lovins,

		[EnumMember(Value = "Norwegian")]
		Norwegian,

		[EnumMember(Value = "Porter")]
		Porter,

		[EnumMember(Value = "Portuguese")]
		Portuguese,

		[EnumMember(Value = "Romanian")]
		Romanian,

		[EnumMember(Value = "Russian")]
		Russian,

		[EnumMember(Value = "Spanish")]
		Spanish,

		[EnumMember(Value = "Swedish")]
		Swedish,

		[EnumMember(Value = "Turkish")]
		Turkish
	}
}

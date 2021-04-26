/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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

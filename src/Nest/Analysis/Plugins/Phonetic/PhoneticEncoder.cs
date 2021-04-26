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
	[StringEnum]
	public enum PhoneticEncoder
	{
		[EnumMember(Value = "metaphone")]
		Metaphone,

		[EnumMember(Value = "double_metaphone")]
		DoubleMetaphone,

		[EnumMember(Value = "soundex")]
		Soundex,

		[EnumMember(Value = "refined_soundex")]
		RefinedSoundex,

		[EnumMember(Value = "caverphone1")]
		Caverphone1,

		[EnumMember(Value = "caverphone2")]
		Caverphone2,

		[EnumMember(Value = "cologne")]
		Cologne,

		[EnumMember(Value = "nysiis")]
		Nysiis,

		[EnumMember(Value = "koelnerphonetik")]
		KoelnerPhonetik,

		[EnumMember(Value = "haasephonetik")]
		HaasePhonetik,

		[EnumMember(Value = "beider_morse")]
		Beidermorse,

		[EnumMember(Value = "daitch_mokotoff")]
		DaitchMokotoff
	}
}

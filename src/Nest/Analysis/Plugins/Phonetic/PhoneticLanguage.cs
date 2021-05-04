// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum PhoneticLanguage
	{
		[EnumMember(Value = "any")]
		Any,

		[EnumMember(Value = "comomon")]
		Comomon,

		[EnumMember(Value = "cyrillic")]
		Cyrillic,

		[EnumMember(Value = "english")]
		English,

		[EnumMember(Value = "french")]
		French,

		[EnumMember(Value = "german")]
		German,

		[EnumMember(Value = "hebrew")]
		Hebrew,

		[EnumMember(Value = "hungarian")]
		Hungarian,

		[EnumMember(Value = "polish")]
		Polish,

		[EnumMember(Value = "romanian")]
		Romanian,

		[EnumMember(Value = "russian")]
		Russian,

		[EnumMember(Value = "spanish")]
		Spanish,
	}
}

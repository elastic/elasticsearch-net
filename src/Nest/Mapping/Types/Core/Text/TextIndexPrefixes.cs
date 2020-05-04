// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TextIndexPrefixes))]
	public interface ITextIndexPrefixes
	{
		[DataMember(Name ="max_chars")]
		int? MaxCharacters { get; set; }

		[DataMember(Name ="min_chars")]
		int? MinCharacters { get; set; }
	}

	public class TextIndexPrefixes : ITextIndexPrefixes
	{
		public int? MaxCharacters { get; set; }
		public int? MinCharacters { get; set; }
	}

	public class TextIndexPrefixesDescriptor
		: DescriptorBase<TextIndexPrefixesDescriptor, ITextIndexPrefixes>, ITextIndexPrefixes
	{
		int? ITextIndexPrefixes.MaxCharacters { get; set; }
		int? ITextIndexPrefixes.MinCharacters { get; set; }

		public TextIndexPrefixesDescriptor MinCharacters(int? min) => Assign(min, (a, v) => a.MinCharacters = v);

		public TextIndexPrefixesDescriptor MaxCharacters(int? max) => Assign(max, (a, v) => a.MaxCharacters = v);
	}
}

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
using Nest.Utf8Json;

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

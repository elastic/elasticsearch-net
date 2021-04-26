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

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(InputContainer))]
	public interface IInputContainer
	{
		[DataMember(Name ="chain")]
		IChainInput Chain { get; set; }

		[DataMember(Name ="http")]
		IHttpInput Http { get; set; }

		[DataMember(Name ="search")]
		ISearchInput Search { get; set; }

		[DataMember(Name ="simple")]
		ISimpleInput Simple { get; set; }
	}

	[DataContract]
	public class InputContainer : IInputContainer, IDescriptor
	{
		internal InputContainer() { }

		public InputContainer(InputBase input)
		{
			input.ThrowIfNull(nameof(input));
			input.WrapInContainer(this);
		}

		IChainInput IInputContainer.Chain { get; set; }
		IHttpInput IInputContainer.Http { get; set; }
		ISearchInput IInputContainer.Search { get; set; }
		ISimpleInput IInputContainer.Simple { get; set; }

		public static implicit operator InputContainer(InputBase input) => input == null
			? null
			: new InputContainer(input);
	}

	public class InputDescriptor : InputContainer
	{
		private InputDescriptor Assign<TValue>(TValue value, Action<IInputContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public InputDescriptor Search(Func<SearchInputDescriptor, ISearchInput> selector) =>
			Assign(selector, (a, v) => a.Search = v.Invoke(new SearchInputDescriptor()));

		public InputDescriptor Http(Func<HttpInputDescriptor, IHttpInput> selector) =>
			Assign(selector, (a, v) => a.Http = v.Invoke(new HttpInputDescriptor()));

		public InputDescriptor Simple(Func<SimpleInputDescriptor, ISimpleInput> selector) =>
			Assign(selector,(a, v) => a.Simple = v.Invoke(new SimpleInputDescriptor()));

		public InputDescriptor Chain(Func<ChainInputDescriptor, IChainInput> selector) =>
			Assign(selector, (a, v) => a.Chain = v.Invoke(new ChainInputDescriptor()));
	}
}

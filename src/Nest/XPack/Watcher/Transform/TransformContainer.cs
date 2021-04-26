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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TransformContainer))]
	public interface ITransformContainer
	{
		[DataMember(Name ="chain")]
		IChainTransform Chain { get; set; }

		[DataMember(Name ="script")]
		IScriptTransform Script { get; set; }

		[DataMember(Name ="search")]
		ISearchTransform Search { get; set; }
	}

	[DataContract]
	public class TransformContainer : ITransformContainer, IDescriptor
	{
		internal TransformContainer() { }

		public TransformContainer(TransformBase transform)
		{
			transform.ThrowIfNull(nameof(transform));
			transform.WrapInContainer(this);
		}

		IChainTransform ITransformContainer.Chain { get; set; }
		IScriptTransform ITransformContainer.Script { get; set; }
		ISearchTransform ITransformContainer.Search { get; set; }
	}

	public class TransformDescriptor : TransformContainer
	{
		private TransformDescriptor Assign<TValue>(TValue value, Action<ITransformContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector) =>
			Assign(selector,(a, v) => a.Search = v?.InvokeOrDefault(new SearchTransformDescriptor()));

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector) =>
			Assign(selector,(a, v) => a.Script = v?.Invoke(new ScriptTransformDescriptor()));

		public TransformDescriptor Chain(Func<ChainTransformDescriptor, IChainTransform> selector) =>
			Assign(selector,(a, v) => a.Chain = v?.Invoke(new ChainTransformDescriptor()));
	}
}

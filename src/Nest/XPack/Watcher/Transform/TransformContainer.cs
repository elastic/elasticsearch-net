// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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

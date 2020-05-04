// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ITransform { }

	public abstract class TransformBase
	{
		public static implicit operator TransformContainer(TransformBase transform) => transform == null
			? null
			: new TransformContainer(transform);

		internal abstract void WrapInContainer(ITransformContainer container);
	}
}

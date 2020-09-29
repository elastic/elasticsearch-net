// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISmoothingModel { }

	public abstract class SmoothingModelBase
	{
		public static implicit operator SmoothingModelContainer(SmoothingModelBase model) => model == null
			? null
			: new SmoothingModelContainer(model);

		internal abstract void WrapInContainer(ISmoothingModelContainer container);
	}
}

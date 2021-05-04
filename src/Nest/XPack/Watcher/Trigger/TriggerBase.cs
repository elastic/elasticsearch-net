// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ITrigger { }

	public abstract class TriggerBase : ITrigger
	{
		public static implicit operator TriggerContainer(TriggerBase trigger) => trigger == null
			? null
			: new TriggerContainer(trigger);

		internal abstract void WrapInContainer(ITriggerContainer container);
	}
}

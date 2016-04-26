using System;
using System.Collections.Generic;

namespace Nest
{
	public class ProcessorsDescriptor : DescriptorPromiseBase<ProcessorsDescriptor, IList<IProcessor>>
	{
		public ProcessorsDescriptor() : base(new List<IProcessor>()) { }

		public ProcessorsDescriptor Append<T>(Func<AppendProcessorDescriptor<T>, IAppendProcessor> selector) where T : class  =>
			Assign(a => a.AddIfNotNull(selector?.Invoke(new AppendProcessorDescriptor<T>())));
	}
}

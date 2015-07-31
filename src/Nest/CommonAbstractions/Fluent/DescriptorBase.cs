using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class DescriptorBase<TDescriptor, TInterface>
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class
	{
		public TDescriptor Assign(Action<TInterface> assigner) => Fluent.Assign((TDescriptor)this, assigner);
	}
}

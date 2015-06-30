using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class QueryDescriptorBase<TDescriptor, TInterface>
		where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IQuery
	{
		public TDescriptor Name(string name) => Assign(a => a.Name = name);

		public TDescriptor Assign(Action<TInterface> assigner) => Fluent.Assign((TDescriptor)this, assigner);
	}
}

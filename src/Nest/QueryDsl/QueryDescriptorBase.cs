using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class QueryDescriptorBase<TDescriptor, TInterface> : IQuery
		where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IQuery
	{
		string IQuery.Name { get; set; }
		double? IQuery.Boost { get; set; }
		bool IQuery.Conditionless { get; }

		public TDescriptor Name(string name) => Assign(a => a.Name = name);
		public TDescriptor Boost(double boost) => Assign(a => a.Boost = boost);

		public TDescriptor Assign(Action<TInterface> assigner) => Fluent.Assign((TDescriptor)this, assigner);
	}
}

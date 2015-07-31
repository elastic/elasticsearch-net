using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class QueryDescriptorBase<TDescriptor, TInterface> 
		: DescriptorBase<TDescriptor, TInterface>, IQuery
		where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IQuery
	{
		string IQuery.Name { get; set; }
		public TDescriptor Name(string name) => Assign(a => a.Name = name);

		double? IQuery.Boost { get; set; }
		public TDescriptor Boost(double boost) => Assign(a => a.Boost = boost);

		bool IQuery.Conditionless { get; }
	}
}

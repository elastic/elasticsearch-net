using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IPercolatorProperty : IProperty
	{
	}

	public class PercolatorProperty : PropertyBase, IPercolatorProperty
	{
		public PercolatorProperty() : base("percolator") { }
	}

	public class PercolatorPropertyDescriptor<T>
	: PropertyDescriptorBase<PercolatorPropertyDescriptor<T>, IPercolatorProperty, T>, IPercolatorProperty
	where T : class
	{
		public PercolatorPropertyDescriptor() : base("percolator") { }
	}
}

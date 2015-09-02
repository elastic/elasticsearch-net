using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public interface IMurmur3HashProperty : IProperty
	{
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class Murmur3HashProperty : Property, IMurmur3HashProperty
	{
		public Murmur3HashProperty() : base("murmur3") { }
	}

	public class Murmur3HashPropertyDescriptor<T> 
		: PropertyDescriptorBase<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty, T>, IMurmur3HashProperty
		where T : class
	{
		public Murmur3HashPropertyDescriptor() : base("murmur3") { }
	}
}

using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyNameAttribute : Attribute
	{
		public string Name { get; set; }
		public PropertyNameAttribute(string name) => Name = name;
	}
}

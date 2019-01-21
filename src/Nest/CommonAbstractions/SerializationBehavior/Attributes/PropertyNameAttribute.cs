using System;
using Utf8Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyNameAttribute : Attribute, IJsonProperty
	{
		public PropertyNameAttribute(string name) => Name = name;

		public string Name { get; set; }
		public int Order { get; } = -1;
		public bool Ignore { get; set; }
	}
}

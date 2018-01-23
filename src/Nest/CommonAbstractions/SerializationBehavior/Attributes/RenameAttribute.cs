using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	public class RenameAttribute : Attribute
	{
		public string Name { get; set; }
		public RenameAttribute(string name) => Name = name;
	}
}
using System;
using Elasticsearch.Net;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyNameAttribute : Attribute, IJsonProperty
	{
		public PropertyNameAttribute(string name) => Name = name;

		public string Name { get; set; }
		public int Order { get; } = -1;
		public bool Ignore { get; set; }
		public bool? AllowPrivate { get; set; } = true;
	}
}

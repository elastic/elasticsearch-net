using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchPropertyAttribute
	{
		public DynamicMapping Dynamic { get; set; }
		public bool Enabled { get; set; }
		public bool IncludeInAll { get; set; }
		public string Path { get; set; }

		public override IProperty ToProperty() => new ObjectProperty(this);
	}	
}

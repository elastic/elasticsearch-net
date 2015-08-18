using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchPropertyAttribute
	{
		public DynamicMappingOption Dynamic { get; set; }
		public bool Enabled { get; set; }
		public bool IncludeInAll { get; set; }
		public string Path { get; set; }

		public override IElasticsearchProperty ToProperty() => new ObjectProperty(this);
	}	
}

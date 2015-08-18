using System;

namespace Nest
{
	public class BinaryAttribute : ElasticsearchPropertyAttribute
	{
		public override IElasticsearchProperty ToProperty() => new BinaryProperty(this);
	}	
}

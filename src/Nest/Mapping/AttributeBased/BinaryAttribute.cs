using System;

namespace Nest
{
	public class BinaryAttribute : ElasticPropertyAttribute
	{
		public override IElasticsearchProperty ToProperty() => new BinaryProperty(this);
	}	
}

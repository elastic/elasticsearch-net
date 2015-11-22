using System;

namespace Nest
{
	public class BinaryAttribute : ElasticsearchPropertyAttribute
	{
		public override IProperty ToProperty() => new BinaryProperty(this);
	}	
}

using System;

namespace Nest
{
	public class AttachmentAttribute : ElasticPropertyAttribute
	{
		public override IElasticsearchProperty ToProperty() => new AttachmentProperty(this);
	}
}

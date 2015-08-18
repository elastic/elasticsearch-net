using System;

namespace Nest
{
	public class AttachmentAttribute : ElasticsearchPropertyAttribute
	{
		public override IElasticsearchProperty ToProperty() => new AttachmentProperty(this);
	}
}

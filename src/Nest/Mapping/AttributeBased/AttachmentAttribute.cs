using System;

namespace Nest
{
	public class AttachmentAttribute : ElasticsearchPropertyAttribute
	{
		public override IProperty ToProperty() => new AttachmentProperty(this);
	}
}

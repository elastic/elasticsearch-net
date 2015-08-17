using System;

namespace Nest
{
	public class AttachmentAttribute : ElasticPropertyAttribute
	{
		public override IElasticType ToElasticType() => new AttachmentType(this);
	}
}

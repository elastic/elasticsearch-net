using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }
	}

	public abstract class FieldNameQuery : PlainQuery, IFieldNameQuery
	{
		public virtual bool Conditionless => false;
		public PropertyPathMarker Field { get; set; }
	}
}

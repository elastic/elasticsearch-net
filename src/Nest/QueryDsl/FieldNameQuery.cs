using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class FieldNameQuery : PlainQuery, IFieldNameQuery
	{
		public virtual bool Conditionless => false;
		public string Name { get; set; }
		public PropertyPathMarker Field { get; set; }
	}
}

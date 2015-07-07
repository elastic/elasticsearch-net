using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPath Field { get; set; }
	}

	public abstract class FieldNameQuery : QueryBase, IFieldNameQuery
	{
		public virtual bool Conditionless => false;
		public PropertyPath Field { get; set; }
	}
}

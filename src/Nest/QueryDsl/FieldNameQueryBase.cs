using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		FieldName Field { get; set; }
	}

	public abstract class FieldNameQueryBase : QueryBase, IFieldNameQuery
	{
		public virtual bool Conditionless => false;
		public FieldName Field { get; set; }
	}
}

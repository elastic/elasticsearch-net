using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		Field Field { get; set; }
	}

	public abstract class FieldNameQueryBase : QueryBase, IFieldNameQuery
	{
		public virtual bool Conditionless => false;
		public Field Field { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ElasticSearch.DSL
{
	public interface IQuery
	{
		
	}
	public interface IQuery<T> : IQuery
	{
		Expression<Func<T, object>> Expression { get; set; }
	}
}

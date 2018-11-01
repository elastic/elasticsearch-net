using System;
using System.Linq.Expressions;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class ExpressionCreationRunner : RunBase
	{
		protected override RoutineBase Routine() => Loop(() => Exp<Project>(p => p.LeadDeveloper.FirstName), (c, f) => { });

		private static Expression<Func<T, object>> Exp<T>(Expression<Func<T, object>> exp) => exp;
	}
}

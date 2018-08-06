using System;
using System.Linq.Expressions;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class ExpressionCreationRunner : RunBase
	{
		private static Expression<Func<T, object>> Exp<T>(Expression<Func<T, object>> exp) => exp;

		protected override RoutineBase Routine() => this.Loop(() => Exp<Project>(p => p.LeadDeveloper.FirstName), (c, f) => { });
	}
}

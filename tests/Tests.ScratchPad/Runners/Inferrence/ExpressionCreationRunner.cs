// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using Tests.Domain;

namespace Tests.ScratchPad.Runners.Inferrence
{
	public class ExpressionCreationRunner : RunBase
	{
		private static Expression<Func<T, object>> Exp<T>(Expression<Func<T, object>> exp) => exp;

		protected override RoutineBase Routine() => Loop(() => Exp<Project>(p => p.LeadDeveloper.FirstName), (c, f) => { });
	}
}

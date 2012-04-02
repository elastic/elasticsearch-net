using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Nest.Resolvers
{
	/// <summary>
	/// This comes from Matt Warren's sample:
	/// http://blogs.msdn.com/mattwar/archive/2007/07/31/linq-building-an-iqueryable-provider-part-ii.aspx
	/// </summary>
	public abstract class ExpressionVisitor
	{
		public virtual Expression Visit(Expression exp, Stack<string>  stack)
		{
			if (exp == null)
				return exp;

			switch (exp.NodeType)
			{
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
				case ExpressionType.Not:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.ArrayLength:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
					return this.VisitUnary((UnaryExpression)exp, stack);
				case ExpressionType.Add:
				case ExpressionType.AddChecked:
				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
				case ExpressionType.Divide:
				case ExpressionType.Modulo:
				case ExpressionType.And:
				case ExpressionType.AndAlso:
				case ExpressionType.Or:
				case ExpressionType.OrElse:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.Coalesce:
				case ExpressionType.ArrayIndex:
				case ExpressionType.RightShift:
				case ExpressionType.LeftShift:
				case ExpressionType.ExclusiveOr:
					return this.VisitBinary((BinaryExpression)exp, stack);
				case ExpressionType.TypeIs:
					return this.VisitTypeIs((TypeBinaryExpression)exp, stack);
				case ExpressionType.Conditional:
					return this.VisitConditional((ConditionalExpression)exp, stack);
				case ExpressionType.Constant:
					return this.VisitConstant((ConstantExpression)exp, stack);
				case ExpressionType.Parameter:
					return this.VisitParameter((ParameterExpression)exp, stack);
				case ExpressionType.MemberAccess:
					return this.VisitMemberAccess((MemberExpression)exp, stack);
				case ExpressionType.Call:
					return this.VisitMethodCall((MethodCallExpression)exp, stack);
				case ExpressionType.Lambda:
					return this.VisitLambda((LambdaExpression)exp, stack);
				case ExpressionType.New:
					return this.VisitNew((NewExpression)exp, stack);
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					return this.VisitNewArray((NewArrayExpression)exp, stack);
				case ExpressionType.Invoke:
					return this.VisitInvocation((InvocationExpression)exp, stack);
				case ExpressionType.MemberInit:
					return this.VisitMemberInit((MemberInitExpression)exp, stack);
				case ExpressionType.ListInit:
					return this.VisitListInit((ListInitExpression)exp, stack);
				default:
					throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
			}
		}

    protected virtual MemberBinding VisitBinding(MemberBinding binding, Stack<string> stack)
		{
			switch (binding.BindingType)
			{
				case MemberBindingType.Assignment:
					return this.VisitMemberAssignment((MemberAssignment)binding, stack);
				case MemberBindingType.MemberBinding:
          return this.VisitMemberMemberBinding((MemberMemberBinding)binding, stack);
				case MemberBindingType.ListBinding:
          return this.VisitMemberListBinding((MemberListBinding)binding, stack);
				default:
					throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

    protected virtual ElementInit VisitElementInitializer(ElementInit initializer, Stack<string> stack)
		{
      ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments, stack);
			if (arguments != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, arguments);
			}
			return initializer;
		}

    protected virtual Expression VisitUnary(UnaryExpression u, Stack<string> stack)
		{
      Expression operand = this.Visit(u.Operand, stack);
			if (operand != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
			}
			return u;
		}

    protected virtual Expression VisitBinary(BinaryExpression b, Stack<string> stack)
		{
      Expression left = this.Visit(b.Left, stack);
      Expression right = this.Visit(b.Right, stack);
      Expression conversion = this.Visit(b.Conversion, stack);
			if (left != b.Left || right != b.Right || conversion != b.Conversion)
			{
				if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
					return Expression.Coalesce(left, right, conversion as LambdaExpression);
				else
					return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
			}
			return b;
		}

    protected virtual Expression VisitTypeIs(TypeBinaryExpression b, Stack<string> stack)
		{
      Expression expr = this.Visit(b.Expression, stack);
			if (expr != b.Expression)
			{
				return Expression.TypeIs(expr, b.TypeOperand);
			}
			return b;
		}

    protected virtual Expression VisitConstant(ConstantExpression c, Stack<string> stack)
		{
			return c;
		}

    protected virtual Expression VisitConditional(ConditionalExpression c, Stack<string> stack)
		{
      Expression test = this.Visit(c.Test, stack);
      Expression ifTrue = this.Visit(c.IfTrue, stack);
      Expression ifFalse = this.Visit(c.IfFalse, stack);
			if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
			{
				return Expression.Condition(test, ifTrue, ifFalse);
			}
			return c;
		}

    protected virtual Expression VisitParameter(ParameterExpression p, Stack<string> stack)
		{
			return p;
		}

    protected virtual Expression VisitMemberAccess(MemberExpression m, Stack<string> stack)
		{
      Expression exp = this.Visit(m.Expression, stack);
			if (exp != m.Expression)
			{
				return Expression.MakeMemberAccess(exp, m.Member);
			}
			return m;
		}

    protected virtual Expression VisitMethodCall(MethodCallExpression m, Stack<string> stack)
		{
      Expression obj = this.Visit(m.Object, stack);
      IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments, stack);
			if (obj != m.Object || args != m.Arguments)
			{
				return Expression.Call(obj, m.Method, args);
			}
			return m;
		}

		protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original, Stack<string>  stack)
		{
			List<Expression> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
				Expression p = this.Visit(original[i], stack);
				if (list != null)
				{
					list.Add(p);
				}
				else if (p != original[i])
				{
					list = new List<Expression>(n);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(p);
				}
			}
			if (list != null)
			{
				return list.AsReadOnly();
			}
			return original;
		}

		protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment, Stack<string>  stack)
		{
      Expression e = this.Visit(assignment.Expression, stack);
			if (e != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, e);
			}
			return assignment;
		}

		protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding, Stack<string>  stack)
		{
      IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings, stack);
			if (bindings != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, bindings);
			}
			return binding;
		}

		protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding, Stack<string>  stack)
		{
      IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers, stack);
			if (initializers != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, initializers);
			}
			return binding;
		}

		protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original, Stack<string>  stack)
		{
			List<MemberBinding> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
        MemberBinding b = this.VisitBinding(original[i], stack);
				if (list != null)
				{
					list.Add(b);
				}
				else if (b != original[i])
				{
					list = new List<MemberBinding>(n);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(b);
				}
			}
			if (list != null)
				return list;
			return original;
		}

		protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original, Stack<string>  stack)
		{
			List<ElementInit> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
        ElementInit init = this.VisitElementInitializer(original[i], stack);
				if (list != null)
				{
					list.Add(init);
				}
				else if (init != original[i])
				{
					list = new List<ElementInit>(n);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(init);
				}
			}
			if (list != null)
				return list;
			return original;
		}

		protected virtual Expression VisitLambda(LambdaExpression lambda, Stack<string>  stack)
		{
      Expression body = this.Visit(lambda.Body, stack);
			if (body != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, body, lambda.Parameters);
			}
			return lambda;
		}

		protected virtual NewExpression VisitNew(NewExpression nex, Stack<string>  stack)
		{
      IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments, stack);
			if (args != nex.Arguments)
			{
				if (nex.Members != null)
					return Expression.New(nex.Constructor, args, nex.Members);
				else
					return Expression.New(nex.Constructor, args);
			}
			return nex;
		}

		protected virtual Expression VisitMemberInit(MemberInitExpression init, Stack<string>  stack)
		{
      NewExpression n = this.VisitNew(init.NewExpression, stack);
      IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings, stack);
			if (n != init.NewExpression || bindings != init.Bindings)
			{
				return Expression.MemberInit(n, bindings);
			}
			return init;
		}

		protected virtual Expression VisitListInit(ListInitExpression init, Stack<string>  stack)
		{
      NewExpression n = this.VisitNew(init.NewExpression, stack);
      IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers, stack);
			if (n != init.NewExpression || initializers != init.Initializers)
			{
				return Expression.ListInit(n, initializers);
			}
			return init;
		}

		protected virtual Expression VisitNewArray(NewArrayExpression na, Stack<string>  stack)
		{
      IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions, stack);
			if (exprs != na.Expressions)
			{
				if (na.NodeType == ExpressionType.NewArrayInit)
				{
					return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
				}
				else
				{
					return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
				}
			}
			return na;
		}

		protected virtual Expression VisitInvocation(InvocationExpression iv, Stack<string>  stack)
		{
      IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments, stack);
			Expression expr = this.Visit(iv.Expression, stack);
			if (args != iv.Arguments || expr != iv.Expression)
			{
				return Expression.Invoke(expr, args);
			}
			return iv;
		}
	}


	

}

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
		public virtual Expression Visit(Expression exp, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
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
					return this.VisitUnary((UnaryExpression)exp, stack, properties);
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
					return this.VisitBinary((BinaryExpression)exp, stack, properties);
				case ExpressionType.TypeIs:
					return this.VisitTypeIs((TypeBinaryExpression)exp, stack, properties);
				case ExpressionType.Conditional:
					return this.VisitConditional((ConditionalExpression)exp, stack, properties);
				case ExpressionType.Constant:
					return this.VisitConstant((ConstantExpression)exp, stack, properties);
				case ExpressionType.Parameter:
					return this.VisitParameter((ParameterExpression)exp, stack, properties);
				case ExpressionType.MemberAccess:
					return this.VisitMemberAccess((MemberExpression)exp, stack, properties);
				case ExpressionType.Call:
					return this.VisitMethodCall((MethodCallExpression)exp, stack, properties);
				case ExpressionType.Lambda:
					return this.VisitLambda((LambdaExpression)exp, stack, properties);
				case ExpressionType.New:
					return this.VisitNew((NewExpression)exp, stack, properties);
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					return this.VisitNewArray((NewArrayExpression)exp, stack, properties);
				case ExpressionType.Invoke:
					return this.VisitInvocation((InvocationExpression)exp, stack, properties);
				case ExpressionType.MemberInit:
					return this.VisitMemberInit((MemberInitExpression)exp, stack, properties);
				case ExpressionType.ListInit:
					return this.VisitListInit((ListInitExpression)exp, stack, properties);
				default:
					throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
			}
		}

    protected virtual MemberBinding VisitBinding(MemberBinding binding, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			switch (binding.BindingType)
			{
				case MemberBindingType.Assignment:
					return this.VisitMemberAssignment((MemberAssignment)binding, stack, properties);
				case MemberBindingType.MemberBinding:
					return this.VisitMemberMemberBinding((MemberMemberBinding)binding, stack, properties);
				case MemberBindingType.ListBinding:
					return this.VisitMemberListBinding((MemberListBinding)binding, stack, properties);
				default:
					throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

    protected virtual ElementInit VisitElementInitializer(ElementInit initializer, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments, stack, properties);
			if (arguments != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, arguments);
			}
			return initializer;
		}

    protected virtual Expression VisitUnary(UnaryExpression u, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression operand = this.Visit(u.Operand, stack, properties);
			if (operand != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
			}
			return u;
		}

    protected virtual Expression VisitBinary(BinaryExpression b, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression left = this.Visit(b.Left, stack, properties);
			Expression right = this.Visit(b.Right, stack, properties);
			Expression conversion = this.Visit(b.Conversion, stack, properties);
			if (left != b.Left || right != b.Right || conversion != b.Conversion)
			{
				if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
					return Expression.Coalesce(left, right, conversion as LambdaExpression);
				else
					return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
			}
			return b;
		}

    protected virtual Expression VisitTypeIs(TypeBinaryExpression b, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression expr = this.Visit(b.Expression, stack, properties);
			if (expr != b.Expression)
			{
				return Expression.TypeIs(expr, b.TypeOperand);
			}
			return b;
		}

    protected virtual Expression VisitConstant(ConstantExpression c, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			return c;
		}

    protected virtual Expression VisitConditional(ConditionalExpression c, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression test = this.Visit(c.Test, stack, properties);
			Expression ifTrue = this.Visit(c.IfTrue, stack, properties);
			Expression ifFalse = this.Visit(c.IfFalse, stack, properties);
			if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
			{
				return Expression.Condition(test, ifTrue, ifFalse);
			}
			return c;
		}

    protected virtual Expression VisitParameter(ParameterExpression p, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			return p;
		}

    protected virtual Expression VisitMemberAccess(MemberExpression m, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression exp = this.Visit(m.Expression, stack, properties);
			if (exp != m.Expression)
			{
				return Expression.MakeMemberAccess(exp, m.Member);
			}
			return m;
		}

    protected virtual Expression VisitMethodCall(MethodCallExpression m, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression obj = this.Visit(m.Object, stack, properties);
			IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments, stack, properties);

			if (obj != m.Object || args != m.Arguments)
			{
				return Expression.Call(obj, m.Method, args);
			}
			return m;
		}

    protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			List<Expression> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
				Expression p = this.Visit(original[i], stack, properties);
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

    protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression e = this.Visit(assignment.Expression, stack, properties);
			if (e != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, e);
			}
			return assignment;
		}

    protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings, stack, properties);
			if (bindings != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, bindings);
			}
			return binding;
		}

    protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers, stack, properties);
			if (initializers != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, initializers);
			}
			return binding;
		}

    protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			List<MemberBinding> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
				MemberBinding b = this.VisitBinding(original[i], stack, properties);
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

    protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			List<ElementInit> list = null;
			for (int i = 0, n = original.Count; i < n; i++)
			{
				ElementInit init = this.VisitElementInitializer(original[i], stack, properties);
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

    protected virtual Expression VisitLambda(LambdaExpression lambda, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			Expression body = this.Visit(lambda.Body, stack, properties);
			if (body != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, body, lambda.Parameters);
			}
			return lambda;
		}

    protected virtual NewExpression VisitNew(NewExpression nex, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments, stack, properties);
			if (args != nex.Arguments)
			{
				if (nex.Members != null)
					return Expression.New(nex.Constructor, args, nex.Members);
				else
					return Expression.New(nex.Constructor, args);
			}
			return nex;
		}

    protected virtual Expression VisitMemberInit(MemberInitExpression init, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			NewExpression n = this.VisitNew(init.NewExpression, stack, properties);
			IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings, stack, properties);
			if (n != init.NewExpression || bindings != init.Bindings)
			{
				return Expression.MemberInit(n, bindings);
			}
			return init;
		}

    protected virtual Expression VisitListInit(ListInitExpression init, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			NewExpression n = this.VisitNew(init.NewExpression, stack, properties);
			IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers, stack, properties);
			if (n != init.NewExpression || initializers != init.Initializers)
			{
				return Expression.ListInit(n, initializers);
			}
			return init;
		}

    protected virtual Expression VisitNewArray(NewArrayExpression na, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions, stack, properties);
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

    protected virtual Expression VisitInvocation(InvocationExpression iv, Stack<string> stack, Stack<ElasticPropertyAttribute> properties)
		{
			IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments, stack, properties);
			Expression expr = this.Visit(iv.Expression, stack, properties);
			if (args != iv.Arguments || expr != iv.Expression)
			{
				return Expression.Invoke(expr, args);
			}
			return iv;
		}
	}


	

}

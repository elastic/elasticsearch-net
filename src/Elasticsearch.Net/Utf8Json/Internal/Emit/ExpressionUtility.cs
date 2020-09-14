#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Elasticsearch.Net.Utf8Json.Internal.Emit
{
    internal static class ExpressionUtility
    {
        // Method

		private static MethodInfo GetMethodInfoCore(LambdaExpression expression)
        {
            if (expression == null)
				throw new ArgumentNullException(nameof(expression));

            return (expression.Body as MethodCallExpression).Method;
        }

        /// <summary>
        /// Get MethodInfo from Expression for Static(with result) method.
        /// </summary>
        public static MethodInfo GetMethodInfo<T>(Expression<Func<T>> expression) => GetMethodInfoCore(expression);

		/// <summary>
        /// Get MethodInfo from Expression for Static(void) method.
        /// </summary>
        public static MethodInfo GetMethodInfo(Expression<Action> expression) => GetMethodInfoCore(expression);

		/// <summary>
        /// Get MethodInfo from Expression for Instance(with result) method.
        /// </summary>
        public static MethodInfo GetMethodInfo<T, TR>(Expression<Func<T, TR>> expression) => GetMethodInfoCore(expression);

		/// <summary>
        /// Get MethodInfo from Expression for Instance(void) method.
        /// </summary>
        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression) => GetMethodInfoCore(expression);

		// WithArgument(for ref, out) helper

        /// <summary>
        /// Get MethodInfo from Expression for Instance(void) method.
        /// </summary>
        public static MethodInfo GetMethodInfo<TArg1, TArg2>(Expression<Action<TArg1, TArg2>> expression) => GetMethodInfoCore(expression);

		/// <summary>
        /// Get MethodInfo from Expression for Instance(with result) method.
        /// </summary>
        public static MethodInfo GetMethodInfo<T, TArg1, TR>(Expression<Func<T, TArg1, TR>> expression) => GetMethodInfoCore(expression);

		// Property

		private static MemberInfo GetMemberInfoCore<T>(Expression<T> source)
        {
            if (source == null)
				throw new ArgumentNullException("source");

            var memberExpression = source.Body as MemberExpression;
            return memberExpression.Member;
        }

        public static PropertyInfo GetPropertyInfo<T, TR>(Expression<Func<T, TR>> expression) => GetMemberInfoCore(expression) as PropertyInfo;

		// Field

        public static FieldInfo GetFieldInfo<T, TR>(Expression<Func<T, TR>> expression) => GetMemberInfoCore(expression) as FieldInfo;
	}
}

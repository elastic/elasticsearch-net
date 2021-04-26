/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Term))]
	public interface ITerm
	{
		Field Field { get; set; }
		object Missing { get; set; }
	}

	public class Term : ITerm
	{
		[DataMember(Name = "field")]
		public Field Field { get; set; }
		[DataMember(Name = "missing")]
		public object Missing { get; set; }
	}

	public class TermDescriptor<T> : DescriptorBase<TermDescriptor<T>, ITerm>, ITerm where T : class
	{
		Field ITerm.Field { get; set; }
		object ITerm.Missing { get; set; }

		public TermDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);
		public TermDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);
		public TermDescriptor<T> Missing(object missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}

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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SuggestContextFormatter))]
	public interface ISuggestContext
	{
		[DataMember(Name = "name")]
		string Name { get; set; }

		[DataMember(Name = "path")]
		Field Path { get; set; }

		[DataMember(Name = "type")]
		string Type { get; }
	}

	public abstract class SuggestContextBase : ISuggestContext
	{
		public string Name { get; set; }
		public Field Path { get; set; }
		public abstract string Type { get; }
	}

	public abstract class SuggestContextDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggestContext
		where TDescriptor : SuggestContextDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggestContext
		where TInterface : class, ISuggestContext
	{
		[IgnoreDataMember]
		protected abstract string Type { get; }
		string ISuggestContext.Name { get; set; }
		Field ISuggestContext.Path { get; set; }
		string ISuggestContext.Type => Type;

		public TDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		public TDescriptor Path(Field field) => Assign(field, (a, v) => a.Path = v);

		public TDescriptor Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);
	}
}

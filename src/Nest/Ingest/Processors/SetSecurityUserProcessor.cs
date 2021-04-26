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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Sets user-related details (such as username, roles, email, full_name and metadata ) from the
	/// current authenticated user to the current document by pre-processing the ingest.
	/// </summary>
	[InterfaceDataContract]
	public interface ISetSecurityUserProcessor : IProcessor
	{
		/// <summary>The field to store the user information into. </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// Controls what user related properties are added to the field.
		/// Defaults to username, roles, email, full_name, metadata
		/// </summary>
		[DataMember(Name = "properties")]
		IEnumerable<string> Properties { get; set; }
	}

	/// <inheritdoc cref="ISetSecurityUserProcessor" />
	public class SetSecurityUserProcessor : ProcessorBase, ISetSecurityUserProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public IEnumerable<string> Properties { get; set; }
		protected override string Name => "set_security_user";
	}

	/// <inheritdoc cref="ISetSecurityUserProcessor" />
	public class SetSecurityUserProcessorDescriptor<T>
		: ProcessorDescriptorBase<SetSecurityUserProcessorDescriptor<T>, ISetSecurityUserProcessor>, ISetSecurityUserProcessor
		where T : class
	{
		protected override string Name => "set_security_user";
		Field ISetSecurityUserProcessor.Field { get; set; }
		IEnumerable<string> ISetSecurityUserProcessor.Properties { get; set; }

		/// <inheritdoc cref="ISetSecurityUserProcessor.Field"/>
		public SetSecurityUserProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Field"/>
		public SetSecurityUserProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Properties"/>
		public SetSecurityUserProcessorDescriptor<T> Properties(IEnumerable<string> properties) =>
			Assign(properties, (a, v) => a.Properties = v);

		/// <inheritdoc cref="ISetSecurityUserProcessor.Properties"/>
		public SetSecurityUserProcessorDescriptor<T> Properties(params string[] properties) =>
			Assign(properties, (a, v) => a.Properties = v);
	}
}

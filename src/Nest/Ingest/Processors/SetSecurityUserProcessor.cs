// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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

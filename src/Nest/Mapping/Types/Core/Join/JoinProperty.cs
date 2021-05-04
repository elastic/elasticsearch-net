// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The join datatype is a special field that creates parent/child relation within documents of the same index.
	/// </summary>
	[InterfaceDataContract]
	public interface IJoinProperty : IProperty
	{
		/// <summary>
		/// Defines a set of possible relations within the documents,
		/// each relation being a parent name and a child name.
		/// </summary>
		[DataMember(Name = "relations")]
		IRelations Relations { get; set; }
	}

	/// <inheritdoc cref="IJoinProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinProperty : PropertyBase, IJoinProperty
	{
		public JoinProperty() : base(FieldType.Join) { }

		public IRelations Relations { get; set; }
	}

	/// <inheritdoc cref="IJoinProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class JoinPropertyDescriptor<T> : PropertyDescriptorBase<JoinPropertyDescriptor<T>, IJoinProperty, T>, IJoinProperty
		where T : class
	{
		public JoinPropertyDescriptor() : base(FieldType.Join) { }

		IRelations IJoinProperty.Relations { get; set; }

		/// <inheritdoc cref="IJoinProperty.Relations"/>
		public JoinPropertyDescriptor<T> Relations(Func<RelationsDescriptor, IPromise<IRelations>> selector) =>
			Assign(selector, (a, v) => a.Relations = v?.Invoke(new RelationsDescriptor())?.Value);
	}
}

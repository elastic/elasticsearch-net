// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A special single bucket aggregation that selects parent documents that
	/// have the specified type, as defined in a join field.
	/// </summary>
	/// <remarks>
	/// Valid only in Elasticsearch 6.6.0+
	/// </remarks>
	[InterfaceDataContract]
	[ReadAs(typeof(ParentAggregation))]
	public interface IParentAggregation : IBucketAggregation
	{
		/// <summary>
		/// The type for the child in the parent/child relationship
		/// </summary>
		[DataMember(Name = "type")]
		RelationName Type { get; set; }
	}

	/// <inheritdoc cref="IParentAggregation"/>
	public class ParentAggregation : BucketAggregationBase, IParentAggregation
	{
		internal ParentAggregation() { }

		public ParentAggregation(string name, RelationName type) : base(name) => Type = type;

		public RelationName Type { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Parent = this;
	}

	/// <inheritdoc cref="IParentAggregation"/>
	public class ParentAggregationDescriptor<T, TParent>
		: BucketAggregationDescriptorBase<ParentAggregationDescriptor<T, TParent>, IParentAggregation, TParent>, IParentAggregation
		where T : class
		where TParent : class
	{
		RelationName IParentAggregation.Type { get; set; } = typeof(T);

		public ParentAggregationDescriptor<T, TParent> Type(RelationName type) =>
			Assign(type, (a, v) => a.Type = v);

		public ParentAggregationDescriptor<T, TParent> Type<TOtherParent>() =>
			Assign(typeof(TOtherParent), (a, v) => a.Type = v);
	}
}

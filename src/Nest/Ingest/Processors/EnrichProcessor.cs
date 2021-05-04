// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The enrich processor can enrich documents with data from another index.
	/// </summary>
	[InterfaceDataContract]
	public interface IEnrichProcessor : IProcessor
	{
		/// <summary>
		/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// The name of the enrich policy to use.
		/// </summary>
		[DataMember(Name = "policy_name")]
		string PolicyName { get; set; }

		/// <summary>
		/// Field added to incoming documents to contain enrich data. This field contains both the match_field and enrich_fields specified
		/// in the enrich policy.
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field"/> does not exist, the processor quietly exits without modifying the document
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// If processor will update fields with pre-existing non-null-valued field. When set to false, such fields will not be touched.
		/// </summary>
		[DataMember(Name = "override")]
		bool? Override { get; set; }

		/// <summary>
		/// The maximum number of matched documents to include under the configured <see cref="TargetField"/>. The <see cref="TargetField"/> will be turned into a
		/// json array if max_matches is higher than 1, otherwise target_field will become a json object. In order to avoid documents getting
		/// too large, the maximum allowed value is 128.
		/// </summary>
		[DataMember(Name = "max_matches")]
		int? MaxMatches { get; set; }

		/// <summary>
		/// A spatial relation operator used to match the geo_shape of incoming documents to documents in the enrich index.
		/// This option is only used for geo_match enrich policy types. The geo_shape strategy mapping parameter determines which spatial
		/// relation operators are available.
		/// </summary>
		[DataMember(Name = "shape_relation")]
		GeoShapeRelation? ShapeRelation { get; set; }
	}

	/// <inheritdoc cref="IEnrichProcessor"/>
	public class EnrichProcessor : ProcessorBase, IEnrichProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public string PolicyName { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public bool? Override { get; set; }

		/// <inheritdoc />
		public int? MaxMatches { get; set; }

		/// <inheritdoc />
		public GeoShapeRelation? ShapeRelation { get; set; }

		protected override string Name => "enrich";
	}

	/// <inheritdoc cref="IEnrichProcessor"/>
	public class EnrichProcessorDescriptor<T> : ProcessorDescriptorBase<EnrichProcessorDescriptor<T>, IEnrichProcessor>, IEnrichProcessor
		where T : class
	{
		protected override string Name => "enrich";
		Field IEnrichProcessor.Field { get; set; }
		Field IEnrichProcessor.TargetField { get; set; }
		string IEnrichProcessor.PolicyName { get; set; }
		bool? IEnrichProcessor.IgnoreMissing { get; set; }
		bool? IEnrichProcessor.Override { get; set; }
		int? IEnrichProcessor.MaxMatches { get; set; }
		GeoShapeRelation? IEnrichProcessor.ShapeRelation { get; set; }

		/// <inheritdoc cref="IEnrichProcessor.Field"/>
		public EnrichProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IEnrichProcessor.Field"/>
		public EnrichProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IEnrichProcessor.PolicyName"/>
		public EnrichProcessorDescriptor<T> PolicyName(string policyName) =>
			Assign(policyName, (a, v) => a.PolicyName = v);

		/// <inheritdoc cref="IEnrichProcessor.TargetField"/>
		public EnrichProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IEnrichProcessor.TargetField"/>
		public EnrichProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IEnrichProcessor.IgnoreMissing"/>
		public EnrichProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) =>
			Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="IEnrichProcessor.Override"/>
		public EnrichProcessorDescriptor<T> Override(bool? @override = true) =>
			Assign(@override, (a, v) => a.Override = v);

		/// <inheritdoc cref="IEnrichProcessor.MaxMatches"/>
		public EnrichProcessorDescriptor<T> MaxMatches(int? maxMatches) =>
			Assign(maxMatches, (a, v) => a.MaxMatches = v);

		/// <inheritdoc cref="IEnrichProcessor.ShapeRelation"/>
		public EnrichProcessorDescriptor<T> ShapeRelation(GeoShapeRelation? relation) =>
			Assign(relation, (a, v) => a.ShapeRelation = v);
	}
}

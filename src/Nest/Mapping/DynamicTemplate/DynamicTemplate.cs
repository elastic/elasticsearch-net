// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;


namespace Nest
{
	/// <summary>
	/// A Dynamic template that defines custom mappings to be applied
	/// to dynamically added fields based on:
	/// <para />
	/// - the datatype detected by Elasticsearch, with <see cref="MatchMappingType" />.
	/// <para />
	/// - the name of the field, with <see cref="Match" /> and <see cref="Unmatch" /> or <see cref="MatchPattern" />.
	/// <para />
	/// - the full dotted path to the field, with <see cref="PathMatch" /> and <see cref="PathUnmatch" />.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(DynamicTemplate))]
	public interface IDynamicTemplate
	{
		/// <summary>
		/// The mapping to apply to matching fields
		/// </summary>
		[DataMember(Name ="mapping")]
		IProperty Mapping { get; set; }

		/// <summary>
		/// A pattern to match on the field name
		/// </summary>
		[DataMember(Name ="match")]
		string Match { get; set; }

		/// <summary>
		/// Matches on the datatype detected by dynamic field mapping,
		/// in other words, the datatype that Elasticsearch thinks the field should have.
		/// Only the following datatypes can be automatically detected: boolean, date, double,
		/// long, object, string. It also accepts * to match all datatypes.
		/// </summary>
		[DataMember(Name ="match_mapping_type")]
		string MatchMappingType { get; set; }

		/// <summary>
		/// Adjusts the behavior of <see cref="Match" /> such that it supports full
		/// Java regular expression matching on the field name instead of simple wildcards
		/// </summary>
		[DataMember(Name ="match_pattern")]
		MatchType? MatchPattern { get; set; }

		/// <summary>
		/// A pattern to match on the field name, which may be the full dotted path
		/// to the field name
		/// </summary>
		[DataMember(Name ="path_match")]
		string PathMatch { get; set; }

		/// <summary>
		/// A pattern to exclude fields matched by <see cref="PathMatch" />
		/// </summary>
		[DataMember(Name ="path_unmatch")]
		string PathUnmatch { get; set; }

		/// <summary>
		/// A pattern to exclude fields matched by <see cref="Match" />
		/// </summary>
		[DataMember(Name ="unmatch")]
		string Unmatch { get; set; }
	}

	/// <inheritdoc />
	public class DynamicTemplate : IDynamicTemplate
	{
		/// <inheritdoc />
		public IProperty Mapping { get; set; }

		/// <inheritdoc />
		public string Match { get; set; }

		/// <inheritdoc />
		public string MatchMappingType { get; set; }

		/// <inheritdoc />
		public MatchType? MatchPattern { get; set; }

		/// <inheritdoc />
		public string PathMatch { get; set; }

		/// <inheritdoc />
		public string PathUnmatch { get; set; }

		/// <inheritdoc />
		public string Unmatch { get; set; }
	}

	/// <inheritdoc cref="IDynamicTemplate" />
	public class DynamicTemplateDescriptor<T> : DescriptorBase<DynamicTemplateDescriptor<T>, IDynamicTemplate>, IDynamicTemplate
		where T : class
	{
		IProperty IDynamicTemplate.Mapping { get; set; }
		string IDynamicTemplate.Match { get; set; }
		string IDynamicTemplate.MatchMappingType { get; set; }
		MatchType? IDynamicTemplate.MatchPattern { get; set; }
		string IDynamicTemplate.PathMatch { get; set; }
		string IDynamicTemplate.PathUnmatch { get; set; }
		string IDynamicTemplate.Unmatch { get; set; }

		/// <inheritdoc cref="IDynamicTemplate.Match" />
		public DynamicTemplateDescriptor<T> Match(string match) => Assign(match, (a, v) => a.Match = v);

		/// <inheritdoc cref="IDynamicTemplate.MatchPattern" />
		public DynamicTemplateDescriptor<T> MatchPattern(MatchType? matchPattern) => Assign(matchPattern, (a, v) => a.MatchPattern = v);

		/// <inheritdoc cref="IDynamicTemplate.Unmatch" />
		public DynamicTemplateDescriptor<T> Unmatch(string unMatch) => Assign(unMatch, (a, v) => a.Unmatch = v);

		/// <inheritdoc cref="IDynamicTemplate.MatchMappingType" />
		public DynamicTemplateDescriptor<T> MatchMappingType(string matchMappingType) => Assign(matchMappingType, (a, v) => a.MatchMappingType = v);

		/// <inheritdoc cref="IDynamicTemplate.PathMatch" />
		public DynamicTemplateDescriptor<T> PathMatch(string pathMatch) => Assign(pathMatch, (a, v) => a.PathMatch = v);

		/// <inheritdoc cref="IDynamicTemplate.PathUnmatch" />
		public DynamicTemplateDescriptor<T> PathUnmatch(string pathUnmatch) => Assign(pathUnmatch, (a, v) => a.PathUnmatch = v);

		/// <inheritdoc cref="IDynamicTemplate.Mapping" />
		public DynamicTemplateDescriptor<T> Mapping(Func<SingleMappingSelector<T>, IProperty> mappingSelector) =>
			Assign(mappingSelector, (a, v) => a.Mapping = v?.Invoke(new SingleMappingSelector<T>()));
	}

	/// <summary>
	/// Dynamic match pattern type
	/// </summary>
	[StringEnum]
	public enum MatchType
	{
		/// <summary>
		/// Simple matching with wildcards
		/// </summary>
		[EnumMember(Value = "simple")]
		Simple,

		/// <summary>
		/// Regular expression matching
		/// </summary>
		[EnumMember(Value = "regex")]
		Regex
	}
}

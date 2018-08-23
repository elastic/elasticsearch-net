using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// A Dynamic template that defines custom mappings to be applied
	/// to dynamically added fields based on:
	/// <para />- the datatype detected by Elasticsearch, with <see cref="MatchMappingType"/>.
	/// <para />- the name of the field, with <see cref="Match"/> and <see cref="Unmatch"/> or <see cref="MatchPattern"/>.
	/// <para />- the full dotted path to the field, with <see cref="PathMatch"/> and <see cref="PathUnmatch"/>.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DynamicTemplate>))]
	public interface IDynamicTemplate
	{
		/// <summary>
		/// A pattern to match on the field name
		/// </summary>
		[JsonProperty("match")]
		string Match { get; set; }

		/// <summary>
		/// Adjusts the behavior of <see cref="Match"/> such that it supports full
		/// Java regular expression matching on the field name instead of simple wildcards
		/// </summary>
		[JsonProperty("match_pattern")]
		MatchType? MatchPattern { get; set; }

		/// <summary>
		/// A pattern to exclude fields matched by <see cref="Match"/>
		/// </summary>
		[JsonProperty("unmatch")]
		string Unmatch { get; set; }

		/// <summary>
		/// Matches on the datatype detected by dynamic field mapping,
		/// in other words, the datatype that Elasticsearch thinks the field should have.
		/// Only the following datatypes can be automatically detected: boolean, date, double,
		/// long, object, string. It also accepts * to match all datatypes.
		/// </summary>
		[JsonProperty("match_mapping_type")]
		string MatchMappingType { get; set; }

		/// <summary>
		/// A pattern to match on the field name, which may be the full dotted path
		/// to the field name
		/// </summary>
		[JsonProperty("path_match")]
		string PathMatch { get; set; }

		/// <summary>
		/// A pattern to exclude fields matched by <see cref="PathMatch"/>
		/// </summary>
		[JsonProperty("path_unmatch")]
		string PathUnmatch { get; set; }

		/// <summary>
		/// The mapping to apply to matching fields
		/// </summary>
		[JsonProperty("mapping")]
		IProperty Mapping { get; set; }
	}

	/// <inheritdoc />
	public class DynamicTemplate : IDynamicTemplate
	{
		/// <inheritdoc />
		public string Match { get; set; }

		/// <inheritdoc />
		public MatchType? MatchPattern { get; set; }

		/// <inheritdoc />
		public string Unmatch { get; set; }

		/// <inheritdoc />
		public string MatchMappingType { get; set; }

		/// <inheritdoc />
		public string PathMatch { get; set; }

		/// <inheritdoc />
		public string PathUnmatch { get; set; }

		/// <inheritdoc />
		public IProperty Mapping { get; set; }
	}

	/// <inheritdoc cref="IDynamicTemplate"/>
	public class DynamicTemplateDescriptor<T> : DescriptorBase<DynamicTemplateDescriptor<T>, IDynamicTemplate>, IDynamicTemplate
		where T : class
	{
		string IDynamicTemplate.Match { get; set; }
		MatchType? IDynamicTemplate.MatchPattern { get; set; }
		string IDynamicTemplate.Unmatch { get; set; }
		string IDynamicTemplate.MatchMappingType { get; set; }
		string IDynamicTemplate.PathMatch { get; set; }
		string IDynamicTemplate.PathUnmatch { get; set; }
		IProperty IDynamicTemplate.Mapping { get; set; }

		/// <inheritdoc cref="IDynamicTemplate.Match"/>
		public DynamicTemplateDescriptor<T> Match(string match) => Assign(a => a.Match = match);

		/// <inheritdoc cref="IDynamicTemplate.MatchPattern"/>
		public DynamicTemplateDescriptor<T> MatchPattern(MatchType? matchPattern) => Assign(a => a.MatchPattern = matchPattern);

		/// <inheritdoc cref="IDynamicTemplate.Unmatch"/>
		public DynamicTemplateDescriptor<T> Unmatch(string unMatch) => Assign(a => a.Unmatch = unMatch);

		/// <inheritdoc cref="IDynamicTemplate.MatchMappingType"/>
		public DynamicTemplateDescriptor<T> MatchMappingType(string matchMappingType) => Assign(a => a.MatchMappingType = matchMappingType);

		/// <inheritdoc cref="IDynamicTemplate.PathMatch"/>
		public DynamicTemplateDescriptor<T> PathMatch(string pathMatch) => Assign(a => a.PathMatch = pathMatch);

		/// <inheritdoc cref="IDynamicTemplate.PathUnmatch"/>
		public DynamicTemplateDescriptor<T> PathUnmatch(string pathUnmatch) => Assign(a => a.PathUnmatch = pathUnmatch);

		/// <inheritdoc cref="IDynamicTemplate.Mapping"/>
		public DynamicTemplateDescriptor<T> Mapping(Func<SingleMappingSelector<T>, IProperty> mappingSelector) => Assign(a => a.Mapping = mappingSelector?.Invoke(new SingleMappingSelector<T>()));
	}

	/// <summary>
	/// Dynamic match pattern type
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
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

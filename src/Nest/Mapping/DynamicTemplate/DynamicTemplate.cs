using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DynamicTemplate>))]
	public interface IDynamicTemplate
	{
		[JsonProperty("match")]
		string Match { get; set; }

		[JsonProperty("unmatch")]
		string Unmatch { get; set; }

		[JsonProperty("match_mapping_type")]
		string MatchMappingType { get; set; }

		[JsonProperty("path_match")]
		string PathMatch { get; set; }

		[JsonProperty("path_unmatch")]
		string PathUnmatch { get; set; }

		[JsonProperty("mapping")]
		IProperty Mapping { get; set; }
	}

	public class DynamicTemplate : IDynamicTemplate
	{
		public string Match { get; set; }

		public string Unmatch { get; set; }

		public string MatchMappingType { get; set; }

		public string PathMatch { get; set; }

		public string PathUnmatch { get; set; }

		public IProperty Mapping { get; set; }
	}

	public class DynamicTemplateDescriptor<T> : DescriptorBase<DynamicTemplateDescriptor<T>, IDynamicTemplate>, IDynamicTemplate
		where T : class
	{
		string IDynamicTemplate.Match { get; set; }
		string IDynamicTemplate.Unmatch { get; set; }
		string IDynamicTemplate.MatchMappingType { get; set; }
		string IDynamicTemplate.PathMatch { get; set; }
		string IDynamicTemplate.PathUnmatch { get; set; }
		IProperty IDynamicTemplate.Mapping { get; set; }

		public DynamicTemplateDescriptor<T> Match(string match) => Assign(a => a.Match = match);

		public DynamicTemplateDescriptor<T> Unmatch(string unMatch) => Assign(a => a.Unmatch = unMatch);

		public DynamicTemplateDescriptor<T> MatchMappingType(string matchMappingType) => Assign(a => a.MatchMappingType = matchMappingType);

		public DynamicTemplateDescriptor<T> PathMatch(string pathMatch) => Assign(a => a.PathMatch = pathMatch);

		public DynamicTemplateDescriptor<T> PathUnmatch(string pathUnmatch) => Assign(a => a.PathUnmatch = pathUnmatch);

		public DynamicTemplateDescriptor<T> Mapping(Func<SingleMappingDescriptor<T>, IProperty> mappingSelector) => Assign(a => a.Mapping = mappingSelector?.Invoke(new SingleMappingDescriptor<T>()));
	}
}
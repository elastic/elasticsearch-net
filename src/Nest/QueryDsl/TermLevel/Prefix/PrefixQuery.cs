using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class PrefixQuery : FieldNameQuery, IPrefixQuery
	{
		bool IQuery.Conditionless => TermQuery.IsConditionless(this);
		public object Value { get; set; }
		public double? Boost { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Prefix = this;
	}

	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>, 
		IPrefixQuery where T : class
	{
		private IPrefixQuery Self => this;

		PropertyPath IFieldNameQuery.Field { get; set; }
		RewriteMultiTerm? IPrefixQuery.Rewrite { get; set; }

		public PrefixQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}
	}
}

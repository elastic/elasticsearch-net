using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<PrefixQuery>))]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonProperty("rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }
	}

	public class PrefixQuery : FieldNameQueryBase, IPrefixQuery
	{
		protected override bool Conditionless => TermQuery.IsConditionless(this);
		public object Value { get; set; }

		public MultiTermQueryRewrite Rewrite { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Prefix = this;
	}

	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, IPrefixQuery, T>,
		IPrefixQuery where T : class
	{
		MultiTermQueryRewrite IPrefixQuery.Rewrite { get; set; }

		public PrefixQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => a.Rewrite = rewrite);
		}
		}

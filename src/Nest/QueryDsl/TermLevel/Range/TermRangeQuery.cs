using Newtonsoft.Json;

namespace Nest
{
    [JsonConverter(typeof(FieldNameQueryJsonConverter<TermRangeQuery>))]
    public interface ITermRangeQuery : IRangeQuery
    {
        [JsonProperty("gte")]
        string GreaterThanOrEqualTo { get; set; }

        [JsonProperty("lte")]
        string LessThanOrEqualTo { get; set; }

        [JsonProperty("gt")]
        string GreaterThan { get; set; }

        [JsonProperty("lt")]
        string LessThan { get; set; }
    }

    public class TermRangeQuery : FieldNameQueryBase, ITermRangeQuery
    {
        protected override bool Conditionless => IsConditionless(this);
        public string GreaterThanOrEqualTo { get; set; }
        public string LessThanOrEqualTo { get; set; }
        public string GreaterThan { get; set; }
        public string LessThan { get; set; }

        internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

        internal static bool IsConditionless(ITermRangeQuery q)
        {
            return q.Field.IsConditionless()
                   || (q.GreaterThanOrEqualTo.IsNullOrEmpty()
                       && q.LessThanOrEqualTo.IsNullOrEmpty()
                       && q.GreaterThan.IsNullOrEmpty()
                       && q.LessThan.IsNullOrEmpty());
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TermRangeQueryDescriptor<T>
        : FieldNameQueryDescriptorBase<TermRangeQueryDescriptor<T>, ITermRangeQuery, T>
        , ITermRangeQuery where T : class
    {
        protected override bool Conditionless => TermRangeQuery.IsConditionless(this);
        string ITermRangeQuery.GreaterThanOrEqualTo { get; set; }
        string ITermRangeQuery.LessThanOrEqualTo { get; set; }
        string ITermRangeQuery.GreaterThan { get; set; }
        string ITermRangeQuery.LessThan { get; set; }

        public TermRangeQueryDescriptor<T> GreaterThan(string from) => Assign(a => a.GreaterThan = from);

        public TermRangeQueryDescriptor<T> GreaterThanOrEquals(string from) => Assign(a => a.GreaterThanOrEqualTo = from);

        public TermRangeQueryDescriptor<T> LessThan(string to) => Assign(a => a.LessThan = to);

        public TermRangeQueryDescriptor<T> LessThanOrEquals(string to) => Assign(a => a.LessThanOrEqualTo = to);
    }
}

namespace Nest
{
    public class NestedElasticPropertyAttribute : ElasticPropertyAttribute, IElasticPropertyAttribute
    {
        public bool IncludeInParent { get; set; }
        public bool IncludeInRoot { get; set; }

        public NestedElasticPropertyAttribute()
        {
            Type = FieldType.nested;
        }

        public void Accept(IElasticPropertyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
namespace Nest {
    public interface IElasticPropertyVisitor
    {
        void Visit(ElasticPropertyAttribute attribute);
        void Visit(NestedElasticPropertyAttribute attribute);
    }
}
namespace Nest {
    public interface IElasticPropertyAttribute
    {
        void Accept(IElasticPropertyVisitor visitor);
    }
}
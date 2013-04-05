namespace Nest.Dsl.Factory
{
    public interface ISortBuilder : IJsonSerializable
    {
        /// <summary>
        /// The order of sorting. Defaults to {@link SortOrder#ASC}.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        ISortBuilder Order(SortOrder order);

        /// <summary>
        /// Sets the value when a field is missing in a doc. Can also be set to <tt>_last</tt> or
        /// <tt>_first</tt> to sort missing last or first respectively.
        /// </summary>
        /// <param name="missing"></param>
        /// <returns></returns>
        ISortBuilder Missing(object missing);
    }
}
using Nest.FactoryDsl.Sort;

namespace Nest.FactoryDsl
{
    public static class SortFactory
    {
        /// <summary>
        /// Constructs a new score sort.
        /// </summary>
        /// <returns></returns>
        public static ScoreSortBuilder ScoreSort()
        {
            return new ScoreSortBuilder();
        }

        /// <summary>
        /// Constructs a new field based sort.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static FieldSortBuilder FieldSort(string field)
        {
            return new FieldSortBuilder(field);
        }

        /// <summary>
        /// Constructs a new script based sort.
        /// </summary>
        /// <param name="script">The script to use</param>
        /// <param name="type">The type, can either be "string" or "number"</param>
        /// <returns></returns>
        public static ScriptSortBuilder ScriptSort(string script, string type)
        {
            return new ScriptSortBuilder(script, type);
        }

        /// <summary>
        /// A geo distance based sort.
        /// </summary>
        /// <param name="fieldName">The geo point like field name.</param>
        /// <returns></returns>
        public static GeoDistanceSortBuilder GeoDistanceSort(string fieldName)
        {
            return new GeoDistanceSortBuilder(fieldName);
        }
    }
}
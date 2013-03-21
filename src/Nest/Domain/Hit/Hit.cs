using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    public interface IMyDictionary<out T> where T : class
    {
        T this[string property] { get; }

        IEnumerable<IMyDictionaryItem<T>> Items { get; }
    }

    public interface IMyDictionaryItem<out T> where T : class
    {
        string Key { get; }
        T Value { get; }
    }

    public class MyDictionary<T> : IMyDictionary<T> where T : class
    {
        public T this[string property]
        {
            get
            {
                if (this.Items != null && this.Items.Count() > 0)
                    this.Items.FirstOrDefault(x => x.Key == property);

                return null;
            }
            internal set { throw new NotImplementedException(); }
        }

        public IEnumerable<IMyDictionaryItem<T>> Items { get; internal set; }
    }

    public class MyDictionaryItem<T> : IMyDictionaryItem<T>
        where T : class
    {
        public string Key { get; internal set; }
        public T Value { get; internal set; }
    }
    public interface IHit<out T> where T : class
    {
        T Fields { get; }
        T Source { get; }
        string Index { get; }
        double Score { get; }
        string Type { get; }
        string Version { get; }
        string Id { get; }

        IEnumerable<object> Sorts { get; }

        Dictionary<string, List<string>> Highlight { get; }
        Explanation Explanation { get; }
        IMyDictionary<T> PartialFields { get; }
    }

    [JsonObject]
    public class Hit<T> : IHit<T>
        where T : class
    {
        [JsonProperty(PropertyName = "fields")]
        public T Fields { get; internal set; }
        [JsonProperty(PropertyName = "_source")]
        public T Source { get; internal set; }
        [JsonProperty(PropertyName = "_index")]
        public string Index { get; internal set; }
        [JsonProperty(PropertyName = "_score")]
        public double Score { get; internal set; }
        [JsonProperty(PropertyName = "_type")]
        public string Type { get; internal set; }
        [JsonProperty(PropertyName = "_version")]
        public string Version { get; internal set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; internal set; }

        [JsonProperty(PropertyName = "sort")]
        public IEnumerable<object> Sorts { get; internal set; }

        [JsonProperty(PropertyName = "highlight")]
        public Dictionary<string, List<string>> Highlight { get; internal set; }
        [JsonProperty(PropertyName = "_explanation")]
        public Explanation Explanation { get; internal set; }

        public IMyDictionary<T> PartialFields { get; internal set; }

        public Hit()
        {

        }
    }
}

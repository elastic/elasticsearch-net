using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
    public interface IMyDictionary<out T> where T : class
    {
        T this[string key] { get; }

        IEnumerable<IMyDictionaryItem<T>> Items { get; }
    }

    public interface IMyDictionaryItem<out T> where T : class
    {
        string Key { get; }
        T Value { get; }
    }

    public class MyDictionary<T> : IMyDictionary<T> where T : class
    {
        public T this[string key]
        {
            get
            {
                var pair = this.Items.EmptyIfNull().FirstOrDefault(x => x.Key == key);
                return pair == null ? null : pair.Value;
            }
        }

        public IEnumerable<IMyDictionaryItem<T>> Items { get; internal set; }
    }

    public class MyDictionaryItem<T> : IMyDictionaryItem<T>
        where T : class
    {
        public string Key { get; internal set; }
        public T Value { get; internal set; }
    }
}

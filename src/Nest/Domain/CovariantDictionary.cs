using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
    public interface ICovariantDictionary<out T> where T : class
    {
        T this[string key] { get; }

        IEnumerable<ICovariantItem<T>> Items { get; }
    }

    public interface ICovariantItem<out T> where T : class
    {
        string Key { get; }
        T Value { get; }
    }

    public class CovariantDictionary<T> : ICovariantDictionary<T> where T : class
    {
        public T this[string key]
        {
            get
            {
                var pair = this.Items.EmptyIfNull().FirstOrDefault(x => x.Key == key);
                return pair == null ? null : pair.Value;
            }
        }

        public IEnumerable<ICovariantItem<T>> Items { get; internal set; }
    }

    public class CovariantItem<T> : ICovariantItem<T>
        where T : class
    {
        public string Key { get; internal set; }
        public T Value { get; internal set; }
    }
}

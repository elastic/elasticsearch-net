#if NETSTANDARD

using System;

namespace Utf8Json.Internal
{
    // Unity compiler can't understand this.

    internal static class FuncExtensions
    {
        // hack of avoid closure allocation(() => value).
        public static Func<T> AsFunc<T>(this T value)
        {
            return new Func<T>(value.ReturnBox<T>);
        }

        public static Func<T> AsFuncFast<T>(this T value) where T : class
        {
            return new Func<T>(value.Return<T>);
        }

        static T Return<T>(this T value)
        {
            return value;
        }

        static T ReturnBox<T>(this object value)
        {
            return (T)value;
        }
    }
}


#endif
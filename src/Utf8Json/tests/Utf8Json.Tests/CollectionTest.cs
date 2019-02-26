using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class CollectionTest
    {
        T Convert<T>(T value)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
        }

        public static object collectionTestData = new object[]
        {
            new object[]{ new int[]{ 1,10, 100 } , null },
            new object[]{ new List<int>{ 1,10, 100 } , null },
            new object[]{ new LinkedList<int>(new[] { 1, 10, 100 }) , null },
            new object[]{ new Queue<int>(new[] { 1, 10, 100 }) , null },
            new object[]{ new HashSet<int>(new[] { 1, 10, 100 }), null },
            new object[]{ new ReadOnlyCollection<int>(new[] { 1, 10, 100 }), null },
            new object[]{ new ObservableCollection<int>(new[] { 1, 10, 100 }), null },
            new object[]{ new ReadOnlyObservableCollection<int>(new ObservableCollection<int>(new[] { 1, 10, 100 })), null },
        };

        [Theory]
        [MemberData(nameof(collectionTestData))]
        public void ConcreteCollectionTest<T>(T x, T y)
        {
            Convert(x).IsStructuralEqual(x);
            Convert(y).IsStructuralEqual(y);
        }

        [Fact]
        public void InterfaceCollectionTest()
        {
            var a = (IList<int>)new int[] { 1, 10, 100 };
            var b = (ICollection<int>)new int[] { 1, 10, 100 };
            var c = (Enumerable.Range(1, 100).AsEnumerable());
            var d = (IReadOnlyList<int>)new int[] { 1, 10, 100 };
            var e = (IReadOnlyCollection<int>)new int[] { 1, 10, 100 };
            var f = (ISet<int>)new HashSet<int>(new[] { 1, 10, 100 });
            var g = (ILookup<bool, int>)Enumerable.Range(1, 100).ToLookup(x => x % 2 == 0);

            Convert(a).Is(a);
            Convert(b).Is(b);
            Convert(c).Is(c);
            Convert(d).Is(d);
            Convert(e).Is(e);
            Convert(f).Is(f);

            var check = Utf8Json.JsonSerializer.ToJsonString(g);
            Convert(g).Is(g);

            a = null;
            b = null;
            c = null;
            d = null;
            e = null;
            f = null;
            g = null;

            Convert(a).Is(a);
            Convert(b).Is(b);
            Convert(c).IsNull();
            Convert(d).Is(d);
            Convert(e).Is(e);
            Convert(f).Is(f);
            Convert(g).Is(g);
        }

        [Fact]
        public void StackTest()
        {
            var stack = new Stack<int>(new[] { 1, 10, 100 });
            stack.AsEnumerable().Is(100, 10, 1);
            Convert(stack).AsEnumerable().Is(100, 10, 1);

            stack = new Stack<int>();
            Convert(stack).AsEnumerable().Count().Is(0);

            stack = null;
            Convert(stack).IsNull();
        }

        [Fact]
        public void ConcurrentCollectionTest()
        {
            var c0 = new ConcurrentQueue<int>(new[] { 1, 10, 100 });
            var c1 = new ConcurrentStack<int>(new[] { 1, 10, 100 });
            var c2 = new ConcurrentBag<int>(new[] { 1, 10, 100 });

            Convert(c0).Is(1, 10, 100);
            Convert(c1).Is(100, 10, 1);

            Convert(c2).OrderBy(x => x).Is(1, 10, 100);

            c0 = null;
            c1 = null;
            c2 = null;

            Convert(c0).IsNull();
            Convert(c1).IsNull();
            Convert(c2).IsNull();
        }

        [Fact]
        public void ArraySegmentTest()
        {
            var test = new ArraySegment<int>(new[] { 1, 10, 100 });
            Convert(test).Is(1, 10, 100);
            ArraySegment<int>? nullableTest = new ArraySegment<int>(new[] { 1, 10, 100 });
            Convert(nullableTest).Is(1, 10, 100);
            nullableTest = null;
            Convert(nullableTest).IsNull();
        }

        [Fact]
        public void ByteArrayArrayTest()
        {
            var xs1 = new byte[100];
            var xs2 = new byte[100];
            for (int i = 0; i < xs1.Length; i++)
            {
                xs1[i] = 1;
                xs2[i] = 1;
            }

            var bin = JsonSerializer.Serialize(new[] { xs1, xs2 });
            var foo = JsonSerializer.Deserialize<byte[][]>(bin);
            for (int i = 0; i < foo[0].Length; i++)
            {
                foo[0][i].Is((byte)1);
                foo[1][i].Is((byte)1);
            }
        }
    }
}

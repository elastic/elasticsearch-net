using System;
using System.Collections.Generic;

namespace SharedData
{
    public enum ByteEnum : byte { A, B, C, D, E }
    public enum SByteEnum : sbyte { A, B, C, D, E }
    public enum ShortEnum : short { A, B, C, D, E }
    public enum UShortEnum : ushort { A, B, C, D, E }
    public enum IntEnum : int { A, B, C, D, E }
    public enum UIntEnum : uint { A, B, C, D, E }
    public enum LongEnum : long { A, B, C, D, E }
    public enum ULongEnum : ulong { A, B, C, D, E }

    
    public class FirstSimpleData
    {
        
        public int Prop1 { get; set; }
        
        public string Prop2 { get; set; }
        
        public int Prop3 { get; set; }
    }

    
    public class SimpleIntKeyData
    {
        
        public int Prop1 { get; set; }
        
        public ByteEnum Prop2 { get; set; }
        
        public string Prop3 { get; set; }
        
        public SimlpeStringKeyData Prop4 { get; set; }
        
        public SimpleStructIntKeyData Prop5 { get; set; }
        
        public SimpleStructStringKeyData Prop6 { get; set; }
        
        public byte[] BytesSpecial { get; set; }
    }

    
    public class SimlpeStringKeyData
    {
        public int Prop1 { get; set; }
        public ByteEnum Prop2 { get; set; }
        public int Prop3 { get; set; }
    }

    
    public struct SimpleStructIntKeyData
    {
        
        public int X { get; set; }
        
        public int Y { get; set; }
        
        public byte[] BytesSpecial { get; set; }
    }


    
    public struct SimpleStructStringKeyData
    {
        
        public int X { get; set; }
        
        public int[] Y { get; set; }
    }

    
    public struct Vector2
    {
        
        public readonly float X;
        
        public readonly float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    
    public class EmptyClass
    {

    }

    
    public struct EmptyStruct
    {

    }



    
    public class Version1
    {
        
        public int MyProperty1 { get; set; }
        
        public int MyProperty2 { get; set; }
        
        public int MyProperty3 { get; set; }
    }


    
    public class Version2
    {
        
        public int MyProperty1 { get; set; }
        
        public int MyProperty2 { get; set; }
        
        public int MyProperty3 { get; set; }
        // 
        // public int MyProperty4 { get; set; }
        
        public int MyProperty5 { get; set; }
    }


    
    public class Version0
    {
        
        public int MyProperty1 { get; set; }
    }

    
    public class HolderV1
    {
        
        public Version1 MyProperty1 { get; set; }
        
        public int After { get; set; }
    }

    
    public class HolderV2
    {
        
        public Version2 MyProperty1 { get; set; }
        
        public int After { get; set; }
    }

    
    public class HolderV0
    {
        
        public Version0 MyProperty1 { get; set; }
        
        public int After { get; set; }
    }

    

    
    public class GenericClass<T1, T2>
    {
        
        public T1 MyProperty0 { get; set; }
        
        public T2 MyProperty1 { get; set; }
    }

    
    public struct GenericStruct<T1, T2>
    {
        
        public T1 MyProperty0 { get; set; }
        
        public T2 MyProperty1 { get; set; }
    }


    
    public class VersionBlockTest
    {
        
        public int MyProperty { get; set; }

        
        public MyClass UnknownBlock { get; set; }

        
        public int MyProperty2 { get; set; }
    }

    
    public class UnVersionBlockTest
    {
        
        public int MyProperty { get; set; }

        //
        //public MyClass UnknownBlock { get; set; }

        
        public int MyProperty2 { get; set; }
    }

    
    public class MyClass
    {
        
        public int MyProperty1 { get; set; }
        
        public int MyProperty2 { get; set; }
        
        public int MyProperty3 { get; set; }
    }


    
    public class Empty1
    {
    }


    
    public class Empty2
    {
    }

    
    public class NonEmpty1
    {
        
        public int MyProperty { get; set; }
    }


    
    public class NonEmpty2
    {
        
        public int MyProperty { get; set; }
    }

    
    public struct VectorLike2
    {
        
        public float x;
        
        public float y;

        
        public VectorLike2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    
    public struct Vector3Like
    {
        
        public float x;
        
        public float y;
        
        public float z;

        
        public Vector3Like(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Like operator *(Vector3Like a, float d)
        {
            return new Vector3Like(a.x * d, a.y * d, a.z * d);
        }
    }



    public class ContractlessConstructorCheck
    {
        public int MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }


        public ContractlessConstructorCheck(KeyValuePair<int, string> ok)
        {

        }

        
        public ContractlessConstructorCheck(int myProperty1, string myProperty2)
        {
            this.MyProperty1 = myProperty1;
            this.MyProperty2 = myProperty2;
        }
    }

    public class FindingConstructorCheck
    {
        public int MyProperty1 { get; private set; }
        public string MyProperty2 { get; private set; }


        public FindingConstructorCheck(KeyValuePair<int, string> ok)
        {

        }

        public FindingConstructorCheck(int myProperty1, string myProperty2)
        {
            this.MyProperty1 = myProperty1;
            this.MyProperty2 = myProperty2;
        }
    }

    
    public class ArrayOptimizeClass
    {
        
        public int MyProperty0 { get; set; }
        
        public int MyProperty1 { get; set; }
        
        public int MyProperty2 { get; set; }
        
        public int MyProperty3 { get; set; }
        
        public int MyProperty4 { get; set; }
        
        public int MyProperty5 { get; set; }
        
        public int MyProperty6 { get; set; }
        
        public int MyProperty7 { get; set; }
        
        public int MyProperty8 { get; set; }
        
        public int MyProvperty9 { get; set; }
        
        public int MyProperty10 { get; set; }
        
        public int MyProperty11 { get; set; }
        
        public int MyPropverty12 { get; set; }
        
        public int MyPropevrty13 { get; set; }
        
        public int MyProperty14 { get; set; }
        
        public int MyProperty15 { get; set; }
    }



    
    public struct DynamicArgumentTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        
        public readonly T1 Item1;
        
        public readonly T2 Item2;
        
        public readonly T3 Item3;
        
        public readonly T4 Item4;
        
        public readonly T5 Item5;
        
        public readonly T6 Item6;
        
        public readonly T7 Item7;
        
        public readonly T8 Item8;
        
        public readonly T9 Item9;

        
        public DynamicArgumentTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
            Item9 = item9;
        }
    }


    public class NestParent
    {
        
        public class NestContract
        {
            
            public int MyProperty { get; set; }
        }

        public class NestContractless
        {
            public int MyProperty { get; set; }
        }
    }

    public interface IUnionSample
    {
    }

    
    public class FooClass : IUnionSample
    {
        
        public int XYZ { get; set; }
    }

    
    public class BarClass : IUnionSample
    {
        
        public string OPQ { get; set; }
    }
}

namespace Abcdefg.Efcdigjl.Ateatatea.Hgfagfafgad
{
    
    public class TnonodsfarnoiuAtatqaga
    {
        
        public int MyProperty { get; set; }
    }
}


public class GlobalMan
{
    
    public int MyProperty { get; set; }
}


public class Message
{
    
    public int UserId { get; set; }
    
    public int RoomId { get; set; }
    
    public DateTime PostTime { get; set; }

    // 本文
    
    public IMessageBody Body { get; set; }
}

public interface IMessageBody { }


public class TextMessageBody : IMessageBody
{
    
    public string Text { get; set; }
}


public class StampMessageBody : IMessageBody
{
    
    public int StampId { get; set; }
}


public class QuestMessageBody : IMessageBody
{
    
    public int QuestId { get; set; }
    
    public string Text { get; set; }
}


public enum GlobalMyEnum
{
    Apple, Orange
}


public class ArrayTestTest
{
    
    public int[] MyProperty0 { get; set; }
    
    public int[,] MyProperty1 { get; set; }
    
    public GlobalMyEnum[,] MyProperty2 { get; set; }
    
    public int[,,] MyProperty3 { get; set; }
    
    public int[,,,] MyProperty4 { get; set; }
    
    public GlobalMyEnum[] MyProperty5 { get; set; }
    
    public QuestMessageBody[] MyProperty6 { get; set; }
}


public class ComplexModel
{
    public IDictionary<string, string> AdditionalProperty { get; private set; }

    public DateTimeOffset CreatedOn { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTimeOffset UpdatedOn { get; set; }

    public IList<SimpleModel> SimpleModels { get; private set; }

    public ComplexModel()
    {
        AdditionalProperty = new Dictionary<string, string>();
        SimpleModels = new List<SimpleModel>();
    }
}


public class SimpleModel
{

    private decimal money;

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public int Precision { get; set; }

    public SimpleModel()
    {
        Precision = 4;
    }

    public decimal Money
    {
        get
        {
            return this.money;
        }

        set
        {
            this.money = Math.Round(value, this.Precision);
        }
    }

    public long Amount
    {
        get
        {
            return (long)Math.Round(this.Money, 0, MidpointRounding.ToEven);
        }
    }
}

namespace PerfBenchmarkDotNet
{
    
    public class StringKeySerializerTarget
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public int MyProperty3 { get; set; }
        public int MyProperty4 { get; set; }
        public int MyProperty5 { get; set; }
        public int MyProperty6 { get; set; }
        public int MyProperty7 { get; set; }
        public int MyProperty8 { get; set; }
        public int MyProperty9 { get; set; }
    }

}
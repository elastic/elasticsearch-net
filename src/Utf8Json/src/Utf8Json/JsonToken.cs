namespace Utf8Json
{
    // 0 = None, 1 ~ 4 is block token, 5 ~ 9 = value token, 10 ~ 11 = delimiter token
    // you can use range-check if optimization needed.

    public enum JsonToken : byte
    {
        None = 0,
        /// <summary>{</summary>
        BeginObject = 1,
        /// <summary>}</summary>
        EndObject = 2,
        /// <summary>[</summary>
        BeginArray = 3,
        /// <summary>]</summary>
        EndArray = 4,
        /// <summary>0~9, -</summary>
        Number = 5,
        /// <summary>"</summary>
        String = 6,
        /// <summary>t</summary>
        True = 7,
        /// <summary>f</summary>
        False = 8,
        /// <summary>n</summary>
        Null = 9,
        /// <summary>,</summary>
        ValueSeparator = 10,
        /// <summary>:</summary>
        NameSeparator = 11
    }
}
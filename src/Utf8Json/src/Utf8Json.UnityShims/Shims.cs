
using System.Runtime.Serialization;
using Utf8Json;

namespace UnityEngine
{
    
    public struct Vector2
    {
        
        public float x;
        
        public float y;

        [SerializationConstructor]
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    
    public struct Vector3
    {
        
        public float x;
        
        public float y;
        
        public float z;

        [SerializationConstructor]
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }
    }

    
    public struct Vector4
    {
        
        public float x;
        
        public float y;
        
        public float z;
        
        public float w;

        [SerializationConstructor]
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    
    public struct Quaternion
    {
        
        public float x;
        
        public float y;
        
        public float z;
        
        public float w;

        [SerializationConstructor]
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    
    public struct Color
    {
        
        public float r;
        
        public float g;
        
        public float b;
        
        public float a;

        public Color(float r, float g, float b)
            : this(r, g, b, 1.0f)
        {

        }

        [SerializationConstructor]
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }

    
    public struct Bounds
    {
        
        public Vector3 center { get; set; }

        [IgnoreDataMember]
        public Vector3 extents { get; set; }

        
        public Vector3 size
        {
            get
            {
                return this.extents * 2f;
            }
            set
            {
                this.extents = value * 0.5f;
            }
        }

        [SerializationConstructor]
        public Bounds(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.extents = size * 0.5f;
        }
    }

    
    public struct Rect
    {
        
        public float x { get; set; }

        
        public float y { get; set; }

        
        public float width { get; set; }

        
        public float height { get; set; }

        [SerializationConstructor]
        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rect(Vector2 position, Vector2 size)
        {
            this.x = position.x;
            this.y = position.y;
            this.width = size.x;
            this.height = size.y;
        }

        public Rect(Rect source)
        {
            this.x = source.x;
            this.y = source.y;
            this.width = source.width;
            this.height = source.height;
        }
    }
}
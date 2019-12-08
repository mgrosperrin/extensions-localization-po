using System;
using System.Diagnostics;

namespace MGR.PortableObject.Parsing
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct PortableObjectKey : IEquatable<PortableObjectKey>
    {
        public string Id { get; }
        public string? Context { get; }
        public PortableObjectKey(string id):this(id, null) { }
        public PortableObjectKey(string id, string? context)
        {
            Id = id;
            Context = context;
        }

        public bool Equals(PortableObjectKey other)
        {
            return Id == other.Id && Context == other.Context;
        }

        public override bool Equals(object obj)
        {
            return obj is PortableObjectKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Context != null ? Context.GetHashCode() : 0);
            }
        }

        private string DebuggerDisplay =>
            Context == null ? $"Id={Id}" : $"Context={Context}¤Id={Id}";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnumObjectExperiment.Model
{
    public enum EnumerationType
    {
        None = 0,
        TaskState = 1,
        TaskPriority = 2,
    }

    public abstract class Enumeration : IComparable
    {
        public double Id { get; private set; }
        public string SystemName { get; private set; }

        public string DisplayName { get; private set; }

        // ValueObject
        public abstract EnumerationType TypeId { get; protected set; }

        protected Enumeration()
        {

        }

        protected Enumeration(double id, string systemName, string displayName)
        {
            Id = id;
            SystemName = systemName;
            DisplayName = displayName;
        }

        public override string ToString() => SystemName;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
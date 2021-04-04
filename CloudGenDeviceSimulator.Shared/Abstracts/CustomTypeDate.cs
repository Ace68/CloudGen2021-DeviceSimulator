using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public abstract class CustomTypeDate<T> where T : CustomTypeDate<T>
    {
        public DateTime Value { get; }

        protected CustomTypeDate(DateTime value) => this.Value = value;

        public override bool Equals(object other) => this.Equals(other as T);

        protected virtual IEnumerable<object> GetAttributesToIncludeInEqualityCheck() => new List<object>();

        public bool Equals(T other) => !(other == null) && this.GetAttributesToIncludeInEqualityCheck().SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());

        public static bool operator ==(CustomTypeDate<T> left, CustomTypeDate<T> right) => Equals(left, right);

        public static bool operator !=(CustomTypeDate<T> left, CustomTypeDate<T> right) => !(left == right);

        public override int GetHashCode() => this.GetAttributesToIncludeInEqualityCheck().Aggregate(17, (current, obj) => current * 31 + (obj != null ? obj.GetHashCode() : 0));

    }
}
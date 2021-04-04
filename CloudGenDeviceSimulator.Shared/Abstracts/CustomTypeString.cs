using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public abstract class CustomTypeString<T> where T : CustomTypeString<T>
    {
        public string Value { get; }

        protected CustomTypeString(string value) => this.Value = value;

        public override bool Equals(object other) => this.Equals(other as T);

        protected virtual IEnumerable<object> GetAttributesToIncludeInEqualityCheck() => new List<object>();

        public bool Equals(T other) => !(other == null) && this.GetAttributesToIncludeInEqualityCheck().SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());

        public static bool operator ==(CustomTypeString<T> left, CustomTypeString<T> right) => Equals(left, right);

        public static bool operator !=(CustomTypeString<T> left, CustomTypeString<T> right) => !(left == right);

        public override int GetHashCode() => this.GetAttributesToIncludeInEqualityCheck().Aggregate(17, (current, obj) => current * 31 + (obj != null ? obj.GetHashCode() : 0));

        public virtual Guid GetGuid()
        {
            Guid.TryParse(this.Value, out var result);
            return result;
        }

        public virtual string GetValue() => this.Value;

        public virtual bool IsValid() => !string.IsNullOrEmpty(this.Value);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudGenDeviceSimulator.Shared.Abstracts
{
    public abstract class CustomTypeBase<T> where T : CustomTypeBase<T>
    {
        public override bool Equals(object other) => this.Equals(other as T);

        protected virtual IEnumerable<object> GetAttributesToIncludeInEqualityCheck() => Enumerable.Empty<object>();

        public bool Equals(T other) => !((CustomTypeBase<T>)other == (CustomTypeBase<T>)null) && this.GetAttributesToIncludeInEqualityCheck().SequenceEqual<object>(other.GetAttributesToIncludeInEqualityCheck());

        public static bool operator ==(CustomTypeBase<T> left, CustomTypeBase<T> right) => object.Equals((object)left, (object)right);

        public static bool operator !=(CustomTypeBase<T> left, CustomTypeBase<T> right) => !(left == right);

        public override int GetHashCode() => this.GetAttributesToIncludeInEqualityCheck().Aggregate<object, int>(17, (Func<int, object, int>)((current, obj) => current * 31 + (obj != null ? obj.GetHashCode() : 0)));
    }
}
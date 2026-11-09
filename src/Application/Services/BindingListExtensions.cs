using System.ComponentModel;

namespace Jaeger.Minutas.Aplication.Services {
    internal static class BindingListExtensions {
        public static System.Collections.Generic.List<T> ToListSafe<T>(this BindingList<T> source) {
            return source == null ? new System.Collections.Generic.List<T>() : new System.Collections.Generic.List<T>(source);
        }
    }
}

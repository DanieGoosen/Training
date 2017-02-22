using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sybrin.ImageSelectorEditor.Tools {
    public static class Extensions {
        public static void RemoveAll<T>(this IList<T> list, Func<T, bool> predicate) {
            for (int i = 0; i < list.Count; i++) {
                if (predicate(list[i])) {
                    list.RemoveAt(i--);
                }
            }
        }
    }
}

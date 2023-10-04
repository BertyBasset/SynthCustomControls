using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthCustomControls.Utils;

public static class ExtensionMethods { 
    public static T Clamp<T>(this T value, T minimum, T maximum) where T : IComparable<T> {
        if (value.CompareTo(minimum) < 0)
            return minimum;
        else if (value.CompareTo(maximum) > 0)
            return maximum;
        else
            return value;
    }
}

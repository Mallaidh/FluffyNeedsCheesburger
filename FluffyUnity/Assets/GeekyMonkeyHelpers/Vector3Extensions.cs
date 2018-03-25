using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GeekyMonkeyHelpers
{
    public static class Vector3Extensions
    {
        public static Vector3 SetZ(this Vector3 v, float z)
        {
            v.Set(v.x, v.y, z);
            return v;
        }
    }
}

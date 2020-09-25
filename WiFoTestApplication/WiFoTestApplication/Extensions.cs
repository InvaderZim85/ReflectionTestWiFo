using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFoTestApplication
{
    internal static class Extensions
    {
        public static bool IsNumericType(this object type)
        {
            return Type.GetTypeCode(type.GetType()) switch
            {
                TypeCode.Byte => true,
                TypeCode.SByte => true,
                TypeCode.UInt16 => true,
                TypeCode.UInt32 => true,
                TypeCode.UInt64 => true,
                TypeCode.Int16 => true,
                TypeCode.Int32 => true,
                TypeCode.Int64 => true,
                TypeCode.Decimal => true,
                TypeCode.Double => true,
                TypeCode.Single => true,
                _ => false
            };
        }
    }
}

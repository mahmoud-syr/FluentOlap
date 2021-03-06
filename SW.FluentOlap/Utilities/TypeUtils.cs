﻿using SW.FluentOlap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.FluentOlap.Utilities
{
    internal static class TypeUtils
    {
        public static InternalType GuessType(Type type)
        {
            InternalType t;
            if (!TryGuessInternalType(type, out t))
                throw new Exception($"Could not guess type of {type.Name}, please define using Property()");
            return t;
        }


        public static bool IsPrimitive(this Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
                return true;
            if (type == typeof(string))
                return true;
            if (type == typeof(DateTime))
                return true;
            if (type == typeof(decimal))
                return true;
            return type.IsPrimitive;
        }
        
        public static bool TryGuessInternalType(Type type, out InternalType sqlType)
        {
            Type focusedType = Nullable.GetUnderlyingType(type)?? type;
            sqlType = new InternalType();

            if (focusedType.IsAssignableFrom(typeof(string)))
            {
                sqlType = InternalType.STRING;
                return true;
            }

            if (focusedType.IsAssignableFrom(typeof(DateTime)))
            {
                sqlType = InternalType.DATETIME;
                return true;
            }

            if (focusedType.IsAssignableFrom(typeof(decimal)))
            {
                sqlType = InternalType.FLOAT;
                return true;
            }
            

            if (!focusedType.IsPrimitive) return false;

            if (focusedType.IsAssignableFrom(typeof(float)))
                sqlType = InternalType.FLOAT;

            if (focusedType.IsAssignableFrom(typeof(int)))
                sqlType = InternalType.INTEGER;

            if (focusedType.IsAssignableFrom(typeof(bool)))
                sqlType = InternalType.BOOLEAN;


            return true;

        }
    }
}

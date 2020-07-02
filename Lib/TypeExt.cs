﻿using System;

namespace DotNetConsoleAppToolkit.Lib
{
    public static class TypeExt
    {
        public static bool InheritsFrom(this Type type,Type ancestorType)
        {
            while (type!=null)
            {
                if (type.BaseType == ancestorType) return true;
                type = type.BaseType;
            }
            return false;
        }

        public static bool HasInterface(this Type type, Type interfaceType)
            => type.GetInterface(interfaceType.FullName) != null;
    }
}

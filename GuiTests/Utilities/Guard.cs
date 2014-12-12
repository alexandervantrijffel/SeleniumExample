using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Structura.GuiTests.Utilities
{
    public static class Guard
    {
        [DebuggerStepThrough]
        public static void IsLargerThan<TException>(decimal argumentValue, decimal mininumValue, string format, params object[] parameters) where TException : Exception
        {
            if (argumentValue <= mininumValue)
            {
                throw GetException<TException>(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<TExpection>(ICollection argumentValue, string format, params object[] parameters) where TExpection : Exception
        {
            if (argumentValue == null || argumentValue.Count == 0)
            {
                throw GetException<TExpection>(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<TExpection, TValue>(Nullable<TValue> value, string format, params object[] parameters)
            where TExpection : Exception
            where TValue : struct
        {
            if (value.HasValue || value.Value.Equals(default(TValue)))
            {
                throw GetException<TExpection>(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<TValue>(Nullable<TValue> value, string argumentName) where TValue : struct
        {
            if (!value.HasValue || value.Value.Equals(default(TValue)))
            {
                throw new ArgumentException(argumentName + " cannot be " + (value.HasValue ? default(TValue).ToString() : "null"), argumentName);
            }
        }

        //[DebuggerStepThrough]
        public static void IsNotNullOrEmpty(Enum value, string argumentName)
        {
            if (value == null || Convert.ToInt32(value) == 0)
            {
                throw new ArgumentException(argumentName + " cannot be " + value, argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNull<TExpection>(object argumentValue, string format, params object[] parameters) where TExpection : Exception
        {
            if (argumentValue == null)
            {
                throw GetException<TExpection>(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNull(object argumentValue, string format, params object[] parameters)
        {
            if (argumentValue == null)
            {
                throw GetException(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty<TException>(string argumentValue, string format, params object[] parameters) where TException : Exception
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw GetException<TException>(format, parameters);
        }

        [DebuggerStepThrough]
        public static void IsTrue<TExpection>(bool condition, string format, params object[] parameters) where TExpection : Exception
        {
            if (!condition)
            {
                throw GetException<TExpection>(format, parameters);
            }
        }

        [DebuggerStepThrough]
        public static void IsTrue(bool condition, string format, params object[] parameters)
        {
            if (!condition)
            {
                throw GetException<InvalidOperationException>(format, parameters);
            }
        }

        private static TExpection GetException<TExpection>(string format, params object[] parameters)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, parameters);
            return (TExpection)Activator.CreateInstance(typeof(TExpection), message);
        }

        private static InvalidOperationException GetException(string format, params object[] parameters)
        {
            return new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, format, parameters));
        }
    }
}

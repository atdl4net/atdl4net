#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Reflection;
using Common.Logging;

namespace Atdl4net.Diagnostics
{
    /// <summary>
    /// Static helper class for generating new instances of exceptions using the provided parameters
    /// </summary>
    public static class ThrowHelper
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Diagnostics");

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, innerException, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), innerException, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="info">The info.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, ExceptionInfo info, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="info">The info.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, ExceptionInfo info, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, innerException, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, ExceptionInfo info, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, ExceptionInfo info, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), innerException, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Wraps the supplied exception in a new exception of the same type as that supplied, in order to get a
        /// decent error message back to the end-user.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">An array of zero or more arguments.</param>
        /// <returns>A new exception of the same type as the supplied exception.</returns>
        public static Exception Rethrow(object source, Exception ex, string format, params object[] args)
        {
            Exception newException = Rethrow(source, ex, string.Format(format, args), new object());

            _log.Error("Exception rethrown by ThrowHelper", newException);

            return newException;
        }

        /// <summary>
        /// Wraps the supplied exception in a new exception of the same type as that supplied, in order to get a
        /// decent error message back to the end-user.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        /// <returns>A new exception of the same type as the supplied exception.</returns>
        public static Exception Rethrow(object source, Exception ex, string format, object arg)
        {
            Exception newException = Rethrow(source, ex, null, format, arg);

            _log.Error("Exception rethrown by ThrowHelper", newException);

            return newException;
        }

        /// <summary>
        /// Wraps the supplied exception in a new exception of the same type as that supplied, in order to get a
        /// decent error message back to the end-user.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        /// <returns>A new exception of the same type as the supplied exception.</returns>
        public static Exception Rethrow(object source, Exception ex, ExceptionInfo info, string format, object arg)
        {
            Type classType = ex.GetType();

            ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

            Exception exception = (Exception)classConstructor.Invoke(new object[] { string.Format(format, arg, ex.Message), ex });

            exception.Source = source.ToString();

            if (info != null)
                info.PopulateExceptionData(exception.Data);

            return exception;
        }

        // Workaround limitation in C# 3.0/4.0 - can't create an instance of a generic type with parameters using new T().
        private static T CreateException<T>(object source, string message, ExceptionInfo info) where T : System.Exception
        {
            Type classType = typeof(T);

            switch (classType.Name)
            {
                // Special treatment is needed for ArgumentOutOfRangeException and ArgumentNullException because the constructor that takes 
                // a single string for these types makes its own message.
                case "ArgumentOutOfRangeException":
                case "ArgumentNullException":
                    {
                        ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(string) });

                        T exception = (T)classConstructor.Invoke(new object[] { "Value", message });

                        exception.Source = source.ToString();

                        if (info != null)
                            info.PopulateExceptionData(exception.Data);

                        return exception;
                    }

                default:
                    {
                        ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string) });

                        T exception = (T)classConstructor.Invoke(new object[] { message });

                        exception.Source = source.ToString();

                        if (info != null)
                            info.PopulateExceptionData(exception.Data);

                        return exception;
                    }
            }
        }

        // Workaround limitation in C# 3.0/4.0 - can't create an instance of a generic type with parameters using new T().
        private static T CreateException<T>(object source, string message, Exception innerException, ExceptionInfo info) where T : System.Exception
        {
            Type classType = typeof(T);

            ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

            T exception = (T)classConstructor.Invoke(new object[] { message, innerException });

            exception.Source = source.ToString();

            if (info != null)
                info.PopulateExceptionData(exception.Data);

            return exception;
        }
    }
}
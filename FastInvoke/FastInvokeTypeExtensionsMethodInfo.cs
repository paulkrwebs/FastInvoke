/*
 *  FastInvoke is a fast way for calling invoke on a method, property, or constructor.
 *  Copyright (C) 2011 Paul Kienan

 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.

 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.

 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FastInvoke
{
    public static class FastInvokeTypeExtensionsMethodInfo
    {
        #region // none type safe

        public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name)
        {
            MethodInfo method = type.GetMethod(name);
            return method.CreateFastInvoke();
        }

        /// <summary>
        /// Used for none type safe method calls that return values
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name, BindingFlags bindingFlags)
        {
            MethodInfo method = type.GetMethod(name, bindingFlags);
            return method.CreateFastInvoke();
        }

        /// <summary>
        /// Used for none type safe method calls that return values
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name, Type[] types)
        {
            MethodInfo method = type.GetMethod(name,types);
            return method.CreateFastInvoke();
        }

        ///// <summary>
        ///// Used for none type safe method calls that return values
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, types, parameterModifiers);
        //    return method.CreateFastInvoke();
        //}

        ///// <summary>
        ///// Used for none type safe method calls that return values
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name, BindingFlags bindFlags, Binder binder, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, bindFlags, binder, types, parameterModifiers);
        //    return method.CreateFastInvoke();
        //}

        /// <summary>
        /// Used for none type safe method calls that return values
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<object, object[], object> GetMethodFastInvoke(this Type type, string name, BindingFlags bindFlags, Binder binder,
            CallingConventions callingConventions, Type[] types, ParameterModifier[] parameterModifiers)
        {
            MethodInfo method = type.GetMethod(name, bindFlags, binder, callingConventions, types, parameterModifiers);
            return method.CreateFastInvoke();
        }

        #endregion

        #region // type safe return

        /// <summary>
        /// Used when the type of the instance is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name)
        {
            MethodInfo method = type.GetMethod(name);
            return method.CreateFastInvoke<TInstance>();
        }

        /// <summary>
        /// Used when the type of the instance is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name, BindingFlags bindingFlags)
        {
            MethodInfo method = type.GetMethod(name, bindingFlags);
            return method.CreateFastInvoke<TInstance>();
        }

        /// <summary>
        /// Used when the type of the instance is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name, Type[] types)
        {
            MethodInfo method = type.GetMethod(name, types);
            return method.CreateFastInvoke<TInstance>();
        }

        ///// <summary>
        ///// Used when the type of the instance is known
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, types, parameterModifiers);
        //    return method.CreateFastInvoke<TInstance>();
        //}

        ///// <summary>
        ///// Used when the type of the instance is known
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name, BindingFlags bindFlags, Binder binder, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, bindFlags, binder, types, parameterModifiers);
        //    return method.CreateFastInvoke<TInstance>();
        //}

        /// <summary>
        /// Used when the type of the instance is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], object> GetMethodFastInvoke<TInstance>(this Type type, string name, BindingFlags bindFlags, Binder binder,
            CallingConventions callingConventions, Type[] types, ParameterModifier[] parameterModifiers)
        {
            MethodInfo method = type.GetMethod(name, bindFlags, binder, callingConventions, types, parameterModifiers);
            return method.CreateFastInvoke<TInstance>();
        }

        #endregion

        #region // type safe

        /// <summary>
        /// Used for type safe method calls
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name)
        {
            MethodInfo method = type.GetMethod(name);
            return method.CreateFastInvoke<TInstance, TReturn>();
        }

        /// <summary>
        /// Used for type safe method calls
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name, BindingFlags bindingFlags)
        {
            MethodInfo method = type.GetMethod(name, bindingFlags);
            return method.CreateFastInvoke<TInstance, TReturn>();
        }

        /// <summary>
        /// Used for type safe method calls
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name, Type[] types)
        {
            MethodInfo method = type.GetMethod(name, types);
            return method.CreateFastInvoke<TInstance, TReturn>();
        }

        ///// <summary>
        ///// Used for type safe method calls
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, types, parameterModifiers);
        //    return method.CreateFastInvoke<TInstance, TReturn>();
        //}

        ///// <summary>
        ///// Used for type safe method calls
        ///// </summary>
        ///// <param name="method"></param>
        ///// <returns></returns>
        //public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name, BindingFlags bindFlags, Binder binder, Type[] types, ParameterModifier[] parameterModifiers)
        //{
        //    MethodInfo method = type.GetMethod(name, bindFlags, binder, types, parameterModifiers);
        //    return method.CreateFastInvoke<TInstance, TReturn>();
        //}

        /// <summary>
        /// Used for type safe method calls
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], TReturn> GetMethodFastInvoke<TInstance, TReturn>(this Type type, string name, BindingFlags bindFlags, Binder binder,
            CallingConventions callingConventions, Type[] types, ParameterModifier[] parameterModifiers)
        {
            MethodInfo method = type.GetMethod(name, bindFlags, binder, callingConventions, types, parameterModifiers);
            return method.CreateFastInvoke<TInstance, TReturn>();
        }

        public static ReferenceParams<TInstance, TReturn> GetMethodFastInvokeAsRefenceCall<TInstance, TReturn>(this Type type, string name, BindingFlags bindFlags, Binder binder,
                CallingConventions callingConventions, Type[] types, ParameterModifier[] parameterModifiers)
        {
            MethodInfo method = type.GetMethod(name, bindFlags, binder, callingConventions, types, parameterModifiers);
            return method.CreateFastInvokeAsRefenceCall<TInstance, TReturn>();
        }

        #endregion
    }
}

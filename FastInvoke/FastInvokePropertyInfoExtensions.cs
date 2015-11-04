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
using System.Linq.Expressions;
using System.Reflection;

namespace FastInvoke
{
    /// <summary>
    /// Research based on http://kohari.org/2009/03/06/fast-late-bound-invocation-with-expression-trees/
    /// </summary>
    public static class FastInvokePropertyInfoExtensions
    {
        #region // set as actions

        /// <summary>
        /// Used for properties where the type of property is not known
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static Action<object, object> SetAsFastInvoke(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object), "arguments");
            MethodInfo method = property.GetSetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsAction
            MethodCallExpression call = Expression.Call(
              Expression.Convert(instanceParameter, property.DeclaringType),
              method,
              Expression.Convert(argumentsParameter, property.PropertyType));

            Expression<Action<object, object>> lambda = Expression.Lambda<Action<object, object>>(
              call,
              instanceParameter,
              argumentsParameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Used for properties where the type of property is not known
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static Action<TType, object> SetAsFastInvoke<TType>(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TType), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object), "arguments");
            MethodInfo method = property.GetSetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsAction
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method,
              Expression.Convert(argumentsParameter, property.PropertyType));

            Expression<Action<TType, object>> lambda = Expression.Lambda<Action<TType, object>>(
              call,
              instanceParameter,
              argumentsParameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Used for properties where the type of property is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Action<TType, TValue> SetAsFastInvoke<TType, TValue>(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TType), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(TValue), "arguments");
            MethodInfo method = property.GetSetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsAction
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method,
              argumentsParameter);

            Expression<Action<TType, TValue>> lambda = Expression.Lambda<Action<TType, TValue>>(
              call,
              instanceParameter,
              argumentsParameter);

            return lambda.Compile();
        }

        #endregion

        #region // get as func

        /// <summary>
        /// Creates a function that gets a property value. Fully type safe and fastest method
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static Func<TInstance, TReturn> GetAsFastInvoke<TInstance, TReturn>(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance), "target");
            MethodInfo method = property.GetGetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsFunc
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method);

            Expression<Func<TInstance, TReturn>> lambda = Expression.Lambda<Func<TInstance, TReturn>>(
              call,
              instanceParameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Creates a function that gets a property value when the type of the value is not known
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static Func<TInstance, object> GetAsFastInvoke<TInstance>(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance), "target");
            MethodInfo method = property.GetGetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsFunc
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method);

            Expression<Func<TInstance, object>> lambda = Expression.Lambda<Func<TInstance, object>>(
              Expression.Convert(call,typeof(object)),
              instanceParameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Creates a function that gets a property value when the type of the value, and the instance type is not known
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static Func<object, object> GetAsFastInvoke(this PropertyInfo property)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");
            MethodInfo method = property.GetGetMethod();

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsFunc
            MethodCallExpression call = Expression.Call(
              Expression.Convert(instanceParameter, method.DeclaringType),
              method);

            Expression<Func<object, object>> lambda = Expression.Lambda<Func<object, object>>(
              Expression.Convert(call, typeof(object)),
              instanceParameter);

            return lambda.Compile();
        }

        #endregion
    }
}

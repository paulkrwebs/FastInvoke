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
    public delegate TReturn ReferenceParams<TInstance, TReturn>(TInstance instance, ref object[] arguments);

    /// <summary>
    /// Research based on http://kohari.org/2009/03/06/fast-late-bound-invocation-with-expression-trees/
    /// </summary>
    public static class FastInvokeMethodInfoExtensions
    {
        #region // create as func

        public static ReferenceParams<TInstance, TReturn> CreateFastInvokeAsRefenceCall<TInstance, TReturn>(this MethodInfo method)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]).MakeByRefType(), "arguments");
            Expression<ReferenceParams<TInstance, TReturn>> lambda = null;

            MethodCallExpression call = Expression.Call(
              Expression.Convert(instanceParameter, method.DeclaringType),
              method,
              FastInvokeUtilities.CreateRefParameterExpressions(method, argumentsParameter));

            if (method.ReturnType == typeof(void))
            {
                lambda = Expression.Lambda<ReferenceParams<TInstance, TReturn>>(
                  Expression.Block(call, Expression.Default(typeof(TInstance))),
                  instanceParameter,
                  argumentsParameter);
            }
            else
            {
                lambda = Expression.Lambda<ReferenceParams<TInstance, TReturn>>(
                      call,
                      instanceParameter,
                      argumentsParameter);
            }

            return lambda.Compile();
        }

        /// <summary>
        /// Used for none type safe method calls that return values
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<object, object[], object> CreateFastInvoke(this MethodInfo method)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");
            Expression<Func<object, object[], object>> lambda = null;
            MethodCallExpression call = Expression.Call(
              Expression.Convert(instanceParameter, method.DeclaringType),
              method,
              FastInvokeUtilities.CreateParameterExpressions(method, argumentsParameter));

            if (method.ReturnType == typeof(void))
            {
                lambda = Expression.Lambda<Func<object, object[], object>>(
                  Expression.Block(call, Expression.Default(typeof(object))),
                  instanceParameter,
                  argumentsParameter);
            }
            else
            {
                lambda = Expression.Lambda<Func<object, object[], object>>(
                      call,
                      instanceParameter,
                      argumentsParameter);
            }

            return lambda.Compile();
        }

        /// <summary>
        /// Used when the type of the instance is known
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], object> CreateFastInvoke<TInstance>(this MethodInfo method)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");
            Expression<Func<TInstance, object[], object>> lambda = null;

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsFunc
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method,
              FastInvokeUtilities.CreateParameterExpressions(method, argumentsParameter));

            if (method.ReturnType == typeof(void))
            {
                // if method is void returns default value of object null
                lambda = Expression.Lambda<Func<TInstance, object[], object>>(
                  Expression.Block(call, Expression.Default(typeof(object))),
                  instanceParameter,
                  argumentsParameter);
            }
            else
            {
                // if method is void returns default value of object null
                lambda = Expression.Lambda<Func<TInstance, object[], object>>(
                  call,
                  instanceParameter,
                  argumentsParameter);
            }

            return lambda.Compile();
        }

        /// <summary>
        /// Used for type safe method calls. Fastest way to invoke
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TInstance, object[], TReturn> CreateFastInvoke<TInstance, TReturn>(this MethodInfo method)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(TInstance), "target");
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

            // because we know the type we don't have to convert the instanceParameter like none type safe CreateAsFunc
            MethodCallExpression call = Expression.Call(
              instanceParameter,
              method,
              FastInvokeUtilities.CreateParameterExpressions(method, argumentsParameter));

            Expression<Func<TInstance, object[], TReturn>> lambda = Expression.Lambda<Func<TInstance, object[], TReturn>>(
              call,
              instanceParameter,
              argumentsParameter);

            return lambda.Compile();
        }

        #endregion
    }
}

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
    public static class FastInvokeConstructorInfoExtensions
    {
        #region // create as func

        /// <summary>
        /// Used for constructors that the type is not known
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctor">The ctor.</param>
        /// <returns></returns>
        public static Func<object[], object> CreateAsFastInvoke(this ConstructorInfo constructorInfo)
        {
            return CreateAsFastInvoke<object>(constructorInfo);
        }

        /// <summary>
        /// Used for constructors where the object type is known
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctor">The ctor.</param>
        /// <returns></returns>
        public static Func<object[], TType> CreateAsFastInvoke<TType>(this ConstructorInfo constructorInfo)
        {
            ParameterExpression argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

            NewExpression call = Expression.New(
              constructorInfo,
              FastInvokeUtilities.CreateParameterExpressions(constructorInfo, argumentsParameter));

            Expression<Func<object[], TType>> lambda = Expression.Lambda<Func<object[], TType>>(
              call,
              argumentsParameter);

            return lambda.Compile();
        }

        #endregion
    }
}

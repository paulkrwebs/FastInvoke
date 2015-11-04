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
using System.Linq.Expressions;

namespace FastInvoke
{
    public class FastInvokeUtilities
    {
        #region // helper methods

        internal static Expression[] CreateParameterExpressions(MethodBase method, Expression argumentsParameter)
        {
            return method.GetParameters().Select((parameter, index) =>
              Expression.Convert(
                Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)), parameter.ParameterType)).ToArray();
        }

        internal static Expression[] CreateRefParameterExpressions(MethodBase method, Expression argumentsParameter)
        {
            // http://stackoverflow.com/questions/3146317/create-expression-to-invoke-method-with-out-parameter
            // for reference parameters need to call MakeByRefType() somehow???
            //return method.GetParameters().Select((parameter, index) =>
            //  ).ToArray();

            ParameterInfo[] parameters = method.GetParameters();
            Expression[] toReturn = new Expression[parameters.Count()];

            for (int i=0;i<parameters.Length; i++)
            {
                ParameterInfo pi = parameters[i];
                if (pi.ParameterType.IsByRef)
                { 
                    // doesn't work somehow I need to affect the argumentsParemter expression else I get a not in scope exception!
                    toReturn[i] = Expression.Convert(Expression.ArrayIndex(argumentsParameter, Expression.Constant(i)), pi.ParameterType.GetElementType());
                }
                else
                {
                    toReturn[i] = Expression.Convert(Expression.ArrayIndex(argumentsParameter, Expression.Constant(i)), pi.ParameterType);
                }
            }

            return toReturn;
        }

        #endregion
    }
}

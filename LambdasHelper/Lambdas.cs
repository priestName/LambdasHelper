using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LambdasHelper
{
    public static class Lambdas
    {
        static void Main()
        {

        }
        #region Lambda拼接
        /// <summary>
        /// Lambda And拼接
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="first">Lambda 表达式1</param>
        /// <param name="second">Lambda 表达式2</param>
        /// <returns>拼接完成的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.LambdasAnd(second);
        }
        /// <summary>
        /// Lambda Or拼接
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="first">Lambda 表达式1</param>
        /// <param name="second">Lambda 表达式2</param>
        /// <returns>拼接完成的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.LambdasOr(second);
        }
        #endregion
        #region 字符串形式转为Lambda表达式形式
        /// <summary>
        /// 将源字符串分析为 Lambda 表达式.
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="LambdaText">用于分析的源字符串</param>
        /// <param name="ns">分析过程中可能用到的命名空间列表</param>
        /// <returns>Lambda 表达式</returns>
        public static Expression<Func<T, bool>> ToLambdas<T>(this string LambdaText, params string[] ns)
        {
            if (LambdaText.Contains("=>"))
            {
                LambdaText = LambdaText.Contains("'")? LambdaText.Replace("'","\""): LambdaText;
                return StringToLambda.LambdaParser.Parse<T>(LambdaText, ns);
            }
            return s => true;
        }
        #endregion
        #region 字符串形式转为委托表达式形式
        /// <summary>
        /// 将源字符串分析为委托.
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="source">用于分析的源字符串.</param>
        /// <param name="ns">分析过程中可能用到的命名空间列表.</param>
        /// <returns>分析委托.</returns>
        public static Func<T, bool> ToDelegate<T>(this string LambdaText, params string[] ns)
        {
            return StringToLambda.LambdaParser.Compile<T>(LambdaText, ns);
        }
        /// <summary>
        /// 将源字符串分析为委托.
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="source">用于分析的源字符串.</param>
        /// <param name="assemblies">可能用到的程序集列表.</param>
        /// <param name="ns">分析过程中可能用到的命名空间列表.</param>
        /// <returns>分析委托.</returns>
        public static Func<T, bool> ToDelegate<T>(this string LambdaText, Assembly[] assemblies, params string[] ns)
        {
            return StringToLambda.LambdaParser.Compile<T>(LambdaText, assemblies, ns);
        }
        #endregion
    }
}

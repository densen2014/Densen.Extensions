// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AME
{
    /// <summary>
    /// 动态生成比较lamda表达式帮助类
    /// </summary>
    public class LamdaHelper
    {
        /// <summary>
        /// 创建lambda表达式：p.propertyName
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="TKey">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <returns></returns>
        public static MemberExpression GetExpression<T, TKey>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            return Expression.Property(parameter, propertyName);
        }

        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return p => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return p => false;
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="TKey">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, propertyName), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateEqual<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateNotEqual<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThan<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>  propertyValue > p.propertyName 
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThan<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThanOrEqual<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>propertyValue >= p.propertyName 
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <typeparam name="S">参数类型</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThanOrEqual<T, S>(string propertyName, S propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(S));//创建常数
            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetContains<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <typeparam name="T">对象名称（类名）</typeparam>
        /// <param name="propertyName">字段名称（数据库中字段名称）</param>
        /// <param name="propertyValue">数据值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetNotContains<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }
    }
}

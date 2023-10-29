// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AME;

/// <summary>
/// IQueryable 扩展方法
/// </summary>
public static class IQueryableExtensions
{

    /// <summary>
    /// 根据指定属性名称对序列进行排序
    /// </summary>
    /// <typeparam name="TSource">source中的元素的类型</typeparam>
    /// <param name="source">一个要排序的值序列</param>
    /// <param name="property">属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns></returns>
    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending) where TSource : class
    {
        ParameterExpression param = Expression.Parameter(typeof(TSource), "c");
        PropertyInfo pi = typeof(TSource).GetProperty(property);
        MemberExpression selector = Expression.MakeMemberAccess(param, pi);
        LambdaExpression le = Expression.Lambda(selector, param);
        string methodName = (descending) ? "OrderByDescending" : "OrderBy";
        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TSource), pi.PropertyType }, source.Expression, le);
        return source.Provider.CreateQuery<TSource>(resultExp);
    }




    /// <summary>
    /// 并行查找DataTable单元格
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static IEnumerable<DataRow> DataTableParallelForEach<T>(DataTable dt)
    {
        IEnumerable<DataRow> temporaryEnumerable = dt.Rows.Cast<DataRow>();
        return temporaryEnumerable;
    }


    /// <summary>
    /// 转换 List T - BindingList T 转 List T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="property">排序字段</param>
    /// <param name="ascSort">顺序/逆序</param>
    /// <param name="itemList">数据 List T , 为空则使用 bindingList</param>
    /// <param name="bindingList">数据 BindingList T </param>
    /// <returns></returns>
    public static List<T> CustomSort<T>(string property, bool ascSort, List<T> itemList = null, BindingList<T> bindingList = null) where T : class
    {
        try
        {
            var source = (itemList ?? bindingList.ToList()).AsQueryable();
            itemList = source.OrderBy(property, !ascSort).ToList();
        }
        catch
        {
        }
        finally
        {
        }
        return itemList;
    }

    /// <summary>
    /// 自定义排序 , BindingList T 转 List T 并按指定字段排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="property">排序字段</param>
    /// <param name="ascSort">顺序/逆序</param>
    /// <param name="bindingList">数据 BindingList T</param>
    /// <returns></returns>
    public static List<T> CustomSort<T>(string property, bool ascSort, BindingList<T> bindingList) where T : class
    {
        List<T> itemList;
        try
        {
            var source = bindingList.ToList().AsQueryable();
            itemList = source.OrderBy(property, !ascSort).ToList();
            return itemList;
        }
        catch
        {
            return bindingList.ToList();
        }
        finally
        {
        }
    }

}

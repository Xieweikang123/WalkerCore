using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace WalkerCore
{
    public class Common
    {
        /// <summary>
        /// 查询拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="ivm"></param>
        /// <param name="ovm"></param>
        public static void QueryJoin<T>(IQueryable<T> query, QueryDataInputVM ivm, ref QueryDataOutputVM ovm)
        {
            //总条数
            ovm.total = query.Count();
            if (ovm.total <= 0)
            {
                return;
            }

            //排序
            if (!string.IsNullOrWhiteSpace(ivm.sort))
            {
                query = QueryableTo.OrderBy(query, ivm.sort, ivm.order);
            }

            //分页
            if (ivm.pagination == 1)
            {
                query = query.Skip((ivm.page - 1) * ivm.rows).Take(ivm.rows);
            }

            //数据
            var data = query.ToList();
            ovm.data = data;
        }
        /// <summary>
        /// 查询支持处理
        /// </summary>
        public class QueryableTo
        {
            /// <summary>
            /// 排序
            /// </summary>
            /// <param name="query"></param>
            /// <param name="sorts">排序字段，支持多个，逗号分割</param>
            /// <param name="orders">排序类型，支持多个，逗号分割</param>
            public static IQueryable<T> OrderBy<T>(IQueryable<T> query, string sorts, string orders = "asc")
            {
                var listSort = sorts.Split(',').ToList();
                var listOrder = orders.Split(',').ToList();

                //倒叙
                for (int i = listSort.Count - 1; i >= 0; i--)
                {
                    var sort = listSort[i];
                    var order = i < listOrder.Count ? listOrder[i] : "asc";

                    var property = typeof(T).GetProperties().Where(x => x.Name.ToLower() == sort.ToLower()).First();

                    ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
                    MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    LambdaExpression lambda = Expression.Lambda(propertyAccess, parameter);

                    if (order.ToLower() == "desc")
                    {
                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(lambda));
                        query = query.Provider.CreateQuery<T>(resultExp);
                    }
                    else
                    {
                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(lambda));
                        query = query.Provider.CreateQuery<T>(resultExp);
                    }
                }

                return query;
            }
        }
    }
}

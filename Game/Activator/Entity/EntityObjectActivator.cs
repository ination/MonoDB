using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

using Project.Common.DB;

namespace Project.Game.DataBase
{
    internal class EntityObjectActivator
    {
        public static TEntity CreateEntity<TEntity>(PCResultSet rs)
        {
            if (null == rs)
            {
                return default(TEntity);
            }
            object retEntity = Activator.CreateInstance(typeof(TEntity));
            if (null == retEntity)
            {
                return default(TEntity);
            }
            Type entityType = retEntity.GetType();
            PropertyInfo[] properties = entityType.GetProperties();
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo pInfo = properties[i];
                object columnValue = rs.ValueForColumn(pInfo.Name);
                if (null != columnValue)
                {
                    if (!pInfo.PropertyType.IsGenericType)
                    {
                        //非泛型
                        pInfo.SetValue(retEntity, Convert.ChangeType(columnValue, pInfo.PropertyType), null);
                    }
                    else
                    {
                        //泛型Nullable<>
                        Type genericTypeDefinition = pInfo.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            pInfo.SetValue(retEntity, Convert.ChangeType(columnValue, Nullable.GetUnderlyingType(pInfo.PropertyType)), null);
                        }
                    }
                }
            }
            return (TEntity)retEntity;
        }
    }
}
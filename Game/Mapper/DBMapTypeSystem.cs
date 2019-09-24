using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Project.Game.DataBase
{
    public enum DBType
    {
        INVAILD,

        TEXT,
        INTEGER,
        REAL,
        BLOB,
    }

    partial class DBMapTypeSystem
    {
        static private Dictionary<DBType, string> m_DBTypeStringMap;

        static private Dictionary<Type, DBType> m_DBTypeMap;
        static DBMapTypeSystem()
        {
            m_DBTypeStringMap = new Dictionary<DBType, string>
            {
                {DBType.TEXT, "TEXT"},
                {DBType.INTEGER, "INTEGER"},
                {DBType.REAL, "REAL"},
                {DBType.BLOB, "BLOB"}
            };

            SetItemType(typeof(char), DBType.TEXT);
            SetItemType(typeof(string), DBType.TEXT);

            SetItemType(typeof(bool), DBType.INTEGER);
            SetItemType(typeof(byte), DBType.INTEGER);

            SetItemType(typeof(int), DBType.INTEGER);
            SetItemType(typeof(short), DBType.INTEGER);
            SetItemType(typeof(long), DBType.INTEGER);

            SetItemType(typeof(uint), DBType.INTEGER);
            SetItemType(typeof(ushort), DBType.INTEGER);
            SetItemType(typeof(ulong), DBType.INTEGER);

            SetItemType(typeof(Int16), DBType.INTEGER);
            SetItemType(typeof(Int32), DBType.INTEGER);
            SetItemType(typeof(Int64), DBType.INTEGER);

            SetItemType(typeof(UInt16), DBType.INTEGER);
            SetItemType(typeof(UInt32), DBType.INTEGER);
            SetItemType(typeof(UInt64), DBType.INTEGER);

            SetItemType(typeof(float), DBType.REAL);
            SetItemType(typeof(double), DBType.REAL);
            SetItemType(typeof(decimal), DBType.REAL);

        }

        private static void SetItemType(Type type, DBType dbType)
        {
            if (null == type)
            {
                return;
            }
            if (null == m_DBTypeMap)
            {
                m_DBTypeMap = new Dictionary<Type, DBType>();
            }
            if (m_DBTypeMap.ContainsKey(type))
            {
                return;
            }

            m_DBTypeMap[type] = dbType;
        }

        public static DBType MapDBType(Type type)
        {
            if (null == type)
            {
                return DBType.INVAILD;
            }
            DBType dbType = m_DBTypeMap[type];
            return dbType;
        }

        public static string MapDBTypeString(Type type)
        {
            if (null == type){
                return null;
            }
            DBType dbType = m_DBTypeMap[type];
            if (dbType == DBType.INVAILD)
            {
                return null;
            }
            string dbTypeStr = m_DBTypeStringMap[dbType];
            return dbTypeStr;
        }

        public static void CheckDBTypeMapped(Type type, string param = null)
        {
            bool mapped = false;
            if (null != type)
            {
                mapped = m_DBTypeMap.ContainsKey(type);
            }

            if (!mapped)
            {
                throw new ArgumentNullException(param);
            }
        }
    }
}


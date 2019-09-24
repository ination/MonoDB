using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

using Project.Common.Utils;

namespace Project.Game.DataBase
{
    internal class DBTable
    {
        private Type m_type;

        protected string m_tableName;
        public string TableName
        {
            get { return m_tableName; }
        }

        protected Dictionary<string, DBColumn> m_Columns;
        public Dictionary<string, DBColumn> Columns
        {
            get { return m_Columns; }
        }

        protected List<string> m_primaryKeys;
        public List<string> PrimaryKeys
        {
            get { return m_primaryKeys; }
        }

        protected List<string> m_notMapKeys;
        public List<string> NotMapKeys
        {
            get { return m_notMapKeys; }
        }

        protected List<string> m_addNewKeys;
        public List<string> AddNewKeys
        {
            get { return m_addNewKeys; }
        }

        public DBTable(Type type)
        {
            m_type = type;
            m_Columns = new Dictionary<string, DBColumn>();

            m_tableName = this.GetTableName();
        }

        public void InitColumnByType()
        {
            PropertyInfo[] properties = m_type.GetProperties();
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo pInfo = properties[i];
                DBMapTypeSystem.CheckDBTypeMapped(pInfo.PropertyType);

                DBColumn column = new DBColumn();
                column.Init(pInfo);

                this.CheckAttributeKeys(column);
            }
        }

        public void InitColumnByEntity<TEntity>(TEntity entity)
        {
            PropertyInfo[] properties = m_type.GetProperties();
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo pInfo = properties[i];
                DBMapTypeSystem.CheckDBTypeMapped(pInfo.PropertyType);

                DBColumn column = new DBColumn();
                column.Init(pInfo, entity);

                this.CheckAttributeKeys(column);
            }
        }

        private string GetTableName()
        {
            Tools.CheckNull(m_type);

            string retName = null;

            TableAttribute tableAttr = Attribute.GetCustomAttribute(m_type, typeof(TableAttribute)) as TableAttribute;
            if (null != tableAttr)
            {
                string tableName = tableAttr.Name;
                if (null != tableName && tableName.Length > 0)
                {
                    retName = tableName;
                }
            }
            return retName;
        }

        private void CheckAttributeKeys(DBColumn column)
        {
            if (null == column)
            {
                return;
            }
            string columnName = column.Name;
            if (column.IsPrimaryKey)
            {
                if (null == m_primaryKeys)
                {
                    m_primaryKeys = new List<string>();
                }
                if (!m_primaryKeys.Contains(columnName))
                {
                    m_primaryKeys.Add(columnName);
                }
            }
            if (column.IsAddNew){
                if (null == m_addNewKeys)
                {
                    m_addNewKeys = new List<string>();
                }
                if (!m_addNewKeys.Contains(columnName))
                {
                    m_addNewKeys.Add(columnName);
                }
            }
            if (column.IsNotMapped)
            {
                if (null == m_notMapKeys)
                {
                    m_notMapKeys = new List<string>();
                }
                if (!m_notMapKeys.Contains(columnName))
                {
                    m_notMapKeys.Add(columnName);
                }
            }
            else
            {
                m_Columns[columnName] = column;
            }

        }
    }
}

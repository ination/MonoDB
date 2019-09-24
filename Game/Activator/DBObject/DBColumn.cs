using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

using Project.Common.Utils;     

namespace Project.Game.DataBase
{
    internal class DBColumn
    {
        private PropertyInfo m_property;

        protected string m_name;
        public string Name
        {
            get { return m_name; }
        }

        protected object m_value;
        public object Value
        {
            get { return m_value; }
        }

        protected Type m_valueType;
        public Type ValueType
        {
            get { return m_valueType; }
        }

        protected bool m_isPrimaryKey;
        public bool IsPrimaryKey
        {
            get { return m_isPrimaryKey; }
        }

        protected bool m_isAutoIncrement;
        public bool IsAutoIncrement
        {
            get { return m_isAutoIncrement; }
        }

        protected bool m_isNotMapped;
        public bool IsNotMapped
        {
            get { return m_isNotMapped; }
        }

        protected bool m_isAddNew;
        public bool IsAddNew
        {
            get { return m_isAddNew; }
        }

        public DBColumn()
        {

        }

        public void Init(PropertyInfo property, object obj = null)
        {
            Tools.CheckNull(property);

            m_property = property;

            ColumnAttribute pAttribute = Attribute.GetCustomAttribute(property, typeof(ColumnAttribute)) as ColumnAttribute;

            if (null != pAttribute)
            {
                m_name = pAttribute.Name;

                m_isPrimaryKey = pAttribute.PrimaryKey;
                m_isAutoIncrement = pAttribute.AutoIncrement;
                m_isNotMapped = pAttribute.NotMapped;
                m_isAddNew = pAttribute.AddNew;
            }

            if (null == m_name || m_name.Length < 1)
            {
                m_name = property.Name;
            }

            if (null != obj)
            {
                m_value = property.GetValue(obj,null);
            }

            m_valueType = property.PropertyType;

        }

    }
}

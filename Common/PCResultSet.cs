using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

namespace Project.Common.DB
{
    public class PCResultSet
    {
        private SqliteDataReader m_dataReader;
        private int m_filedCount;

        private PCResultSet()
        {
            m_dataReader = null;
            m_filedCount = 0;
        }

        internal PCResultSet(SqliteDataReader reader)
        {
            m_dataReader = reader;
            m_filedCount = 0;
            if (null != reader){
                m_filedCount = m_dataReader.FieldCount;
            }
        }

        public bool IsClose()
        {
            if (null == m_dataReader)
            {
                return false;
            }
            return m_dataReader.IsClosed;
        }

        public void Close()
        {
            if (null == m_dataReader){
                return;
            }
            m_dataReader.Close();
        }

        public bool Next()
        {
            if (null == m_dataReader)
            {
                return false;
            }
            return m_dataReader.Read();
        }

        public int ColumnCount()
        {
            if (null == m_dataReader)
            {
                return 0;
            }
            return m_dataReader.FieldCount;
        }

        public int ColumnValues(object[] values)
        {
            if (null == m_dataReader)
            {
                return 0;
            }
            return m_dataReader.GetValues(values);
        }

        public int ColumnIndexForName(string columnName)
        {
            if (null == m_dataReader){
                return -1;
            }
            if (columnName.Length < 1){
                return -1;
            }
            return m_dataReader.GetOrdinal(columnName);
        }


        public string ColumnNameForIndex(int columnIdx)
        {
            if (null == m_dataReader){
                return null;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return null;
            }
            return m_dataReader.GetName(columnIdx);
        }

        public int IntForColumn(string columnName)
        {
            int ret = 0;
            if (null == m_dataReader){
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0){
                ret = m_dataReader.GetInt32(columnIdx);
            }

            return ret;
        }

        public int IntForColumnIndex(int columnIdx)
        {
            int ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetInt32(columnIdx);
            return ret;
        }

        public short ShortForColumn(string columnName)
        {
            short ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetInt16(columnIdx);
            }

            return ret;
        }


        public short ShortForColumnIndex(int columnIdx)
        {
            short ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetInt16(columnIdx);
            return ret;
        }

        public long LongForColumn(string columnName)
        {
            long ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetInt64(columnIdx);
            }

            return ret;
        }

        public long LongForColumnIndex(int columnIdx)
        {
            long ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetInt64(columnIdx);
            return ret;
        }

        public bool BoolForColumn(string columnName)
        {
            bool ret = false;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetBoolean(columnIdx);
            }

            return ret;
        }

        public bool BoolForColumnIndex(int columnIdx)
        {
            bool ret = false;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetBoolean(columnIdx);
            return ret;
        }

        public float floatForColumn(string columnName)
        {
            float ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetFloat(columnIdx);
            }

            return ret;
        }

        public float FloatForColumnIndex(int columnIdx)
        {
            float ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetFloat(columnIdx);
            return ret;
        }

        public double DoubleForColumn(string columnName)
        {
            double ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetDouble(columnIdx);
            }

            return ret;
        }

        public double DoubleForColumnIndex(int columnIdx)
        {
            double ret = 0;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetDouble(columnIdx);
            return ret;
        }

        public string StringForColumn(string columnName)
        {
            string ret = null;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetString(columnIdx);
            }

            return ret;
        }

        public string StringForColumnIndex(int columnIdx)
        {
            string ret = null;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetString(columnIdx);
            return ret;
        }

        public object ValueForColumn(string columnName)
        {
            object ret = null;
            if (null == m_dataReader)
            {
                return ret;
            }
            int columnIdx = this.ColumnIndexForName(columnName);
            if (columnIdx >= 0)
            {
                ret = m_dataReader.GetValue(columnIdx);
            }

            return ret;
        }

        public object ValueForColumnIndex(int columnIdx)
        {
            object ret = null;
            if (null == m_dataReader)
            {
                return ret;
            }
            if (columnIdx < 0 || columnIdx >= m_filedCount)
            {
                return ret;
            }
            ret = m_dataReader.GetValue(columnIdx);
            return ret;
        }



        //public byte[] byteForColumn(string columnName)
        //{
        //    byte[] ret = null;
        //    if (null == m_dataReader)
        //    {
        //        return ret;
        //    }
        //    int columnIdx = this.columnIndexForName(columnName);
        //    if (columnIdx >= 0)
        //    {
        //        ret = m_dataReader.GetBytes(columnIdx);
        //    }

        //    return ret;
        //}

        //public byte[] byteForColumnIndex(int columnIdx)
        //{
        //    return null;
        //}
    }
}



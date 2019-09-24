using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;

namespace Project.Common.DB
{
    public class PCDatabase
    {
        private string m_dbPath;
        private SqliteConnection m_dbConn;

        private bool m_isOpen;
        private bool m_isInTransaction;

        private PCDatabase ()
        {

        }

        public PCDatabase (string dbPath)
        {
            m_dbPath = dbPath;
        }

        public bool IsOpen()
        {
            return m_isOpen;
        }

        public bool Open()
        {
            m_isOpen = false;
            if (null == m_dbPath || m_dbPath.Length < 1){
                return m_isOpen;
            }
            try
            {
                //构造数据库连接
                m_dbConn = new SqliteConnection(m_dbPath);
                //打开数据库
                m_dbConn.Open();
                m_isOpen = true;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                m_isOpen = false;
            }
            return m_isOpen;
        }

        public void Close()
        {
            if (null == m_dbConn){
                return;
            }

            try
            {
                m_dbConn.Close();
                m_isOpen = false;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

        }

        public PCResultSet ExecuteQuery(string sql)
        {
            SqliteDataReader reader = this._PrivateExecuteSql(sql);
            if (null ==  reader){
                return null;
            } 
            PCResultSet set = new PCResultSet(reader);

            return set;
        }

        public bool ExecuteUpdate(string sql)
        {
            SqliteDataReader reader = this._PrivateExecuteSql(sql);
            if (null == reader)
            {
                return false;
            }
            return true;
        }


        public bool Rollback()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("rollback transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = false;
            }
    
            return b;
        }

        public bool Commit()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("commit transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = false;
            }
    
            return b;
        }

        public bool BeginTransaction()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("begin exclusive transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = true;
            }
    
            return b;
        }

        public bool BeginDeferredTransaction()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("begin deferred transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = true;
            }
    
            return b;
        }

        public bool BeginImmediateTransaction()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("begin immediate transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = true;
            }
    
            return b;
        }

        public bool BeginExclusiveTransaction()
        {
            bool b = false;

            SqliteDataReader reader = this._PrivateExecuteSql("begin exclusive transaction");
            if (null != reader)
            {
                b = true;
            }
            if (b) {
                m_isInTransaction = true;
            }
    
            return b;
        }

        public bool IsInTransaction()
        {
            return m_isInTransaction;
        }

        private SqliteDataReader _PrivateExecuteSql(string sql)
        {
            if (null == sql || sql.Length < 1)
            {
                return null;
            }
            if (null == m_dbConn)
            {
                return null;
            }

            SqliteCommand command = null;
            try
            {
                command = m_dbConn.CreateCommand();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            if (null == command)
            {
                return null;
            }
            command.CommandText = sql;


            SqliteDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return reader;
        }
    }

}


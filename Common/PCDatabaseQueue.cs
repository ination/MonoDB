using UnityEngine;
using System.Collections;
using System.Threading;

namespace Project.Common.DB
{
    public class PCDatabaseQueue
    {
        private enum TransactionType
        {
            Exclusive,
            Deferred,
            Immediate,
        }

        private string m_dbPath;
        private PCDatabase m_db;
        private Queue m_threadQueue;

        public PCDatabaseQueue()
        {
            this.Initionlize();
        }

        public PCDatabaseQueue(string dbPath)
        {
            m_dbPath = dbPath;
            this.Initionlize();
        }


        public void Close()
        {
            if (null == m_db){
                return;
            }
            m_db.Close();
        }

        public void InDataBase(DataBaseCallback callback)
        {
            PCDatabase database = this.GetDatabase();
            callback(database);
        }

        public void AsyncInDataBase(DataBaseCallback callback)
        {
            PCDatabase database = this.GetDatabase();
            callback(database);
        }

        public void InTransaction(TransactionCallback callback)
        {
            Transaction(TransactionType.Exclusive, callback);
        }

        public void InDeferredTransaction(TransactionCallback callback)
        {
            Transaction(TransactionType.Deferred, callback);
        }

        public void InExclusiveTransaction(TransactionCallback callback)
        {
            Transaction(TransactionType.Exclusive, callback);
        }

        public void InImmediateTransaction(TransactionCallback callback)
        {
            Transaction(TransactionType.Immediate, callback);
        }

        private void Transaction(TransactionType type, TransactionCallback callback)
        {
            PCDatabase database = this.GetDatabase();
            bool shouldRollback = false;

            switch (type)
            {
                case TransactionType.Exclusive:
                    database.BeginTransaction();
                    break;
                case TransactionType.Deferred:
                    database.BeginDeferredTransaction();
                    break;
                case TransactionType.Immediate:
                    database.BeginImmediateTransaction();
                    break;
                default:
                    break;
            }
            callback(database, ref shouldRollback);

        
            if (shouldRollback) {
                database.Rollback();
            }
            else {
                database.Commit();
            }
        }

        private void Initionlize()
        {
            if (null == m_dbPath || m_dbPath.Length < 1){
                m_dbPath = "data source =" + Application.persistentDataPath + "/database.fmdb";
            }
            m_threadQueue = new Queue(1);
            m_db = this.GetDatabase();
        }

        private PCDatabase GetDatabase()
        {
            if (null == m_db){
                m_db = new PCDatabase(m_dbPath);
            }
            if (!m_db.IsOpen())
            {
                bool success = m_db.Open();
                if (!success)
                {
                    Debug.Log(@"Could not create database queue for path " + m_dbPath);
                }
            }
            return m_db;
        }
        
    }
}


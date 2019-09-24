using UnityEngine;
using System.Collections;

using Project.Common.DB;
using Project.Common.Singleton;

namespace Project.Game.DataBase
{
    internal class GameDBQueue : Singleton<GameDBQueue>, IGameDBQueue
    {
        private PCDatabaseQueue m_dbQueue;
        public PCDatabaseQueue DBQueue 
        {
            get { return m_dbQueue; }
        }

        private string m_dbPath;
        public string DBPath 
        {
            get { return m_dbPath; }
        }

        private GameDBStore m_dbStore;
        private bool m_isInit = false;

        protected GameDBQueue()
        {

        }

        public void InitDataBase(string dbPath)
        {
            if (m_isInit){
                return;
            }
            m_dbQueue = new PCDatabaseQueue(dbPath);

            m_isInit = true;
        }

        public void CloseDataBase()
        {
            if (null== m_dbQueue)
            {
                return;
            }
            m_dbQueue.Close();
            m_isInit = false;
        }

        internal IGameDBStore GetStore()
        {
            if (null == m_dbStore)
            {
                m_dbStore = new GameDBStore(m_dbQueue);
            }
            return m_dbStore;
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Project.Common.DB;


namespace Project.Game.DataBase
{
    internal class GameDBStore : IGameDBStore
    {
        protected PCDatabaseQueue m_dbQueue;

        public GameDBStore(PCDatabaseQueue dbQueue)
        {
            m_dbQueue = dbQueue;
        }

        public bool TableExists(string tableName)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) => 
            {
                bRet = database.TableExists(tableName);
            });
            return bRet;
        }

        public bool EraseTable(string talbleName)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) => 
            {
                bRet = database.DeleteTable(talbleName);
            });
            return bRet;
        }

        public bool RemoveTable(string talbleName)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) => 
            {
                bRet = database.DropTable(talbleName);
            });
            return bRet;
        }

        public bool ExecuteUpdateSql(string sql)
        {
            bool bRet = false;
            if (null == sql || sql.Length < 1)
            {
                return bRet;
            }
            if (null == m_dbQueue)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) =>
            {
                bRet = database.ExecuteUpdate(sql);
            });
            return bRet;
        }

        public bool TableExists<TEntity>()
        {
            bool bRet = false;
            DBTable table = DBObjectActivator.CreateTable<TEntity>();
            if (null == table)
            {
                return bRet;
            }
            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return bRet;
            }
            return this.TableExists(tableName);
        }

        public bool EraseTable<TEntity>()
        {
            bool bRet = false;
            DBTable table = DBObjectActivator.CreateTable<TEntity>();
            if (null == table)
            {
                return bRet;
            }
            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return bRet;
            }
            return this.EraseTable(tableName);
        }

        public bool RemoveTable<TEntity>()
        {
            bool bRet = false;
            DBTable table = DBObjectActivator.CreateTable<TEntity>();
            if (null == table)
            {
                return bRet;
            }
            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return bRet;
            }
            return this.RemoveTable(tableName);
        }

        public bool CreateTable<TEntity>()
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            DBTable table = DBObjectActivator.CreateTableColumns<TEntity>();
            if (null == table)
            {
                return bRet;
            }
            SqlBuilder builder = new SqlBuilder();
            string sql = builder.BuildCreateSql(table);
            if (null == sql || sql.Length < 1)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) =>
            {
                bRet = database.ExecuteUpdate(sql);
            });
            return bRet;
        }

        public bool AddEntity<TEntity>(TEntity entity)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            DBTable table = DBObjectActivator.CreateTableColumns<TEntity>(entity);
            if (null == table)
            {
                return bRet;
            }
            SqlBuilder builder = new SqlBuilder();
            string sql = builder.BuildInsertSql(table);
            if (null == sql || sql.Length < 1)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) =>
            {
                bRet = database.ExecuteUpdate(sql);
            });
            return bRet;
        }

        public bool UpdateEntity<TEntity>(TEntity entity)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            DBTable table = DBObjectActivator.CreateTableColumns<TEntity>(entity);
            if (null == table)
            {
                return bRet;
            }
            SqlBuilder builder = new SqlBuilder();
            string sql = builder.BuildUpdateSql(table);
            if (null == sql || sql.Length < 1)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) =>
            {
                bRet = database.ExecuteUpdate(sql);
            });
            return bRet;
        }

        public bool AddOrUpdateEntity<TEntity>(TEntity entity)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            DBTable table = DBObjectActivator.CreateTableColumns<TEntity>(entity);
            if (null == table)
            {
                return bRet;
            }
            SqlBuilder builder = new SqlBuilder();
            string sql = builder.BuildInsertOrUpdateSql(table);
            if (null == sql || sql.Length < 1)
            {
                return bRet;
            }
            m_dbQueue.InDataBase((PCDatabase database) =>
            {
                bRet = database.ExecuteUpdate(sql);
            });
            return bRet;
        }

        public bool AddOrUpdateEntitys<TEntity>(List<TEntity> entitys)
        {
            bool bRet = false;
            if (null == m_dbQueue)
            {
                return bRet;
            }
            int eCounts = entitys.Count;
            if (eCounts < 1)
            {
                return bRet;
            }
            DBTable table = DBObjectActivator.CreateTable<TEntity>();
            if (null == table)
            {
                return bRet;
            }
            string sql = null;
            SqlBuilder builder = new SqlBuilder();
            m_dbQueue.InTransaction((PCDatabase database, ref bool rollback) =>
            {
                for (int i = 0; i < eCounts; ++i)
                {
                    TEntity entity = entitys[i];
                    table.InitColumnByEntity<TEntity>(entity);
                    sql = builder.BuildInsertOrUpdateSql(table);
                    if (null == sql || sql.Length < 1){
                        continue;
                    }
                    bRet = database.ExecuteUpdate(sql);
                }
            });

            return bRet;
        }

        public List<TEntity> GetAllEntitys<TEntity>()
        {
            List<TEntity> retList = new List<TEntity>();

            if (null == m_dbQueue)
            {
                return retList;
            }

            DBTable table = DBObjectActivator.CreateTable<TEntity>();
            if (null == table)
            {
                return retList;
            }
            SqlBuilder builder = new SqlBuilder();
            string sql = builder.BuildQueryFullTableSql(table);
            if (null == sql || sql.Length < 1)
            {
                return retList;
            }
            m_dbQueue.InDataBase((PCDatabase database) => {

                PCResultSet rs = database.ExecuteQuery(sql);
                while (rs.Next())
                {
                    TEntity obj = EntityObjectActivator.CreateEntity<TEntity>(rs);
                    if (!obj.Equals(default(TEntity)))
                    {
                        retList.Add(obj);
                    }
                }
                rs.Close();

            });

            return retList;
        }

        public List<TEntity> GetEntitys<TEntity>(string sql)
        {
            List<TEntity> retList = new List<TEntity>();

            if (null == m_dbQueue)
            {
                return retList;
            }
            if (null == sql || sql.Length < 1)
            {
                return retList;
            }
            m_dbQueue.InDataBase((PCDatabase database) => {

                PCResultSet rs = database.ExecuteQuery(sql);
                while (rs.Next())
                {
                    TEntity obj = EntityObjectActivator.CreateEntity<TEntity>(rs);
                    if (!obj.Equals(default(TEntity)))
                    {
                        retList.Add(obj);
                    }
                }
                rs.Close();

            });

            return retList;
        }

        public TEntity GetEntity<TEntity>(string sql)
        {
            TEntity retEntity = default(TEntity);

            if (null == m_dbQueue)
            {
                return retEntity;
            }
            if (null == sql || sql.Length < 1)
            {
                return retEntity;
            }
            m_dbQueue.InDataBase((PCDatabase database) => {

                PCResultSet rs = database.ExecuteQuery(sql);
                if (rs.Next())
                {
                    retEntity = EntityObjectActivator.CreateEntity<TEntity>(rs);

                }
                rs.Close();

            });

            return retEntity;

        }
    }
}

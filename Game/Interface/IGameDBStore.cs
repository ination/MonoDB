using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Project.Game.DataBase
{
    public interface IGameDBStore
    {
        /// <summary>
        /// 根据tableName检查table是否存在
        /// </summary>
        bool TableExists(string tableName);

        /// <summary>
        /// 根据tableName清空表
        /// </summary>
        bool EraseTable(string talbleName);

        /// <summary>
        /// 根据tableName移除表
        /// </summary>
        bool RemoveTable(string talbleName);

        /// <summary>
        /// 根据sql更新数据库 
        /// </summary>
        bool ExecuteUpdateSql(string sql);

        /// <summary>
        /// 根据TEntity检查table是否存在
        /// </summary>
        bool TableExists<TEntity>();

        /// <summary>
        /// 根据tableName清空表
        /// </summary>
        bool EraseTable<TEntity>();
        bool RemoveTable<TEntity>();
        bool CreateTable<TEntity>();

        bool AddEntity<TEntity>(TEntity entity);
        bool UpdateEntity<TEntity>(TEntity entity);
        bool AddOrUpdateEntity<TEntity>(TEntity entity);
        bool AddOrUpdateEntitys<TEntity>(List<TEntity> entitys);

        List<TEntity> GetAllEntitys<TEntity>();
        List<TEntity> GetEntitys<TEntity>(string sql);
        TEntity GetEntity<TEntity>(string sql);
    }
}

using UnityEngine;
using System.Collections;


namespace Project.Game.DataBase
{
    internal class DBObjectActivator
    {
        public static DBTable CreateTable<TEntity>()
        {
            DBTable retTable = new DBTable(typeof(TEntity));
            return retTable;
        }

        public static DBTable CreateTableColumns<TEntity>()
        {
            DBTable retTable = new DBTable(typeof(TEntity));
            retTable.InitColumnByType();
            return retTable;
        }

        public static DBTable CreateTableColumns<TEntity>(TEntity entity)
        {
            DBTable retTable = new DBTable(typeof(TEntity));
            retTable.InitColumnByEntity<TEntity>(entity);
            return retTable;
        }
    }
}



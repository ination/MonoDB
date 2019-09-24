using UnityEngine;
using System.Collections;
using System;

namespace Project.Common.DB
{
    public static class PCDatabaseExtends
    {
        public static bool TableExists(this PCDatabase database, string tableName)
        {
            bool bRet = false;

            if (null == tableName || tableName.Length < 1)
            {
                return bRet;
            }
            string lowCaseTableName = tableName.ToLower();
            string sql = "select [sql] from sqlite_master where [type] = 'table' and lower(name) = " + lowCaseTableName;

            PCResultSet rs = database.ExecuteQuery(sql);
            bRet = rs.Next();
            rs.Close();

            return bRet;
        }


        /// <summary>
        ///  get table with list of tables: 
        ///  result colums: type[STRING], name[STRING],tbl_name[STRING],rootpage[INTEGER],sql[STRING]
        ///  check if table exist in database(patch from OZLB)
        /// </summary>
        /// <returns>The schema.</returns>
        public static PCResultSet GetSchema(this PCDatabase database)
        {
            //result colums: type[STRING], name[STRING],tbl_name[STRING],rootpage[INTEGER],sql[STRING]

            string sql = "SELECT type, name, tbl_name, rootpage, sql FROM " +
                "(SELECT * FROM sqlite_master UNION ALL SELECT * FROM sqlite_temp_master) " +
                "WHERE type != 'meta' AND name NOT LIKE 'sqlite_%' ORDER BY tbl_name, type DESC, name";
            PCResultSet rs = database.ExecuteQuery(sql);
            return rs;
        }

        /// <summary>
        /// get table schema: result colums: cid[INTEGER], name,type [STRING], notnull[INTEGER], dflt_value[],pk[INTEGER]
        /// </summary>
        /// <returns>The table schema.</returns>
        public static PCResultSet GetTableSchema(this PCDatabase database, string tableName)
        {
            //result colums: cid[INTEGER], name,type [STRING], notnull[INTEGER], dflt_value[],pk[INTEGER]

            string sql = "pragma table_info('" + tableName + "')";
            PCResultSet rs = database.ExecuteQuery(sql);
            return rs;
        }

        public static bool ColumnExistsInTable(this PCDatabase database, string columnName, string tableName)
        {
            bool bRet = false;

            string lowCase_cName = columnName.ToLower();
            string lowCase_tName = tableName.ToLower();

            PCResultSet rs = database.GetTableSchema(lowCase_tName);
            while(rs.Next())
            {
                string name = rs.StringForColumn("name").ToLower();
                if (name.Equals(lowCase_cName))
                {
                    bRet = true;
                    break;
                }
            }
            rs.Close();
            return bRet;
        }

        public static bool DropTable(this PCDatabase database, string tableName)
        {
            bool bRet = false;
            if (null == tableName || tableName.Length < 1)
            {
                bRet = false;
            }
            string sql = "DROP TABLE '" + tableName + "'";
            bRet = database.ExecuteUpdate(sql);
            return bRet;
        }

        public static bool DeleteTable(this PCDatabase database, string tableName)
        {
            bool bRet = false;
            if (null == tableName || tableName.Length < 1)
            {
                bRet = false;
            }
            string sql = "DELETE TABLE '" + tableName + "'";
            bRet = database.ExecuteUpdate(sql);
            return bRet;
        }
    }
}


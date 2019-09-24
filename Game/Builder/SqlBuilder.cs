using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Project.Game.DataBase
{
    internal class SqlBuilder
    {

        public SqlBuilder()
        {
        }

        public string BuildCreateSql(DBTable table)
        {
            string retSql = null;
            if (null == table)
            {
                return retSql;
            }

            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return retSql;
            }

            Dictionary<string, DBColumn> tableColumns = table.Columns;
            if (null == tableColumns || tableColumns.Count < 1)
            {
                return retSql;
            }

            string columnsString = "";
            foreach (KeyValuePair<string, DBColumn> kv in tableColumns)
            {
                DBColumn column = kv.Value;

                columnsString += column.Name + " " + DBMapTypeSystem.MapDBTypeString(column.ValueType);
                columnsString += ", ";
            }

            string pKeyString = "";
            List<string> PrimaryKeys = table.PrimaryKeys;

            if (null == PrimaryKeys || 0 == PrimaryKeys.Count)
            {
                ////自动添加一个自动增长的ID
                //pKeyString += "db_auto_id INTEGER PRIMARY KEY AUTOINCREMENT";

                string errorStr = string.Format("{0} Primary Key Not Exist", tableName);
                throw new ArgumentNullException(errorStr);
            }
            else if (1 == PrimaryKeys.Count)
            {
                DBColumn column = tableColumns[PrimaryKeys[0]];
                pKeyString += "PRIMARY KEY(" + column.Name + ")";
            }
            else
            {
                pKeyString += "PRIMARY KEY(";
                int count = PrimaryKeys.Count;
                for (int i = 0; i < count; ++i)
                {
                    pKeyString += PrimaryKeys[i];
                    if (i != count - 1)
                    {
                        pKeyString += ", ";
                    }
                }
                pKeyString += ")";
            }

            retSql = string.Format("CREATE TABLE IF NOT EXISTS '{0}' ({1} {2})", tableName, columnsString, pKeyString);

            return retSql;
        }

        public string BuildInsertSql(DBTable table)
        {
            string retSql = null;
            if (null == table)
            {
                return retSql;
            }

            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return retSql;
            }

            Dictionary<string, DBColumn> tableColumns = table.Columns;
            if (null == tableColumns || tableColumns.Count < 1)
            {
                return retSql;
            }

            string keyStr = "";
            string valueStr = "";
            int i = 0;
            foreach (KeyValuePair<string, DBColumn> kv in tableColumns)
            {
                DBColumn column = kv.Value;
                keyStr += column.Name;
                DBType dbType = DBMapTypeSystem.MapDBType(column.ValueType);
                if (DBType.TEXT == dbType)
                {
                    valueStr += "'" + column.Value + "'";
                }
                else
                {
                    valueStr += column.Value;
                }

                if (i != tableColumns.Count - 1)
                {
                    keyStr += ", ";
                    valueStr += ", ";
                }
                i++;
            }

            retSql = string.Format("INSERT INTO '{0}' ({1}) VALUES ({2})", tableName, keyStr, valueStr);

            return retSql;
        }

        public string BuildUpdateSql(DBTable table)
        {
            string retSql = null;
            if (null == table)
            {
                return retSql;
            }

            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return retSql;
            }

            Dictionary<string, DBColumn> tableColumns = table.Columns;
            if (null == tableColumns || tableColumns.Count < 1)
            {
                return retSql;
            }

            string keyValueStr = "";
            int i = 0;
            foreach (KeyValuePair<string, DBColumn> kv in tableColumns)
            {
                DBColumn column = kv.Value;
                if (column.IsPrimaryKey)
                {
                    continue;
                }
                keyValueStr += column.Name;
                keyValueStr += " = ";

                DBType dbType = DBMapTypeSystem.MapDBType(column.ValueType);
                if (DBType.TEXT == dbType)
                {
                    keyValueStr += "'" + column.Value + "'";
                }
                else
                {
                    keyValueStr += column.Value;
                }

                if (i != tableColumns.Count - 1)
                {
                    keyValueStr += ", ";
                }
                i++;
            }

            string condString = "";
            List<string> PrimaryKeys = table.PrimaryKeys;

            if (null == PrimaryKeys || 0 == PrimaryKeys.Count)
            {
                string errorStr = string.Format("{0} Primary Key Not Exist", tableName);
                throw new ArgumentNullException(errorStr);
            }
            else
            {
                for (int p = 0; p < PrimaryKeys.Count; ++p)
                {
                    string pKey = PrimaryKeys[p];
                    DBColumn column = tableColumns[pKey];
                    condString += column.Name + " = ";

                    DBType dbType = DBMapTypeSystem.MapDBType(column.ValueType);
                    if (DBType.TEXT == dbType)
                    {
                        condString += "'" + column.Value + "'";
                    }
                    else
                    {
                        condString += column.Value;
                    }

                    if (p != PrimaryKeys.Count - 1)
                    {
                        condString += " AND ";
                    }
                }
            }

            retSql = string.Format("UPDATE '{0}' SET {1} WHERE {2} ", tableName, keyValueStr, condString);

            return retSql;
        }

        public string BuildInsertOrUpdateSql(DBTable table)
        {
            string retSql = null;
            if (null == table)
            {
                return retSql;
            }

            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return retSql;
            }

            Dictionary<string, DBColumn> tableColumns = table.Columns;
            if (null == tableColumns || tableColumns.Count < 1)
            {
                return retSql;
            }

            string keyStr = " ";
            string valueStr = "";
            int i = 0;
            foreach (KeyValuePair<string, DBColumn> kv in tableColumns)
            {
                DBColumn column = kv.Value;
                keyStr += column.Name;
                DBType dbType = DBMapTypeSystem.MapDBType(column.ValueType);
                if (DBType.TEXT == dbType)
                {
                    valueStr += "'" + column.Value + "'";
                }
                else
                {
                    valueStr += column.Value;
                }

                if (i != tableColumns.Count - 1)
                {
                    keyStr += ", ";
                    valueStr += ", ";
                }
                i++;
            }

            retSql = string.Format("INSERT OR REPLACE INTO '{0}' ({1}) VALUES ({2})", tableName, keyStr, valueStr);

            return retSql;
        }

        public string BuildQueryFullTableSql(DBTable table)
        {
            string retSql = null;
            if (null == table)
            {
                return retSql;
            }

            string tableName = table.TableName;
            if (null == tableName || tableName.Length < 1)
            {
                return retSql;
            }

            retSql = string.Format("SELECT * FROM {0}", tableName);

            return retSql;
        }
    }
}

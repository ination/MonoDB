using System;
using System.Data;

namespace Project.Game.DataBase
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute() { }
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }
        //名字
        public string Name { get; set; }
        //主键
        public bool PrimaryKey { get; set; }
        //自动增长
        public bool AutoIncrement { get; set; }
        //不参与映射
        public bool NotMapped { get; set; }
        //新添加
        public bool AddNew { get; set; }
    }
}

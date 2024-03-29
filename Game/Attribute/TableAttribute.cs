﻿using System;

namespace Project.Game.DataBase
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        public TableAttribute() { }
        public TableAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public string Schema { get; set; }
    }
}

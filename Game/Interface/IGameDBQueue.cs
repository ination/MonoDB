using UnityEngine;
using System.Collections;

using Project.Common.DB;

namespace Project.Game.DataBase
{
    public interface IGameDBQueue
    {
        string DBPath { get; }
        PCDatabaseQueue DBQueue { get; }

        void InitDataBase(string dbPath);
        void CloseDataBase();
    }
}

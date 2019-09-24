using UnityEngine;
using System.Collections;

using Project.Common.DB;

namespace Project.Game.DataBase
{
    public static class GameStore
    {
        public static void InitStore(string dbPath)
        {
            GameDBQueue.Instance.InitDataBase(dbPath);
        }

        public static void CloseStore()
        {
            GameDBQueue.Instance.CloseDataBase();
        }

        public static IGameDBQueue StoreQueue()
        {
            return GameDBQueue.Instance;
        }

        public static IGameDBStore Store()
        {
            return GameDBQueue.Instance.GetStore();
        }
    }
}


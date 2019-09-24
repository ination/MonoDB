using UnityEngine;
using System.Collections;

namespace Project.Common.DB
{
    public delegate void DataBaseCallback(PCDatabase database);
    public delegate void TransactionCallback(PCDatabase database, ref bool rollback);
}


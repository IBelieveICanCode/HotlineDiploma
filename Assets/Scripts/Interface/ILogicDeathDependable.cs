using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeathHandler();
interface ILogicDeathDependable
{
    event OnDeathHandler DeathEvent;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeathHandler();
public interface ILogicDeathDependable
{
    event OnDeathHandler DeathEvent;
}

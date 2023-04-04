using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Strings
{
    //Tags
    public const string UiTag = "UI";
    public const string CoreScriptsTag = "CoreScripts";
    public const string PlayerTag = "Player";
    public const string EnemyTag = "enemy";
    public const string BossTag = "Boss";    

    //Masks
    public const string FloorMask = "Floor";
    public const string ZombieMask = "Enemy";

    //Aminations

    //-Player
    public const string PlayerMovAnimation = "Mov";

    //-Zumbie
    public const string IsAtackingAnimation = "IsAtacking";
    public const string ZombieMovAnimation = "Mov";
    public const string ZombieDieAnimation = "Die";

    //Scenes
    public const string HotelScene = "Hotel";

    //PlayerPrefs
    public const string BestSurvivedTimeSave = "BestSurvivedTime";
}

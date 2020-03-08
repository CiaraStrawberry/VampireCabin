using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// easy way to centralize dependancies for singletons.
public class GlobalVariables : Singleton<GlobalVariables>
{

    public GameObject center;

    public PlayerHealthManager healthManager;

    public PlayerMovementController playerMovCon;

    public LevelController levelCon;

    public AISpawner aiSpawner;

    public SceneController sceneMan;

    public PlayerWeaponController weaponCon;

}

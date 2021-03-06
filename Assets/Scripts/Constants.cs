﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const float axisThreshold = 0.001f;
    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string ThrowBombButton = "ThrowBomb";

    public const string BombPickUpName = "Bomb";

    //Event names
    public const string PlayerDeathEvent = "PlayerDeath";
    public const string ActivateCharactersEvent = "ActivateCharacters";
    public const string DeactivatePlayerControlEvent = "DeactivatePlayerControl";
    public const string GoToMainMenuEvent = "GoToMainMenu";
    public const string ShowCreditsEvent = "ShowCredits";
}

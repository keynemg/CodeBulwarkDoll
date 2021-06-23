using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int lv_Generator_Hp;
    public int lv_Generator_Armor;
    public int lv_Generator_Regen;

    public float current_Generator_Hp;

    public int lv_Player_Hp;
    public int lv_Player_Sp;
    public float lv_Player_SpRegen;

    public int current_Wave;
    public int current_Gold;

    public PlayerData(PlayerBase generator, Player_Stats player, Spawn_Waves waves)
    {
        lv_Generator_Hp = generator.hpLV;
        lv_Generator_Armor = generator.armorLV;
        lv_Generator_Regen = generator.regenLV;

        current_Generator_Hp = generator.currentHp;

        lv_Player_Hp = player.hpLV;
        lv_Player_Sp = player.spLV;
        lv_Player_SpRegen = player.spRegen;

        current_Wave = waves.currentWave;

        current_Gold = player.p_Current_Gold;
    }

}

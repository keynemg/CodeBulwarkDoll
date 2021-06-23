using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(PlayerBase.Instance.GetComponent<PlayerBase>(), Player_Stats.Instance.GetComponent<Player_Stats>(),Spawn_Waves.Instance.GetComponent<Spawn_Waves>());
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        PlayerBase generator = PlayerBase.Instance.GetComponent<PlayerBase>();
        Player_Stats player = Player_Stats.Instance.GetComponent<Player_Stats>();
        Spawn_Waves waves = Player_Stats.Instance.GetComponent<Spawn_Waves>();


        generator.hpLV = data.lv_Generator_Hp;
        generator.armorLV = data.lv_Generator_Armor;
        generator.regenLV = data.lv_Generator_Regen;

        generator.currentHp = data.current_Generator_Hp;

        player.hpLV = data.lv_Player_Hp;
        player.spLV = data.lv_Player_Sp;
        player.spRegen = data.lv_Player_SpRegen;

        waves.currentWave = data.current_Wave;

        player.p_Current_Gold = data.current_Gold;
    }



}

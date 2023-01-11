using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Items.Firearms;
using MEC;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace SCP999
{
    public class Plugin
    {
        private Player player = null;
        [PluginEntryPoint("SCP999", "1.0", "SCP999", "笑嘻嘻")]
        public void Enter()
        {
            if (Config.isEnable) {
                EventManager.RegisterEvents(this);
            }
        }
        [PluginConfig]
        public Config Config;
        [PluginEvent(PluginAPI.Enums.ServerEventType.RoundStart)]
        private void RoundStart()
        {
            try
            {
                Timing.CallDelayed(2f, () =>
                {
                    List<Player> players = Player.GetPlayers();
                    Random random = new Random();
                    while (true)
                    {
                        int i = random.Next(0, players.Count);
                        player = players[i];
                        if (player.Role == PlayerRoles.RoleTypeId.FacilityGuard)
                        {
                            break;
                        }
                    }
                    player.Role = PlayerRoles.RoleTypeId.Tutorial;
                    player.SendBroadcast("你是SCP-999", 3);
                    player.DisplayNickname = "SCP-999 " + player.Nickname;
                    player.AddItem(ItemType.GunLogicer);
                    player.AddItem(ItemType.ArmorHeavy);
                    player.AddAmmo(ItemType.Ammo762x39, 100);
                    player.GameObject.transform.localScale = new UnityEngine.Vector3(0.5f, 0.5f, 0.5f);
                });
            }
            catch (Exception e)
            {

            }
        }
        [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerDamage)]
        private void PlayerAttack(Player target, Player attacker, DamageHandlerBase damageHandler)
        {
            try
            {
                if (this.player != null)
                {
                    if (attacker.UserId == this.player.UserId)
                    {
                        damageHandler = new FirearmDamageHandler((Firearm)player.CurrentItem, 0);
                        target.Heal(Config.Health);
                    }
                }
            }catch(Exception e)
            {

            }
        }
        [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerDeath)]
        private void PlayerDead(Player player, Player attacker, DamageHandlerBase damageHandler)
        {
            try
            {
                if (this.player != null)
                {
                    if (player.UserId == this.player.UserId)
                    {
                        Server.SendBroadcast("SCP-999收容成功，收容者：" + attacker.Nickname, 5);
                        this.player = null;
                    }
                }
            }catch(Exception e)
            {

            }
        }
    }
}

using System.Text;
using System;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using System.Collections.Generic;
using System.Linq;

namespace BossChecklist_TShock
{
    [ApiVersion(2, 1)]
    public class BossChecklist_TShock : TerrariaPlugin
    {
        public override string Author => "hdseventh and zqhong";
        public override string Description => "Boss Checklist for TShock";
        public override string Name => "Boss Checklist";

        public override Version Version =>
            new Version(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public BossChecklist_TShock(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

        public void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("bcheck.use", bosscheck, "bosses"));
        }

        /// <summary>
        /// bosscheck，列出 BOSS 挑战记录。
        ///
        /// 参考：
        /// https://terraria.fandom.com/zh/wiki/Boss
        /// https://terraria.fandom.com/wiki/Bosses
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void bosscheck(CommandArgs args)
        {
            var killedList = new List<string>();
            var aliveList = new List<string>();

            var bossMap = new Dictionary<string, bool>
            {
                { "克苏鲁之眼", NPC.downedBoss1 },
                { "史莱姆王", NPC.downedSlimeKing },
                { "骷髅王", NPC.downedBoss3 },
                { "血肉墙", Main.hardMode },
                { "史莱姆皇后", NPC.downedQueenSlime },
                { "毁灭者", NPC.downedMechBoss1 },


                { "双子魔眼", NPC.downedMechBoss2 },
                { "机械骷髅王", NPC.downedMechBoss3 },
                { "猪龙鱼公爵", NPC.downedFishron },
                { "石巨人", NPC.downedGolemBoss },
                { "世纪之花", NPC.downedPlantBoss },
                { "光之女皇", NPC.downedEmpressOfLight },
                { "拜月教邪教徒", NPC.downedAncientCultist },
                { "月亮领主", NPC.downedMoonlord },
                { WorldGen.crimson ? "克苏鲁之脑" : "世界吞噬怪", NPC.downedBoss2 }
            };

            foreach (var item in bossMap)
            {
                if (item.Value)
                {
                    killedList.Add(item.Key);
                }
                else
                {
                    aliveList.Add(item.Key);
                }
            }

            var sb = new StringBuilder();


            var killedStr = "无";
            if (killedList.Count > 0)
            {
                killedStr = string.Join("，", killedList);
            }

            var aliveStr = "无";
            if (aliveList.Count > 0)
            {
                aliveStr = string.Join("，", aliveList);
            }

            // const string killed = "([c/FF0000:Killed])";
            // const string alive = "([c/00FF00:Alive])";
            
            args.Player.SendInfoMessage($"[c/FF0000:已打败]：{killedStr}");
            args.Player.SendInfoMessage($"[c/00FF00:未击杀]：{aliveStr}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }

            base.Dispose(disposing);
        }
    }
}
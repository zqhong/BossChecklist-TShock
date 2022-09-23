using System.Text;
using System;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace BossChecklist_TShock
{
    [ApiVersion(2, 1)]
    public class BossChecklist_TShock : TerrariaPlugin
    {
        public override string Author => "hdseventh";
        public override string Description => "Boss Checklist for TShock";
        public override string Name => "Boss Checklist";
        public override Version Version { get { return new Version(1, 0, 0, 0); } }
        public BossChecklist_TShock(Main game) : base(game) { }
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
            var killed = "([c/FF0000:Killed])";
            var alive = "([c/00FF00:Alive])";
            StringBuilder sb = new StringBuilder();
            
            sb.Append("克苏鲁之眼 " + (NPC.downedBoss1 ? killed : alive));
            sb.Append(", 史莱姆王 " + (NPC.downedSlimeKing ? killed : alive));
            if (WorldGen.crimson)
            {
                sb.Append(", 克苏鲁之脑 " + (NPC.downedBoss2 ? killed : alive));
            }
            else
            {
                sb.Append(", 世界吞噬怪 " + (NPC.downedBoss2 ? killed : alive));
            }
            sb.Append(", 骷髅王 " + (NPC.downedBoss3 ? killed : alive));
            sb.Append(", 血肉墙 " + (Main.hardMode ? killed : alive));
            sb.Append(", 史莱姆皇后 " + (NPC.downedQueenSlime ? killed : alive));
            sb.Append(", 毁灭者 " + (NPC.downedMechBoss1 ? killed : alive));
            sb.Append(", 双子魔眼 " + (NPC.downedMechBoss2 ? killed : alive));
            sb.Append(", 机械骷髅王 " + (NPC.downedMechBoss3 ? killed : alive));
            sb.Append(", 猪龙鱼公爵 " + (NPC.downedFishron ? killed : alive));
            sb.Append(", 石巨人 " + (NPC.downedGolemBoss ? killed : alive));
            sb.Append(", 世纪之花 " + (NPC.downedPlantBoss ? killed : alive));
            sb.Append(", 光之女皇 " + (NPC.downedEmpressOfLight ? killed : alive));
            sb.Append(", 拜月教邪教徒 " + (NPC.downedAncientCultist ? killed : alive));
            sb.Append(", 月亮领主 " + (NPC.downedMoonlord ? killed : alive));
            args.Player.SendInfoMessage(sb.ToString());
        }

        protected override void Dispose(bool Disposing)
        {
            if (Disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(Disposing);
        }
    }
}

using AsmResolver;
using Celeste;
using Celeste.Mod;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

[CustomEntity("Celestish/OverrideCS05MirrorPortalRespawn")]
public class OverrideCS05MirrorPortalRespawn : Entity
{
    public string level;

    public static OverrideCS05MirrorPortalRespawn Load(Level level, LevelData level_data, Vector2 offset, EntityData data)
        => new OverrideCS05MirrorPortalRespawn(data, data.Position + offset);

    public OverrideCS05MirrorPortalRespawn(EntityData data, Vector2 position) : base(position)
    {
        On.Celeste.CS04_MirrorPortal.OnEnd += modOnEnd;
        level = data.Attr("level");
    }

    private void modOnEnd(On.Celeste.CS04_MirrorPortal.orig_OnEnd orig, CS04_MirrorPortal cutscene, Level level)
    {
        level.OnEndOfFrame += delegate
            {
                DynamicData data = DynamicData.For(cutscene);
                Player player = data.Get<Player>("player");
                var fader = data.Get("fader");
                DynamicData fader_data = DynamicData.For(fader);

                if (fader != null && !cutscene.WasSkipped)
                {
                    fader_data.Set("Tag", Tags.Global);
                    fader_data.Set("Target", 0f);
                    fader_data.Set("Ended", true);
                }
                Leader.StoreStrawberries(player.Leader);
                level.Remove(player);
                level.UnloadLevel();
                level.Session.Dreaming = true;
                level.Session.Keys.Clear();

                level.Session.Level = this.level;
                level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));

                //FIXME: probably make this customisable via EntityData
                if (level.Session.Area.Mode == AreaMode.Normal)
                {
                    level.LoadLevel(Player.IntroTypes.TempleMirrorVoid);
                }
                else
                {
                    level.LoadLevel(Player.IntroTypes.WakeUp);
                    Audio.SetMusicParam("fade", 1f);
                }

                Leader.RestoreStrawberries(level.Tracker.GetEntity<Player>().Leader);
                level.Camera.Y -= 8f;
                if (!cutscene.WasSkipped && level.Wipe != null)
                {
                    level.Wipe.Cancel();
                }
                if (fader != null)
                {
                    fader_data.Invoke("RemoveTag", (int)Tags.Global);
                }
            };
    }
}
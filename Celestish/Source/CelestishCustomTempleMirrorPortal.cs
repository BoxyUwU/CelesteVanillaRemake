using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Mod.Celestish.Entities;

[CustomEntity("Celestish/CustomTempleMirrorPortal")]
public class CustomTempleMirrorPortal : TempleMirrorPortal
{
    public string level;

    public static CustomTempleMirrorPortal Load(Level level, LevelData level_data, Vector2 offset, EntityData data)
        => new CustomTempleMirrorPortal(data, data.Position + offset);

    public CustomTempleMirrorPortal(EntityData data, Vector2 position) : base(position)
    {
        level = data.Attr("level");
        On.Celeste.TempleMirrorPortal.Added += modAdded;
    }

    public override void Added(Scene scene)
    {
        base.Added(scene);
        On.Celeste.CS04_MirrorPortal.OnEnd += modOnEnd;
    }

    private void modAdded(On.Celeste.TempleMirrorPortal.orig_Added orig, TempleMirrorPortal portal, Scene scene)
    {
        orig.Invoke(portal, scene);
        On.Celeste.CS04_MirrorPortal.OnEnd -= modOnEnd;
    }

    private void modOnEnd(On.Celeste.CS04_MirrorPortal.orig_OnEnd orig, CS04_MirrorPortal cutscene, Level level)
    {
        level.OnEndOfFrame += delegate
            {
                Player player = cutscene.player;
                var fader = cutscene.fader;

                if (fader != null && !cutscene.WasSkipped)
                {
                    fader.Tag = Tags.Global;
                    fader.Target = 0f;
                    fader.Ended = true;
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
                    fader.RemoveTag(Tags.Global);
                }
            };
    }
}
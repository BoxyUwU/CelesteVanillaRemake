using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Mod.Celestish.Entities;

[CustomEntity("Celestish/CustomPlayerSeeker")]
public class CustomPlayerSeeker : PlayerSeeker
{
    public string respawn_level;

    public static CustomPlayerSeeker Load(Level level, LevelData level_data, Vector2 offset, EntityData data)
        => new CustomPlayerSeeker(data, offset);

    public CustomPlayerSeeker(EntityData data, Vector2 offset) : base(data, offset)
    {
        respawn_level = data.Attr("level");
        On.Celeste.PlayerSeeker.Awake += modAwake;
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        On.Celeste.PlayerSeeker.End += modEnd;
    }

    private void modAwake(On.Celeste.PlayerSeeker.orig_Awake orig, PlayerSeeker seeker, Scene scene)
    {
        orig.Invoke(seeker, scene);
        On.Celeste.PlayerSeeker.End -= modEnd;
    }

    private void modEnd(On.Celeste.PlayerSeeker.orig_End orig, PlayerSeeker seeker)
    {
        Level level = base.Scene as Level;
        level.OnEndOfFrame += delegate
        {

            Glitch.Value = 0f;
            Distort.Anxiety = 0f;
            Engine.TimeRate = 1f;
            level.Session.ColorGrade = null;
            level.UnloadLevel();
            level.CanRetry = true;
            level.Session.Level = respawn_level;
            level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
            level.LoadLevel(Player.IntroTypes.WakeUp);
            Leader.RestoreStrawberries(level.Tracker.GetEntity<Player>().Leader);
        };
    }
}
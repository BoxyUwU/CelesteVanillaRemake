using System.Collections;
using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Mod.Celestish.Entities;

[CustomEntity("Celestish/CustomTempleMirrorPortal")]
public class CustomTempleMirrorPortal : TempleMirrorPortal
{
    public string TransitionToLevel;
    public bool TransitionToSeeker;
    public bool ASideIntro;
    public bool ASideMusic;

    public static CustomTempleMirrorPortal Load(Level level, LevelData level_data, Vector2 offset, EntityData data)
        => new CustomTempleMirrorPortal(data, data.Position + offset);

    public CustomTempleMirrorPortal(EntityData data, Vector2 position) : base(position)
    {
        TransitionToLevel = data.Attr("level");
        TransitionToSeeker = data.Bool("to_seeker");
        ASideIntro = data.Bool("aside_intro");
        ASideMusic = data.Bool("bside_intro");
    }

    public override void Added(Scene scene)
    {
        base.Added(scene);
        On.Celeste.TempleMirrorPortal.OnPlayer += modOnPlayer;
    }

    private void modOnPlayer(On.Celeste.TempleMirrorPortal.orig_OnPlayer orig, TempleMirrorPortal portal, Player player)
    {
        if (portal is CustomTempleMirrorPortal)
        {
            if (portal.canTrigger)
            {
                portal.canTrigger = false;
                Scene.Add(new CS04_CustomMirrorPortal(
                    TransitionToLevel,
                    TransitionToSeeker,
                    ASideIntro,
                    ASideMusic,
                    player,
                    (CustomTempleMirrorPortal)portal
                ));
            }
        }
        else
        {
            orig(portal, player);
        }
    }

}

public class CS04_CustomMirrorPortal : CS04_MirrorPortal
{
    public string TransitionToLevel;
    public bool TransitionToSeeker;
    public bool ASideIntro;
    public bool ASideMusic;

    public CS04_CustomMirrorPortal(
        string ToLevel,
        bool ToSeeker,
        bool Intro,
        bool Music,
        Player player,
        CustomTempleMirrorPortal portal
    ) : base(player, portal)
    {
        TransitionToLevel = ToLevel;
        TransitionToSeeker = ToSeeker;
        ASideIntro = Intro;
        ASideMusic = Music;
    }

    public override void OnBegin(Level level)
    {
        Add(new Coroutine(Cutscene(level)));
        level.Add(fader = new Fader());
    }

    new public IEnumerator Cutscene(Level level)
    {
        // All copied from `CS04_MirrorPortal.Cutscene` and edited to branch on fields instead of a/b side

        player.StateMachine.State = 11;
        player.StateMachine.Locked = true;
        player.Dashes = 1;
        if (ASideMusic)
        {
            Audio.SetMusic(null);
        }
        else
        {
            Add(new Coroutine(MusicFadeOutBSide()));
        }
        Add(sfx = new SoundSource());
        sfx.Position = portal.Center;
        sfx.Play("event:/music/lvl5/mirror_cutscene");
        Add(new Coroutine(CenterCamera()));
        yield return player.DummyWalkToExact((int)portal.X);
        yield return 0.25f;
        yield return player.DummyWalkToExact((int)portal.X - 16);
        yield return 0.5f;
        yield return player.DummyWalkToExact((int)portal.X + 16);
        yield return 0.25f;
        player.Facing = Facings.Left;
        yield return 0.25f;
        yield return player.DummyWalkToExact((int)portal.X);
        yield return 0.1f;
        player.DummyAutoAnimate = false;
        player.Sprite.Play("lookUp");
        yield return 1f;
        player.DummyAutoAnimate = true;
        portal.Activate();
        Add(new Coroutine(level.ZoomTo(new Vector2(160f, 90f), 3f, 12f)));
        yield return 0.25f;
        player.ForceStrongWindHair.X = -1f;
        yield return player.DummyWalkToExact((int)player.X + 12, true);
        yield return 0.5f;
        player.Facing = Facings.Right;
        player.DummyAutoAnimate = false;
        player.DummyGravity = false;
        player.Sprite.Play("runWind");
        while (player.Sprite.Rate > 0f)
        {
            player.MoveH(player.Sprite.Rate * 10f * Engine.DeltaTime);
            player.MoveV((0f - (1f - player.Sprite.Rate)) * 6f * Engine.DeltaTime);
            player.Sprite.Rate -= Engine.DeltaTime * 0.15f;
            yield return null;
        }
        yield return 0.5f;
        player.Sprite.Play("fallFast");
        player.Sprite.Rate = 1f;
        Vector2 target = portal.Center + new Vector2(0f, 8f);
        Vector2 from = player.Position;
        for (float p2 = 0f; p2 < 1f; p2 += Engine.DeltaTime * 2f)
        {
            player.Position = from + (target - from) * Ease.SineInOut(p2);
            yield return null;
        }
        player.ForceStrongWindHair.X = 0f;
        fader.Target = 1f;
        yield return 2f;
        player.Sprite.Play("sleep");
        yield return 1f;
        yield return level.ZoomBack(1f);
        if (TransitionToSeeker)
        {
            level.Session.ColorGrade = "templevoid";
            for (float p2 = 0f; p2 < 1f; p2 += Engine.DeltaTime)
            {
                Glitch.Value = p2 * 0.05f;
                level.ScreenPadding = 32f * p2;
                yield return null;
            }
        }
        while ((portal.DistortionFade -= Engine.DeltaTime * 2f) > 0f)
        {
            yield return null;
        }
        EndCutscene(level);
    }

    public override void OnEnd(Level level)
    {
        level.OnEndOfFrame += delegate
    {
        Player player = this.player;
        var fader = this.fader;

        if (fader != null && !WasSkipped)
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

        level.Session.Level = TransitionToLevel;
        level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));

        if (ASideIntro)
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
        if (!WasSkipped && level.Wipe != null)
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
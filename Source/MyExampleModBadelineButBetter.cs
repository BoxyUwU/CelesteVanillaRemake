using System;
using System.Collections;
using System.ComponentModel;
using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

[CustomEntity("badelineButBetter")]
public class BadelineButBetter : BadelineOldsite
{
    public static BadelineButBetter Load(Level level, LevelData level_data, Vector2 offset, EntityData data)
        => new BadelineButBetter(data.Position + offset, 1);

    public BadelineButBetter(Vector2 position, int index) : base(position, index)
    {
    }

    public BadelineButBetter(EntityData data, Vector2 offset, int index) : base(data, offset, index)
    {
    }

    public override void Added(Scene scene)
    {
        // Entity.Added
        var ptr = typeof(Entity).GetMethod("Added").MethodHandle.GetFunctionPointer();
        var baseAdded = (Action<Scene>)Activator.CreateInstance(typeof(Action<Scene>), this, ptr);
        baseAdded(scene);

        Level level = scene as Level;

        if (!level.Session.GetFlag("evil_maddy_intro"))
        {

            // BadelineOldSite.Added but without the hard coded celeste checks
            Hovering = false;
            Visible = true;
            Hair.Visible = true;
            Sprite.Play("pretendDead");


            level.Session.Audio.Music.Event = null;
            level.Session.Audio.Apply(forceSixteenthNoteHack: false);

            Scene.Add(new CS02_BadelineIntro(this));
        }
        else
        {
            Add(new Coroutine(StartChasingRoutine(level)));
        }
    }
}
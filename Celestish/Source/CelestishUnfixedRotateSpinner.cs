using System;
using System.Reflection.Emit;
using Celeste;
using Celeste.Mod;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod;
using MonoMod.Utils;

[CustomEntity("Celestish/UnfixedStarRotateSpinner")]
public class UnfixedStarRotateSpinner : StarRotateSpinner
{
    public UnfixedStarRotateSpinner(EntityData data, Vector2 offset) : base(data, offset)
    {
    }

    public override void Awake(Scene scene)
    {
        base_Awake(scene);
    }

    [MonoModLinkTo("Monocle.Entity", "System.Void Awake(Monocle.Scene)")]
    public void base_Awake(Scene scene)
    {
        base.Awake(scene);
    }
}

[CustomEntity("Celestish/UnfixedBladeRotateSpinner")]
public class UnfixedBladeRotateSpinner : BladeRotateSpinner
{
    public UnfixedBladeRotateSpinner(EntityData data, Vector2 offset) : base(data, offset)
    {
    }

    public override void Awake(Scene scene)
    {
        base_Awake(scene);
    }

    [MonoModLinkTo("Monocle.Entity", "System.Void Awake(Monocle.Scene)")]
    public void base_Awake(Scene scene)
    {
        base.Awake(scene);
    }
}

[CustomEntity("Celestish/UnfixedDustRotateSpinner")]
public class UnfixedDustRotateSpinner : DustRotateSpinner
{
    public UnfixedDustRotateSpinner(EntityData data, Vector2 offset) : base(data, offset)
    {
    }

    public override void Awake(Scene scene)
    {
        base_Awake(scene);
    }

    [MonoModLinkTo("Monocle.Entity", "System.Void Awake(Monocle.Scene)")]
    public void base_Awake(Scene scene)
    {
        base.Awake(scene);
    }
}
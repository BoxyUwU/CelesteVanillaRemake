using System.ComponentModel;
using Celeste;
using Celeste.Editor;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using MonoMod.Utils;

[CustomEntity("changeColorGrade")]
public class ChangeColorGrade : Trigger
{

    public ChangeColorGrade(EntityData data, Vector2 offset) : base(data, offset)
    {
        this.fade = data.Float("fade");
        this.color_grade = data.Attr("color_grade");
    }

    public override void OnEnter(Player player)
    {
        var level = player.Scene as Level;
        if (fade == 0f)
        {
            level.SnapColorGrade(color_grade);
        }
        else
        {
            level.NextColorGrade(color_grade, fade);
        }
    }

    public string color_grade;
    public float fade;
}
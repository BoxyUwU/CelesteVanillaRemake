using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

[CustomEntity("Celestish/ChangeColorGrade")]
public class ChangeColorGrade : Trigger
{

    public ChangeColorGrade(EntityData data, Vector2 offset) : base(data, offset)
    {
        fade = data.Float("fade");
        color_grade = data.Attr("color_grade");
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
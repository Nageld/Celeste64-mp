namespace Celeste64;

public class Climbers : NPC
{
    public const string TALK_FLAG = "GRANNY";

    public Climbers() : base(Assets.Models["granny"])
    {
        Model.Transform = Matrix.CreateScale(3) * Matrix.CreateTranslation(0, 0, -1.5f);
    }

    public override void Interact(Player player)
    {
    }

}
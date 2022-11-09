
public class Enemy : Character
{
    public Player Target { get; private set; }

    public Enemy(Player target)
    {
        Target = target;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }
}

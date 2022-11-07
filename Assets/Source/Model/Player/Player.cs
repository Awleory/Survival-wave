
public class Player : Character
{
    private PlayerController _controller;

    public Player() 
    { 
        _controller = new PlayerController(Movement); 
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _controller.Update(deltaTime);
    }
}


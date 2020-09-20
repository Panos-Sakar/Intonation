namespace EvilOwl.Core
{
    public enum MovementHandler
    {
        None = 0,
        RidgedBody2D = 1,
        PhysicsObject = 2
    }

    public enum ControllerType
    {
        None = 0,
        PlayerInput = 1,
        AiInput = 2,
        RandomInput = 3
    }

    public enum SpellCastingState
    {
        Waiting = 0,
        Casting = 1,
        Firing = 2
    }

}

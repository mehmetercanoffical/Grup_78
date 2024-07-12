public abstract class NPCAttackBase
{
    public abstract void Start(NPCManager manager);
    public abstract void Update(NPCManager manager);
    public abstract void Exit(NPCManager manager);
    public abstract void Die(NPCManager manager);
    public abstract void SayBaseManager(NPCBaseManager basemanager);

}

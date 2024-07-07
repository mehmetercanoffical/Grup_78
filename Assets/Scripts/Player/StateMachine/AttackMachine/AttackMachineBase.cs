
namespace AttackSystem
{
    public abstract class AttackMachineBase
    {
        public abstract void EnterState(AttackMachineBaseManager attack);
        public abstract void UpdateState(AttackMachineBaseManager attack);
        public abstract void ExitState(AttackMachineBaseManager attack);

    }
}

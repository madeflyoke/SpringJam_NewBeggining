using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure.StateMachine
{
    public interface IState
    {
        UniTask Enter();
    }
}
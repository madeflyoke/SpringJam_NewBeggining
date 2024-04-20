using Cysharp.Threading.Tasks;

namespace SpringJam.Infrastructure
{
    public interface ILoadingOperation
    {
        UniTask LoadAsync();
    }
}
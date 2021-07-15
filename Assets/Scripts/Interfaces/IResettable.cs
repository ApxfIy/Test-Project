using UnityEngine.Events;

namespace Assets.Scripts.Interfaces
{
    public interface IResettable
    {
        UnityEvent OnReset { get; }
        void ResetComponent();
    }
}

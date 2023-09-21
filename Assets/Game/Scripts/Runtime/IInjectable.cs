using Zenject;

namespace Game.Scripts.Runtime
{
    public interface IInjectable
    {
        public void Inject(DiContainer container);
    }
}

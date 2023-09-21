using HutongGames.PlayMaker;
using Zenject;

namespace Game.Scripts.Runtime.FSM
{
    public class PlayBackgroundMusic : FsmStateAction, IInjectable
    {
        public AudioClipId audioClipId;
        private AudioManager _audioManager;


        public void Inject(DiContainer container)
        {
            _audioManager = container.Resolve<AudioManager>();
        }


        public override void OnEnter()
        {
            base.OnEnter ();
            _audioManager.PlayBackgroundClip(audioClipId);
            Finish();
        }
    }
}

using HutongGames.PlayMaker;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime.FSM
{
    public class FsmLifetimeHandler : MonoBehaviour
    {
        [Inject]
        public void Inject(DiContainer container)
        {
            PlayMakerFSM fsm = GetComponent<PlayMakerFSM>();
            fsm.Preprocess();

            foreach (FsmState state in fsm.FsmStates)
            {
                foreach (FsmStateAction action in state.Actions)
                {
                    if (action is IInjectable injectable)
                    {
                        injectable.Inject(container);
                    }
                }
            }
        }
    }
}

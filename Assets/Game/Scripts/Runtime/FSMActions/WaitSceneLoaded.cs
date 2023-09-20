using System.Collections;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace Game.Scripts.Runtime.FSMActions
{
    public class WaitSceneLoaded : GetSceneActionBase
    {
        private Coroutine _coroutine;


        public override void OnEnter()
        {
            base.OnEnter ();
            _coroutine = StartCoroutine(WaitSceneRoutine());
        }


        private IEnumerator WaitSceneRoutine()
        {
            while (true)
            {
                if (_scene is { isLoaded: true })
                {
                    break;
                }

                yield return null;
            }

            Finish();
        }
    }
}

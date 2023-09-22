using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Runtime.FSM
{
    public class LoadSceneAsyncFsmAction : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
        public FsmString sceneByName;

        [HutongGames.PlayMaker.Tooltip("Event sent when scene loading is done")]
        public FsmEvent doneEvent;

        [HutongGames.PlayMaker.Tooltip("Allow the scene to be activated as soon as it's ready")]
        public FsmBool allowSceneActivation;

        private AsyncOperation _asyncOperation;


        public override void OnEnter()
        {
            base.OnEnter();
            _asyncOperation = SceneManager.LoadSceneAsync(sceneByName.Value, LoadSceneMode.Single);
            _asyncOperation.allowSceneActivation = allowSceneActivation.Value;
            _asyncOperation.completed += (operation) => Fsm.Event(doneEvent);
        }
    }
}

using DefaultNamespace.GameStates;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class Bootstrap:MonoBehaviour,ICoroutineRunner
    {
        private IStateMachine _stateMachine;
        
        [Inject]
        public void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
            _stateMachine.EnterState<BootstrapState>();
        }
    }
}
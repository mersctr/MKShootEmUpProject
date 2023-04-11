using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerFollower : MonoBehaviour
{
   
    private NavMeshAgent _agent;
    [Inject]  private PlayerController _playerController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_playerController = FindObjectOfType<PlayerController>();
        _agent.ResetPath();
    }

    // Update is called once per frame
    private void Update()
    {
        if ( !_playerController.Vitals.IsAlive)
            return;

        _agent.destination = _playerController.transform.position;
    }

    private void OnEnable()
    {
        _agent.ResetPath();
    }
}
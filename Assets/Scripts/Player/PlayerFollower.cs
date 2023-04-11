using UnityEngine;
using UnityEngine.AI;

public class PlayerFollower : MonoBehaviour
{
    private readonly bool _canFollow = true;
    private NavMeshAgent _agent;
    private PlayerController _playerController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerController = FindObjectOfType<PlayerController>();
        _agent.ResetPath();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_canFollow || !_playerController.Vitals.IsAlive)
            return;

        _agent.destination = _playerController.transform.position;
    }

    private void OnEnable()
    {
        _agent.ResetPath();
    }
}
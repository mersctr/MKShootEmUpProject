using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BossViewActivity : MonoBehaviour
{
  [Inject] private EnemyBossController _bossController;
  [SerializeField] private VisualDamageBehaviour _visualDamageBehaviour;

  private void Awake()
  {
    _visualDamageBehaviour.SetVitalsToObserve(_bossController.Vitals);
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BossViewActivity : MonoBehaviour
{
  [SerializeField] private VisualDamageBehaviour _visualDamageBehaviour;
  [Inject] private EnemyBossController _bossController;

  private void Awake()
  {
    _visualDamageBehaviour.SetVitalsToObserve(_bossController.Vitals);
  }
}

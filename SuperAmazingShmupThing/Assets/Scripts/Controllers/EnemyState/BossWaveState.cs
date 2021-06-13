using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class BossWaveState : EnemyWaveState
    {
        private Boss _boss;
        private bool _isBossReady;
        private EnemyController _controller;

        public BossWaveState(BossFactory bossFactory, Transform targetPlayer)
        {
            _boss = new Boss(bossFactory);
            _boss.ShootingTarget = targetPlayer;
        }

        public override void Operate(EnemyController controller, float deltaTime)
        {
            _controller = controller;

            if (!_isBossReady)
            {
                _boss.ActivateBoss(controller.WaveNumber / 5);
                _isBossReady = true;
                _boss.BossDestroyed += FinishWave;
            }
            else
                _boss.UpdateRegular(deltaTime);
        }

        private void FinishWave()
        {
            _boss.BossDestroyed -= FinishWave;
            _isBossReady = false;
            _controller.SetWaveDelay();
        }
    }
}
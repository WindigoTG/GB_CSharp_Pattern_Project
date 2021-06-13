using UnityEngine;

namespace ShmupProject 
{
    public class Boss : IUpdateable
    {
        BossCore _boss;
        Transform _bossTransform;
        IBossMovement _bossMovement;

        private Transform _targetPlayer;
        private BossFactory _bossFactory;

        public Transform ShootingTarget
        {
            set { _targetPlayer = value; }
        }

        public Boss(BossFactory bossFactory)
        {
            _bossTransform = new GameObject("Boss").transform;
            _bossFactory = bossFactory;
            _bossMovement = new BossMovementRandom(_bossTransform);
        }

        private void CheckIfBossWasHit(Transform hit)
        {
            _boss.CheckHit(hit);
        }

        public void UpdateRegular(float deltaTime)
        {
            _boss.Fire(_targetPlayer);
            _bossMovement.Move(_boss.WidthLeft * 2,
                                _boss.WidthRight * 2,
                                Time.deltaTime);
        }

        public void ActivateBoss(int size)
        {
            _bossTransform.gameObject.SetActive(true);
            _boss = new BossCore(_bossTransform);

            for (int i = 0; i < size; i++)
                IncreaseBossSize();


            _boss.RecalculateHitPoints();

            _bossTransform.Rotate(new Vector3(0, 180, 0));
            _bossTransform.localScale *= 0.5f;

            _bossMovement.ResetBoss();

            _boss.Destroyed += DeactivateBoss;
            
            ServiceLocator.GetService<CollisionManager>().EnemyHit += CheckIfBossWasHit;
        }

        private void IncreaseBossSize()
        {
            _boss.AddPart(_bossFactory.CreatePart(BossPartType.Random),
                              _bossFactory.CreatePart(BossPartType.Random));
        }

        private void DeactivateBoss()
        {
            _bossTransform.localScale = Vector3.one;
            _bossTransform.Rotate(new Vector3(0, 180, 0));
            _boss.Destroyed -= DeactivateBoss;
            _bossTransform.gameObject.SetActive(false);
            ServiceLocator.GetService<CollisionManager>().EnemyHit -= CheckIfBossWasHit;
            BossDestroyed?.Invoke();
        }

        public System.Action BossDestroyed;
    }
}
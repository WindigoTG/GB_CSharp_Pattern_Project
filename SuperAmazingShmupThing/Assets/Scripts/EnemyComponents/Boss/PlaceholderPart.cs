using UnityEngine;

namespace ShmupProject
{
    public class PlaceholderPart : BossPart
    {
        public override void AddPart(BossPart left, BossPart right) { }
        public override void SetPartPosition(Transform parent, bool isRight) { }
        public override void Fire(Transform player) { }
        public override void CheckHit(Transform hit) { }
        protected override void GetThisPartMaterial() { }
        public override void Destroy() { }
        public override void RecalculateHitPoints() { }
    }
}
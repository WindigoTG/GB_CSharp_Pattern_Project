using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class BossPart
    {
        protected BossPart _leftPart;
        protected BossPart _rightPart;

        protected Transform _thisPart;

        protected List<IWeaponEnemy> _weapons;
        protected List<Transform> _weaponMounts;

        protected int _maxHitPoints;
        protected int _hitPoints;

        protected List<Material> _partsMaterial;

        public virtual void AddPart(BossPart left, BossPart right)
        {
            if (_leftPart == null)
            {
                _leftPart = left;
                _leftPart.SetPartPosition(_thisPart, true);
            }
            else
                _leftPart.AddPart(left, new PlaceholderPart());

            if (_rightPart == null)
            {
                _rightPart = right;
                _rightPart.SetPartPosition(_thisPart, false);
            }
            else
                _rightPart.AddPart(new PlaceholderPart(), right);
        }

        public virtual void Fire(Transform player)
        {
            for (int i = 0; i < Mathf.Min(_weapons.Count, _weaponMounts.Count); i++)
                _weapons[i].Shoot(_weaponMounts[i]);
            if (_leftPart != null) _leftPart.Fire(player);
            if (_rightPart != null) _rightPart.Fire(player);
        }

        public virtual void SetPartPosition(Transform parent, bool isLeft)
        {
            if (isLeft) _thisPart.localScale = new Vector3(-1, 1, 1);
            var parentSize = parent.GetComponent<BoxCollider>().bounds.extents.x;
            var thisSize = _thisPart.GetComponent<BoxCollider>().bounds.extents.x;
            _thisPart.position = new Vector3(
                                            parent.position.x - (parentSize + thisSize) * _thisPart.localScale.x,
                                            parent.position.y,
                                            parent.position.z);
            _thisPart.parent = parent;
        }

        public virtual void CheckHit(Transform hit)
        {
            if (_thisPart == hit)
            {
                Debug.Log($"{_thisPart.name} was hit");
                if(_hitPoints > 0)
                    _hitPoints--;
                foreach (Material m in _partsMaterial)
                    m.color = Color.Lerp(Color.red, m.color, (float)_hitPoints / (float)_maxHitPoints);
            }
            else
            {
                if (_leftPart != null) _leftPart.CheckHit(hit);
                if (_rightPart != null) _rightPart.CheckHit(hit);
            }
        }

        protected virtual void GetThisPartMaterial()
        {
            MeshRenderer[] meshRend = _thisPart.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRend)
                _partsMaterial.Add(mr.material);
        }
    }
}
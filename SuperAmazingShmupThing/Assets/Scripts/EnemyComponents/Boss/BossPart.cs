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

        protected bool _isAlive = true;

        protected int _scoreValue = 1000;

        public float WidthLeft
        {
            get
            {
                if (_thisPart != null)
                    return _thisPart.GetComponent<BoxCollider>().bounds.extents.x + 
                        (_leftPart != null ? _leftPart.WidthLeft : 0.0f);
                else
                    return 0.0f;
            }
        }
        public float WidthRight
        {
            get
            {
                if (_thisPart != null)
                    return _thisPart.GetComponent<BoxCollider>().bounds.extents.x + 
                        (_rightPart != null ? _rightPart.WidthRight : 0.0f);
                else
                    return 0.0f;
            }
        }

        public int MaxHP => _maxHitPoints;

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
            if (_isAlive)
            {
                for (int i = 0; i < Mathf.Min(_weapons.Count, _weaponMounts.Count); i++)
                {
                    if (_weaponMounts[i].position.x < Constants.ScreenBoundX && _weaponMounts[i].position.x > -Constants.ScreenBoundX)
                        _weapons[i].Shoot(_weaponMounts[i], player.position);
                }
                if (_leftPart != null) _leftPart.Fire(player);
                if (_rightPart != null) _rightPart.Fire(player);
            }
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
            if (_isAlive)
            {


                if (_thisPart == hit)
                {
                    Debug.Log($"{_thisPart.name} was hit");
                    if (_hitPoints > 0)
                        _hitPoints--;
                    foreach (Material m in _partsMaterial)
                        m.color = Color.Lerp(Color.red, m.color, (float)_hitPoints / (float)_maxHitPoints);
                    if (_hitPoints <= 0)
                    {
                        _isAlive = false;
                        Destroy();
                    }
                }
                else
                {
                    if (_leftPart != null) _leftPart.CheckHit(hit);
                    if (_rightPart != null) _rightPart.CheckHit(hit);
                }
            }
        }

        protected virtual void GetThisPartMaterial()
        {
            MeshRenderer[] meshRend = _thisPart.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRend)
                _partsMaterial.Add(mr.material);
        }

        public virtual void Destroy()
        {
            ServiceLocator.GetService<ScoreTracker>().AddScore(_scoreValue);
            if (_leftPart != null)
            {
                _leftPart.Destroy();
                _leftPart = null;
            }
            if (_rightPart != null)
            {
                _rightPart.Destroy();
                _rightPart = null;
            }
            if (_thisPart != null)
                Object.Destroy(_thisPart.gameObject);
        }

        public virtual void RecalculateHitPoints()
        {
            if (_leftPart != null && _rightPart != null)
            {
                _leftPart.RecalculateHitPoints();
                _rightPart.RecalculateHitPoints();
                _maxHitPoints = (int)(Mathf.Max(_leftPart.MaxHP, _rightPart.MaxHP) * 2f);
                _hitPoints = _maxHitPoints;
            }
        }
    }
}
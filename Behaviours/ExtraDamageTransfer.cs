using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace al3ks1sCore.Behaviours
{
    internal class ExtraDamageTransfer : MonoBehaviour
    {

        public GameObject Target;

        private HealthManager _selfHealth;
        private HealthManager _targetHealth;

        public void Start()
        {

            _selfHealth = GetComponent<HealthManager>();
            _targetHealth = Target.GetComponent<HealthManager>();

            if (_selfHealth != null && _targetHealth != null)
                _selfHealth.TookDamage += TransferDamage;

        }

        public void TransferDamage()
        {
            if (_targetHealth.hp - _selfHealth.lastHitInstance.DamageDealt > 0)
                _targetHealth.ApplyExtraDamage(_selfHealth.lastHitInstance.DamageDealt);
        }


    }
}

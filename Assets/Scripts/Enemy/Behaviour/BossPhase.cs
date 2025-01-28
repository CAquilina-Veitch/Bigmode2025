using System;
using System.Collections.Generic;
using Extensions;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Enemy.Behaviour
{
    public class BossPhase : MonoBehaviour
    {
        public int GoToNextPhaseWhenHealthIsLessThanThisValue;

        public List<BossPhaseStep> PhaseSteps;
        
    }
}
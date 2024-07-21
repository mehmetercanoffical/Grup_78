using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TriForge
{
    [ExecuteAlways]
    public class TF_FW_WindController : MonoBehaviour
    {
        [Range(0.0f, 2.0f)]
        public float WindStrength = 0.3f;
        [Range(0.0f, 1.0f)]
        public float GrassWindRotationMapInfluence = 0.6f;
        [Range(0.0f, 3.0f)]
        public float GrassWindStrength = 0.5f;

        private Vector3 WindDirection;

        void Update()
        {
            WindDirection = transform.right.normalized;
            Shader.SetGlobalVector("TF_WIND_DIRECTION", WindDirection);
            Shader.SetGlobalFloat("TF_WIND_STRENGTH", WindStrength);
            Shader.SetGlobalFloat("TF_ROTATION_MAP_INFLUENCE", GrassWindRotationMapInfluence);
            Shader.SetGlobalFloat("TF_GRASS_WIND_STRENGTH", GrassWindStrength);
        }
    }
}
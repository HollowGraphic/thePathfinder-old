using ThePathfinder.Components.Authoring;
using UnityEngine;
using Time = UnityEngine.Time;
// ReSharper disable Unity.PreferAddressByIdToGraphicsParams

namespace ThePathfinder.Behaviors
{
    [RequireComponent(typeof(MeshRenderer), typeof(Material), typeof(MeshFilter))]
    //INVESTIGATE is this the best way to handle modularity?
    public sealed class SelectionIndicator : MonoBehaviour
    {
        public Color selectedColor;
        public Color selectionCandidateColor;
        private Renderer _renderer;

        private Material _material;

        //public EntityData data;
        public SelectableComponent selectableActor;
        private bool _isSelected;
        private bool _candidate;
        public float anglePerSec;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
            selectableActor.OnIsSelectedCandidateChanged = isCandidate =>
            {
                _candidate = isCandidate;
                if (isCandidate)
                {
                    _material.SetColor("_Color", selectionCandidateColor);
                    _renderer.enabled = isCandidate;
                }
                else if (!_isSelected)
                {
                    _renderer.enabled = isCandidate;
                }
                else if (_isSelected)
                {
                    _material.SetColor("_Color", selectedColor);
                }

                if (!isCandidate)
                {
                    _material.SetFloat("_Rotation", 0);
                }
            };
            selectableActor.OnSelectedChanged = selected =>
            {
                _material.SetColor("_Color", selectedColor);
                _renderer.enabled = _isSelected = selected;
            };
        }

        private void Update()
        {
            if (_candidate)
                _material.SetFloat("_Rotation", _material.GetFloat("_Rotation") + (anglePerSec * Time.deltaTime));
        }
    }
}
using UnityEngine;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Components.Authoring;
using Time = UnityEngine.Time;
namespace ThePathfinder
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
        private bool isSelected;
        private bool candidate;
        public float anglePerSec;
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = _renderer.material;
            selectableActor.OnIsSelectedCandidateChanged = isCandidate =>
            {
                candidate = isCandidate;
                if (isCandidate)
                {
                    _material.SetColor("_Color", selectionCandidateColor);
                    _renderer.enabled = isCandidate;
                }
                else if (!isSelected) { _renderer.enabled = isCandidate; }
                else if (isSelected) { _material.SetColor("_Color", selectedColor); }
                if (!isCandidate){_material.SetFloat("_Rotation", 0);}
            };
            selectableActor.OnSelectedChanged = selected =>
            {
                _material.SetColor("_Color", selectedColor);
                _renderer.enabled = isSelected = selected;
            };
        }
        private void Update()
        {
            if (candidate) _material.SetFloat("_Rotation", _material.GetFloat("_Rotation") + (anglePerSec * Time.deltaTime));
        }
    }
}
using System;
using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessEntityTranslation : Processor, ITick, ITickLate
    {
        private readonly Group<Translation> _group = default;
        public void Tick(float delta)
        {
            foreach (ent entity in _group)
            {
                entity.transform.transform.Translate(entity.TranslationComponent().value);
            }
        }

        public void TickLate(float dt)
        {
            foreach (ent entity in _group)
            {
                entity.TranslationComponent().value = Vector3.zero;
            }
        }
    }
}
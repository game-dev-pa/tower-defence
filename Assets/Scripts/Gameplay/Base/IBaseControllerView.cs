using System;
using UnityEngine;

namespace Gameplay.Base
{
    public interface IBaseControllerView
    {
        Transform Transform { get; }
        float BaseDistanceDamageThreshold { get; }

        event Action OnBaseDestroyedCallback;

        void Initialize(int maxBaseHealth);
    }
}
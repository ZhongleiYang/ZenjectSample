using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using ModestTree.Zenject;

namespace ModestTree.Asteroids
{
    public class Ship : ITickable, IInitializable
    {
        ShipHooks _hooks;
        ShipStateFactory _stateFactory;
        ShipState _state = null;

        public Ship(ShipHooks hooks, ShipStateFactory stateFactory)
        {
            _hooks = hooks;
            _stateFactory = stateFactory;
        }

        public MeshRenderer MeshRenderer
        {
            get
            {
                return _hooks.MeshRenderer;
            }
        }

        public AudioSource AudioSource
        {
            get
            {
                return _hooks.audio;
            }
        }

        public ParticleEmitter ParticleEmitter
        {
            get
            {
                return _hooks.ParticleEmitter;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _hooks.transform.position;
            }
            set
            {
                _hooks.transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return _hooks.transform.rotation;
            }
            set
            {
                _hooks.transform.rotation = value;
            }
        }

        public void Initialize()
        {
            _state = _stateFactory.Create(EShipState.WaitingToStart, this);
            _hooks.TriggerEnter += OnTriggerEnter;
        }

        public void Tick()
        {
            _state.Update();
        }

        void OnTriggerEnter(Collider other)
        {
            _state.OnTriggerEnter(other);
        }

        public void ChangeState(EShipState state, params object[] constructorArgs)
        {
            if (_state != null)
            {
                _state.Stop();
            }

            _state = _stateFactory.Create(state, constructorArgs);
            _state.Start();
        }
    }
}

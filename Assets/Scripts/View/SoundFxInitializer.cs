using System;
using Infrastructure;
using UnityEngine;
using VContainer;

namespace View
{
    public class SoundFxInitializer : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        private ISoundFXService _soundFXService;

        private void Awake()
        {
            this.DoSelfInjection();

            if (_source == null)
                _source = GetComponent<AudioSource>();
        }
        
        private void OnEnable()
        {
            _soundFXService.SetSource(_source);
        }

        private void OnDisable()
        {
            if (_soundFXService.IsSourceMatch(_source))
                _soundFXService.SetSource(null);
        }

        [Inject]
        private void Constructor(ISoundFXService soundFXService)
        {
            _soundFXService = soundFXService;
        }
    }
}
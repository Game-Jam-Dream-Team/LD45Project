using System;
using UnityEngine;

[Serializable]
public class ShipSound : StartStep {
	public AudioSource Sound;

	float _startVolume;

	protected override void Perform(float t) {
		Sound.outputAudioMixerGroup.audioMixer.SetFloat("ShipVolume", Mathf.Lerp(-80, _startVolume, t));
	}

	protected override void OnStart() {
		Sound.outputAudioMixerGroup.audioMixer.GetFloat("ShipVolume", out _startVolume);
		Sound.Play();
	}

	protected override void OnFinish() {
		Sound.outputAudioMixerGroup.audioMixer.SetFloat("ShipVolume", _startVolume);
		Sound.Stop();
	}
}
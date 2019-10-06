using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHolder: MonoBehaviour {
	static MusicHolder _instance;

	public float FadeInTime;

	AudioSource _source;
	float _wantedVolume;

	float _timer;

	void Awake() {
		if ( _instance ) {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		_source = GetComponent<AudioSource>();
		_source.outputAudioMixerGroup.audioMixer.GetFloat("MusicVolume", out _wantedVolume);
		_instance = this;
	}

	void Update() {
		_timer += Time.deltaTime * (1 / FadeInTime);
		if ( _timer > 1.0f ) {
			enabled = false;
		}
		var value = Mathf.Lerp(-80, _wantedVolume, _timer);
		_source.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", value);
	}
}

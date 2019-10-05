using System;
using UnityEngine;

[Serializable]
public class ShipMovement : StartStep {
	public Transform StartPosition;
	public Transform EndPosition;
	public Transform Target;
    public ParticleSystem EngineParticles;

	protected override void Perform(float t) {
		Target.transform.position = Vector3.Lerp(StartPosition.position, EndPosition.position, t);
	}

    protected override void OnFinish()
    {
        EngineParticles.Stop();
    }
}
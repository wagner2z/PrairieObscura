using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot_Effect : MonoBehaviour
{
    ControlAssignment control = new ControlAssignment();
    Light shot_light;
    ParticleSystem shot_flare;
    const float timeActive = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        shot_light = GameObject.Find("Gun Light").GetComponent<Light>();
        shot_flare = GameObject.Find("Gun Flare").GetComponent<ParticleSystem>();
        shot_light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator gunshot()
    {
        shot_light.enabled = true;
        shot_flare.Play();
        yield return new WaitForSeconds(timeActive);
        shot_light.enabled = false;
        shot_flare.Stop();
        yield return null;
    }
}

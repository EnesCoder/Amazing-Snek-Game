using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachineShake : MonoBehaviour
{
    // Start is called before the first frame update
    public float shakeTimer;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void screenShake(float intensity, float timeToShake){
        //i love copilot(thanks github!)
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = timeToShake;
    }
    private void Update() {
        if(shakeTimer > 0f){
        shakeTimer -= Time.deltaTime;
        }
        if(shakeTimer <= 0f){
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}

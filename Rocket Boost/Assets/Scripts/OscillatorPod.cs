using System;
using UnityEngine;

public class OscillatorPod : MonoBehaviour
{
    Vector3 startPos;
    Vector3 EndPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    float movementFactor;
    float previousFactor;
    [SerializeField] ParticleSystem thrustingParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        EndPos = startPos + movementVector;
        previousFactor = Mathf.PingPong(Time.time * speed, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //第一引数には、「時間の経過を表す変数」を入れることが多い。一番使うのは、以下のTime.time * speedで、１秒でspeed分、0とlengthの間を、往復する
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        if (movementFactor > previousFactor)
        {
            Thrusting();
        }
        else
        {
            NotThrusting();
        }
        previousFactor = movementFactor;
        //startPosからendPosまでの道のりの、movementFactorの割合分の位置を返す
        transform.position = Vector3.Lerp(startPos, EndPos, movementFactor);
    }

    private void NotThrusting()
    {
        thrustingParticles.Stop();
    }

    private void Thrusting()
    {
        if (!thrustingParticles.isPlaying)
        {
            thrustingParticles.Play();
        }
    }
}
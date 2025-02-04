
using UnityEngine;
using UnityEngine.InputSystem;

//一つのクラスには、ひとつのことをやらせる。複数のことをやらせない。責任を意識する。
//また、複数のクラスで、同じことをやろうとしない。プレイヤーのスピードを別々のクラスがいじるというようなことはないようにする。

//タグはできるだけ少なくする。例外的なものにタグをつける。
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] private float thrustSpeed = 1000f;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainBooster;
    [SerializeField] private ParticleSystem rightBooster;
    [SerializeField] private ParticleSystem leftBooster;


    Rigidbody rb;
    AudioSource audioSource;

    public void SetMovementEnabled(bool isEnabled)
    {
        if (isEnabled == true)
        {
            thrust.Enable();
            rotation.Enable();
        }
        else
        {
            thrust.Disable();
            rotation.Disable();
        }
    }

    private void OnEnable()
    {
        SetMovementEnabled(true);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        //Ctrl+.で関数の抽出ができ、関数を選んだ状態でF2を押すと、Renameできる（自動的に他の関数名も変わる）
        ProcessThrust();
        ProcessRotation();

    }



    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else if (thrust.enabled && rotation.enabled)
        {
            StopThrusting();
        }

    }
    private void StartThrusting()
    {
        //これを入れることで、何度もAudioが再生されて汚い音になることを防ぐ。フラグでもできるが、これのほうが複雑化しにくい。
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }



    private void ProcessRotation()
    {
        //何もしてなければ０、positiveで１、negativeで-１を返す
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput == 1)
        {
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else if (rotationInput == -1)
        {
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
        //手動で回転させているときは、物理システムによる回転を無視させる(変な挙動がなくなり、操作がしやすくなる)
        rb.freezeRotation = (rotationInput != 0) ? true : false;
        transform.Rotate(Vector3.forward * (-rotationInput) * Time.fixedDeltaTime * rotationSpeed);

        //rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.forward * (-rotationInput) * Time.fixedDeltaTime * rotationSpeed));

    }
}

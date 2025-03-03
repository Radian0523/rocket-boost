
using UnityEngine;
using UnityEngine.InputSystem;

//一つのクラスには、ひとつのことをやらせる。複数のことをやらせない。責任を意識する。
//また、複数のクラスで、同じことをやろうとしない。プレイヤーのスピードを別々のクラスがいじるというようなことはないようにする。

//タグはできるだけ少なくする。例外的なものにタグをつける。
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;
    GameObject deviceTypeManager;
    DeviceTypeManager deviceTypeMng;

    public bool isMobile;

    public float rotationValue;

    public bool doThrust = false;
    Rigidbody rb;
    AudioSource audioSource;

    public void SetMovementEnabled(bool isEnabled)
    {
        if (isEnabled == true)
        {
            thrust.Enable();//(InputAction型の実体).enabled == false の時、そのInputActionの動作は、ボタンが押されているかどうかによらずオフになる。
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

    void Awake()
    {
        deviceTypeManager = GameObject.FindWithTag("DeviceTypeManager");
        deviceTypeMng = deviceTypeManager.GetComponent<DeviceTypeManager>();
        isMobile = deviceTypeMng.GetIsMoblie();
        // isPhone = (SystemInfo.deviceType == DeviceType.Handheld);
        // isMobile = true;
        // isPhone = false;
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
        if (!isMobile)
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
        else
        {
            if (thrust.enabled)
            {
                if (doThrust)
                {
                    StartThrusting();
                }
                else if (thrust.enabled && rotation.enabled)
                {
                    StopThrusting();
                }
            }

        }

    }
    void StartThrusting()
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
    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }



    private void ProcessRotation()
    {
        if (rotation.enabled)
        {
            if (!isMobile)
            {
                //何もしてなければ０、positiveで１、negativeで-１を返す
                float rotationInput = rotation.ReadValue<float>();
                rotationValue = rotationInput;
            }

            if (rotationValue == 1)
            {
                if (!leftBooster.isPlaying)
                {
                    leftBooster.Play();
                }
            }
            else if (rotationValue == -1)
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
            // rb.freezeRotation = (rotationValue != 0) ? true : false;
            // rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * (-rotationValue) * Time.fixedDeltaTime * rotationSpeed);

            //rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.forward * (-rotationInput) * Time.fixedDeltaTime * rotationSpeed));

        }

    }
}

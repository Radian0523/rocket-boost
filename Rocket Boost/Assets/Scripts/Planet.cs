using UnityEngine;

public class Planet : MonoBehaviour
{
    enum PlayerSizeState
    {
        normal,
        wait_1,//ヒットストップみたいな感じに使う、もうちょいいい書き方あるかも
        big_1,
        wait_2,
        big_2,
        wait_3,
        big_3,
        destroy,
    }
    Rigidbody rb;
    [SerializeField] float planetPushPower = 3f;
    [SerializeField] GameObject player;
    [SerializeField] GameObject planetModel;
    PlayerSizeState currentState;
    float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * planetPushPower);

        player = GameObject.FindWithTag("Player");

        currentState = PlayerSizeState.normal;
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerSizeState.normal:
                break;
            case PlayerSizeState.wait_1:
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    timer = 0;
                    currentState++;
                }
                break;
            case PlayerSizeState.big_1:
                player.transform.localScale = (player.transform.localScale.x + 0.2f) * Vector3.one;
                currentState++;
                Debug.Log("big1");
                break;
            case PlayerSizeState.wait_2:
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    timer = 0;
                    currentState++;
                }
                break;
            case PlayerSizeState.big_2:
                player.transform.localScale = (player.transform.localScale.x + 0.2f) * Vector3.one;
                currentState++;
                Debug.Log("big2");
                break;
            case PlayerSizeState.wait_3:
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    timer = 0;
                    currentState++;
                }
                break;
            case PlayerSizeState.big_3:
                player.transform.localScale = (player.transform.localScale.x + 0.2f) * Vector3.one;//なぜかシーンから抜けたり、シーンを再ロードしたりすると、これが適用される。しかもゲーム内で変えた値なのに、ゲームからEscapeした後も値が保存されてるなんで
                currentState++;
                Debug.Log("big3");
                break;
            case PlayerSizeState.destroy:
                currentState++;
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && currentState == PlayerSizeState.normal)
        {
            currentState = PlayerSizeState.wait_1;
            // planetModel.GetComponent<Renderer>().enabled = false;
            LODGroup lodGroup = planetModel.GetComponent<LODGroup>();
            if (lodGroup != null)
            {
                foreach (var renderer in lodGroup.GetLODs())
                {
                    foreach (var r in renderer.renderers)
                    {
                        r.enabled = false;  // 各 LOD の Renderer を無効化
                    }
                }
            }
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}

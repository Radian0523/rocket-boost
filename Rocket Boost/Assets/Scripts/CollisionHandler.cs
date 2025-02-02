using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] private float timeToLoadScene = 2f;//SceneLoadDelayの方がよかったかも。DelayとTimeは意識的に変数名で分けるべきだな
    [SerializeField] private AudioClip deathExplosion;
    [SerializeField] private AudioClip success;

    bool isControllable = true;
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        GetComponent<Movement>().SetMovementEnabled(false);
        audioSource.PlayOneShot(success);
        isControllable = false;
        Invoke("LoadNextLevel", timeToLoadScene);

    }

    private void StartCrashSequence()
    {
        audioSource.Stop();
        GetComponent<Movement>().SetMovementEnabled(false);
        audioSource.PlayOneShot(deathExplosion);
        isControllable = false;

        //第一引数の関数を、第二引数秒分待ってから、呼び出す
        Invoke("ReloadLevel", timeToLoadScene);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneNum = SceneManager.sceneCountInBuildSettings;
        //永遠とステージを回る感じになっている。
        if (currentSceneIndex + 1 == sceneNum)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }


    }
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

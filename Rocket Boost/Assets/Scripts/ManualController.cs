using UnityEngine;

public class ManualController : MonoBehaviour
{
    [SerializeField] GameObject manualContainer;
    [SerializeField] GameObject defaultCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manualContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressManualButton()
    {
        bool current = manualContainer.activeSelf;
        manualContainer.SetActive(!current);
        defaultCanvas.SetActive(current);
    }
}

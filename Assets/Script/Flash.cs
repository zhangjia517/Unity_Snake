using UnityEngine;

public class Flash : MonoBehaviour
{
    public Flash self;
    public float interval;

    private void Awake()
    {
        self = this;
        Fflash();
    }

    public void Fflash()
    {
        InvokeRepeating("FlashLabel", 0, interval);
    }

    private void FlashLabel()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
        CancelInvoke("FlashLabel");
    }
}
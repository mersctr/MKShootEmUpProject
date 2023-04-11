using UnityEngine;
using Zenject;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivityBackButton : MonoBehaviour
{
    [Inject] private Activity _activity;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BackActivity);
    }

    private void BackActivity()
    {
        _activity.Back();
    }
}

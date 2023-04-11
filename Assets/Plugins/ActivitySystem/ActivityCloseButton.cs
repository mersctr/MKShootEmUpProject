using UnityEngine;
using Zenject;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivityCloseButton : MonoBehaviour
{
    [Inject] private Activity _activity;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseActivity);
    }

    private void CloseActivity()
    {
        _activity.Finish();
    }
}

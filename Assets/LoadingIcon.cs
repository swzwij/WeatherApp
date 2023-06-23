using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    private const float ROTATION_SPEED = 250;

    private RectTransform _rectTransform;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    private void Update()
    {
        if(transform.parent.gameObject.activeSelf)
            _rectTransform.Rotate(0f, 0f, -ROTATION_SPEED * Time.deltaTime);
    }
}

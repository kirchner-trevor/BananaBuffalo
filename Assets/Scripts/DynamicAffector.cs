using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAffector : MonoBehaviour
{
    public void Clear(GameGridSpace space)
    {
        space.Clear();
    }

    public void Show(Transform transform)
    {
        transform.gameObject.SetActive(true);
    }

    public void Hide(Transform transform)
    {
        transform.gameObject.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EnemiesDeathVFXPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemiesVFX;

    public GameObject PopVFX() {
        if(_enemiesVFX.Count == 0) {
            return null;
        }
        GameObject vfx = _enemiesVFX[0];
        _enemiesVFX.RemoveAt(0);
        return vfx;
    }

    public void PushVFX(GameObject vfx) {
        vfx.SetActive(false);
        _enemiesVFX.Add(vfx);
    }
}

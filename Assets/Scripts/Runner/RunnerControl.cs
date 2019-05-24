using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerControl : PlayerBasicControl
{
    float tileSize;
    bool _moved = false;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _delay;

    private void Start()
    {
        tileSize = DungeonMap.tileSize;
    }
    protected override void MovePlayer()
    {
        SwitchDirections();
        if (!_moved)
            StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        _moved = true;
        characterController.Move(transform.forward * tileSize);
        yield return new WaitForSeconds(_delay);
        _moved = false;
    }

    private void SwitchDirections()
    {
        if (Input.GetKeyDown(KeyCode.W))
            transform.Rotate(Vector3.zero);
        if (Input.GetKeyDown(KeyCode.S))
            transform.Rotate(Vector3.up * 180);
        if (Input.GetKeyDown(KeyCode.A))
            transform.Rotate(Vector3.up * -90);
        if (Input.GetKeyDown(KeyCode.D))
            transform.Rotate(Vector3.up * 90);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private CharacterStateChannel _bossStateChannel;
    [SerializeField] private float _delayTime = 1f;
    private bool _activated = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated) return;

        if (other.CompareTag("Player"))
        {
            _activated = true;
            Debug.Log("Activated");
            StartCoroutine(DelayActive());
        }
    }

    private IEnumerator DelayActive()
    {
        yield return new WaitForSeconds(_delayTime);
        _bossStateChannel.SendEventMessage(CharacterState.Awake);
    }
}

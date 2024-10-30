using UnityEngine;
using UnityEngine.InputSystem;

public class TestBomber : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            bomb.ThrowBomb(new Vector2(-5f, 6f), 3f);
        }
    }
}

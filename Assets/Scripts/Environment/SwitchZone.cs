using System.Collections;
using UnityEngine;

public class SwitchZone : MonoBehaviour
{
    [Header("Zone")]
    [SerializeField] private Transform _zoneWanted;
    private bool _used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        if (player != null && !_used)
        {
            StartCoroutine(SwitchingZone(player));
        }
    }

    public IEnumerator SwitchingZone(PlayerManager player)
    {
        _used = true;
        StartCoroutine(player.OnFade());
        yield return new WaitForSecondsRealtime(1f);
        player.transform.position = _zoneWanted.position;
        _used = false;
    }
}

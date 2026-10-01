using UnityEngine;

public class OneWayScript : MonoBehaviour
{
    public string oneWayPlatformLayerName = "OneWayPlatform";
    public string playerLayerName = "Player";
    private int playerLayer;
    private int platformLayer;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer(playerLayerName);
        platformLayer = LayerMask.NameToLayer(oneWayPlatformLayerName);
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        }
    }
}
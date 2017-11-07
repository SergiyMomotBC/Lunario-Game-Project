using UnityEngine;

public class PowerUpPicked : MonoBehaviour
{
    public PowerUpType type;

    private AudioSource sound;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        sound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        { 
            switch(type)
            {
                case PowerUpType.Invincible:
                    if (other.GetComponent<PowerUp>() == null) {
                        var powerUp = other.gameObject.AddComponent<InvinciblePowerUp>();
                        powerUp.powerUpIcon = spriteRenderer.sprite;
                    }
                    break;
                case PowerUpType.FreezeEnemies:
                    if (other.GetComponent<PowerUp>() == null) {
                        var powerUp = other.gameObject.AddComponent<FreezeEnemiesPowerUp>();
                        powerUp.powerUpIcon = spriteRenderer.sprite;
                    }
                    break;
                case PowerUpType.SpeedUp:
                    if (other.GetComponent<PowerUp>() == null) {
                        var powerUp = other.gameObject.AddComponent<SpeedUpPowerUp>();
                        powerUp.powerUpIcon = spriteRenderer.sprite;
                    }
                    break;
                case PowerUpType.Magnet:
                    if (other.GetComponent<PowerUp>() == null) {
                        var powerUp = other.gameObject.AddComponent<MagnetPowerUp>();
                        powerUp.powerUpIcon = spriteRenderer.sprite;
                    }
                    break;
                case PowerUpType.Heart:
                    other.gameObject.GetComponent<PlayerLifeController>().AddHP();
                    break;
                case PowerUpType.BonusTime:
                    GameObject.Find("HUD").GetComponent<HUDcontroller>().AddTime(60);
                    break;
            }

            sound.Play();
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(gameObject, sound.clip.length + 0.1f);
        }
    }
}

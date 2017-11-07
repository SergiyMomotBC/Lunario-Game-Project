using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeController : MonoBehaviour 
{
    public int lives { get; private set; }
    public int healthPoints { get; private set; }

    public bool isInvincible;

    private bool wasHurtRecently;
    private float timer;
    private SpriteRenderer sprite;
    private ShakeCamera mainCamera;
    private PlayerController player;

    private const float KILLZONE_Y = -5.4f;
    private const float INVINCIBLE_PERIOD = 3.0f;
    private const short STARTING_HP = 3;
    private const short MAX_HP = 3;
    private const short MAX_LIVES = 12;

	public void Awake() 
    {
        healthPoints = STARTING_HP;
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main.GetComponent<ShakeCamera>();
        player = GetComponent<PlayerController>();

        lives = Game.PlayerStats.lives;
    }
	
	public void FixedUpdate() 
    {
        if (Time.timeScale == 0.0f)
            return;

        if (transform.position.y < KILLZONE_Y || healthPoints <= 0)
            OnDeath(1);

        if (wasHurtRecently)
        {
            timer += Time.deltaTime;

            Color tmp = sprite.color;
            tmp.a = 0.25f + Mathf.PingPong(Time.time * 2, 0.75f);

            if (timer >= INVINCIBLE_PERIOD)
            {
                wasHurtRecently = false;
                timer = 0.0f;
                tmp.a = 1.0f;
            }

            sprite.color = tmp;
        }
	}

    public void AddHP()
    {
        if (healthPoints < MAX_HP)
            healthPoints++;
    }

    public void AddLife()
    {
        if (lives < MAX_LIVES)
            lives++;
    }

    public void WasHurt()
    {
        if (!wasHurtRecently && !isInvincible)
        {
            healthPoints--;
            wasHurtRecently = true;
            mainCamera.Shake();
            player.hurtAudio.Play();
        }
    }

    public void OnDeath(int failureCode)
    {
        lives--;
        var manager = FindObjectOfType<GameplayManager>();
        manager.Pause();
        manager.enabled = false;
        manager.failureCode = failureCode;
        SceneManager.LoadScene("Death Screen", LoadSceneMode.Additive);
    }
}
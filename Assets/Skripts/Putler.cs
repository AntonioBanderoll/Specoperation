using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Putler : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image fillImage;
    [SerializeField] private Text text;
    private int health;
    private Rigidbody[] rbs; 
    private Animator anim;
    private int deathCount;
    public static Putler Instance;
    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        rbs = GetComponentsInChildren<Rigidbody>();
        health = maxHealth;
        if (PlayerPrefs.HasKey("DeathCount"))
        {
            deathCount = PlayerPrefs.GetInt("DeathCount");
        }
        else
        {
            deathCount = 0;
            PlayerPrefs.SetInt("DeathCount", deathCount);
        }
        text.text = deathCount.ToString();
    }
    private void Kick()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].velocity = new Vector3(0, 2, -1);
        }
    }
    private void Death()
    {
        anim.enabled = false;
        Kick();
        anim.Rebind();
        anim.Update(0);
        Invoke("Revive", 2.5f);
        deathCount++;
        PlayerPrefs.SetInt("DeathCount", deathCount);
        text.text = deathCount.ToString();
    }

    private void Revive()
    {
        anim.enabled = true;
        SetHealth(maxHealth);
    }
    public void Hit(BodyPart bodyPart, int damage)
    {
        if (health > 0)
        {
            anim.SetTrigger(bodyPart.ToString());
            if (health - damage > 0)
            {
                SetHealth(health - damage);
            }
            else
            {
                SetHealth(0);
                //Death();
                Invoke("Death", 0.375f);
            }
        }
    }
   
    private void SetHealth(int newHealth)
    {
        health = newHealth;
        fillImage.fillAmount = (float)health / maxHealth;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            Death();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            Revive();
        }
    }
}

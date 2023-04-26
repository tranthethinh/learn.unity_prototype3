using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private int numJump;
    private int core1 = 0;
    private float timeAddCore;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        if (time > 5) { playerAnim.SetFloat("speed", 0.6f); }
        if (Input.GetKey(KeyCode.F))
        {
            playerAnim.speed = 4;
            timeAddCore = 0.5f;
        }
        else
        {
            playerAnim.speed = 1;
            timeAddCore = 1.0f;
        }
        if (isOnGround) { numJump = 0; }
        if (transform.position.x < 0)
        {
            WalkToStartingPoint();

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !gameOver && numJump < 2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            numJump++;
        }

        if (time > timeAddCore && !gameOver)
        {

            core1 = core1 + 5;
            time = 0;
            print(core1 + "thinh");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("GameOver");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
    private void WalkToStartingPoint()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 3.0f, Space.World);
        // change animation from walking to running & start dirt animation
        if (!(transform.position.x < 0))
        {
            playerAnim.SetFloat("Speed_f", 2);
            dirtParticle.Play();
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public float speed = 5;
    public bool loseGame = false;
    public LayerMask holdLayer;

    private Animator anim;
    private Rigidbody2D rgdb;

    private bool frontToRight = true;
    private bool upping = false;
    private bool actionHold = false;
    private bool holdAction = false;
    private bool winLevel = false;
    private bool moving_x = false;

    private float timeToHitMe = 0;
    private float timeToBright = -10;
    private float corre = 1;
    
    private PlayerAudios p_audios;
    private PlayerInteracts p_interacts;
    private SlotsProtectController p_protect;
    private PlayerAnimations p_animations;
    
    //Gets e Sets
    public Animator Anim { get { return anim; } }
    public Rigidbody2D Rgdb { get { return rgdb; } }
    public bool FrontToRight { get { return frontToRight; } }
    public bool WinLevel { get { return winLevel; } }
    public bool HoldAction { get { return holdAction; } set { holdAction = value; } }
    public BoxController BoxHold { get { return GetComponent<PlayerInteracts>().BoxHold; } }
    public bool Moving_x { get { return moving_x; } }

    //Strings fixas
    private string insurable = "Insurable";

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        anim = transform.parent.GetComponent<Animator>();
        rgdb = transform.parent.GetComponent<Rigidbody2D>();
        p_audios = GetComponent<PlayerAudios>();
        p_interacts = GetComponent<PlayerInteracts>();
        p_protect = transform.FindChild("protect").GetComponent<SlotsProtectController>();
        p_animations = GetComponent<PlayerAnimations>();
        
    }
	
	void FixedUpdate () {

        if (rgdb.velocity.x > 0.05f)
            moving_x = true;
        else
            moving_x = false;
        
        verifyInputs();
        attAnimValues();
        
        if (timeToHitMe > 0)
            timeToHitMe -= Time.deltaTime;

        if(timeToHitMe < 0 && timeToHitMe > -10) {
            p_animations.brightMe(false);
            timeToHitMe = -10;
        }

        if (rgdb.velocity.y < 0 && upping && !p_interacts.Grounded) {
            anim.SetTrigger("jumpMax");
            upping = false;
        }
        
	}

    public void hitMe(int value) {
        if (timeToHitMe > 0)
            return;
        p_interacts.hitMe(value, loseGame);
        timeToHitMe = 2;
        p_animations.brightMe(true);
    }

    public void letHold() {
        p_interacts.letHold();
    }

    public void loseNow() {
        loseGame = true;
        SceneManager.instance.loseGame();
        p_audios.playAudio(0, 4);
    }
    
    private void verifyInputs() {

        if (loseGame || winLevel) {
            rgdb.velocity = new Vector2(0,rgdb.velocity.y);
            return;
        }

        float axis_h = Input.GetAxis("Horizontal");
        float axis_v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            corre = 4;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            corre = 1;
        
        //if ((p_interacts.Bumping || (Mathf.Abs(axis_h) > 0.8f && Mathf.Abs(rgdb.velocity.x) <= 0.05f)) && !p_interacts.Grounded)
        if (p_interacts.Bumping && ((frontToRight && axis_h > 0) || (!frontToRight && axis_h < 0)))
                axis_h = 0;
        
         rgdb.velocity = new Vector2(axis_h * speed * corre, rgdb.velocity.y);

        if ((rgdb.velocity.x < 0 && frontToRight) || (rgdb.velocity.x > 0 && !frontToRight))
            flip();

        if (Input.GetButtonDown("Jump") && p_interacts.Grounded) {
            if(!holdAction)
                rgdb.velocity = new Vector2(rgdb.velocity.x, 9);
            else
                rgdb.velocity = new Vector2(rgdb.velocity.x, 7);

            upping = true;
            anim.SetTrigger("jump");
            p_audios.playAudio(0, 0);
        }

        if (Input.GetButtonDown("Fire1")) {
            RaycastHit2D hit;
            if (frontToRight)
                hit = Physics2D.Raycast(transform.position, new Vector2(1,0), 1, holdLayer);
            else
                hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 1, holdLayer);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals(insurable)) {
                p_interacts.holdObject(hit.collider.gameObject);
            }
            
            holdAction = true;
        }

        if (Input.GetButtonUp("Fire1")) {
            holdAction = false;
        }

        if (axis_v < 0 && !p_interacts.Grounded)
            rgdb.velocity = new Vector2(rgdb.velocity.x, -12);
        
    }
    
    private void flip() {
        Transform t = transform;
        t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
        frontToRight = !frontToRight;
        SceneManager.instance.flipCamera();
    }

    private void attAnimValues() {
        anim.SetFloat("velocity", Mathf.Abs(rgdb.velocity.x));
    }

    private void activeHold() {
        actionHold = true;
    }

    private void desactiveHold() {
        actionHold = false;
    }

    public void addProtect(ProtectController pc) {
        p_protect.addProtect(pc);
    }

    public void removeProtect() {

    }

    public void playAudio(EnumsGame.AUDIOS audio) {
        if (audio.Equals(EnumsGame.AUDIOS.P_JUMP)) {
            p_audios.playAudio(0, 0);
        } else if (audio.Equals(EnumsGame.AUDIOS.COLLECT_COIN)) {
            p_audios.playAudio(0, 1);
        } else if (audio.Equals(EnumsGame.AUDIOS.WIN_LEVEL)) {
            p_audios.playAudio(0, 2);
        } else if (audio.Equals(EnumsGame.AUDIOS.COLLECT_CRYSTAL)) {
            p_audios.playAudio(0, 3);
        } else if (audio.Equals(EnumsGame.AUDIOS.LOSE_LEVEL)) {
            p_audios.playAudio(0, 4);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        
        if (col.gameObject.tag.Equals("EndLevel") && !winLevel) {
            winLevel = true;
            SceneManager.instance.endLevel();
            p_audios.playAudio(0, 2);
        }

        if (col.gameObject.tag.Equals("LoseArea")) {
            if (!loseGame) {
                loseNow();
            }
        }
        
    }

    //.............
    void OnTriggerStay2D(Collider2D col) {
        if((col.gameObject.name.Equals("upperLimit") || col.gameObject.name.Equals("bottonLimit")) && rgdb.velocity.y != 0){
            col.transform.parent.parent.GetComponent<SceneManager>().moveCameraVertical();

            //col.transform.parent.parent.GetComponent<SceneManager>().moveCameraVertical();
        }
        
    }
    
}

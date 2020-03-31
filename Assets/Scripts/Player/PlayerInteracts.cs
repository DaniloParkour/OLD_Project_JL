using UnityEngine;
using System.Collections;

public class PlayerInteracts : MonoBehaviour {

    public LayerMask whatIsGround;
    public LayerMask whatIsBump;
    public LayerMask whatHitMe;

    private bool grounded;
    private bool bumping;
    
    private BoxController boxHold = null;
    private Transform groundCheck;
    private Transform bumpCheck;

    //Strings to test
    private string str_coin = "Coin";
    private string str_crystal = "Crystal";
    private string str_protect = "Protect";

    //Gets e Sets
    public bool Grounded { get { return grounded; } }
    public bool Bumping { get { return bumping; } }
    public BoxController BoxHold { get { return boxHold; } }
    
    // Use this for initialization
    void Start () {
        groundCheck = transform.FindChild("groundCheck");
        bumpCheck = transform.FindChild("bumpCheck");
    }
	
	// Update is called once per frame
	void Update () {
        casts_checks();
        interactions();
    }

    public void hitMe(int value, bool loseGame) {
        if (loseGame)
            return;

        PlayerController.instance.Rgdb.velocity = new Vector2(-3,6);
        if (transform.FindChild("protect").GetComponent<SlotsProtectController>().QuantProtect > 0)
            //if (PlayerData.instance.turnData.crystals >= value)
            //SceneManager.instance.removeCrystal(value);
            transform.FindChild("protect").GetComponent<SlotsProtectController>().removeProtect();
        else {
            //SceneManager.instance.removeCrystal(PlayerData.instance.turnData.crystals);
            PlayerController.instance.loseNow();
        }
    }

    public void holdObject(GameObject go) {
        boxHold = go.GetComponent<BoxController>();
        if (boxHold != null) {
            boxHold.holdMe();
            boxHold.gameObject.layer = 14;
            PlayerController.instance.Anim.SetBool("holding", true);
        }
    }

    public void letHold() {
        PlayerController.instance.HoldAction = false;
        if (boxHold != null)
            boxHold.gameObject.layer = 15;
        boxHold = null;
        PlayerController.instance.Anim.SetBool("holding", false);
    }

    /*Some interactions. The first implementes is hold boxes*/
    private void interactions() {
        if(boxHold != null) {
            if(PlayerController.instance.FrontToRight)
                boxHold.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            else
                boxHold.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

            boxHold.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

            if (!PlayerController.instance.HoldAction && boxHold != null) {
                boxHold.gameObject.layer = 15;
                boxHold = null;
                PlayerController.instance.Anim.SetBool("holding", false);
            }
        }
    }
    
    private void casts_checks() {
        //grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, whatIsGround);
        Vector2 a = new Vector2(groundCheck.position.x - 0.15f, groundCheck.position.y);
        Vector2 b = new Vector2(groundCheck.position.x + 0.15f, groundCheck.position.y - 0.1f);

        Vector2 c = new Vector2(a.x, b.y);
        Vector2 d = new Vector2(b.x, a.y);

        grounded = Physics2D.OverlapArea(a,b,whatIsGround);
        PlayerController.instance.Anim.SetBool("grounded", grounded);

        Debug.DrawLine(a, c);
        Debug.DrawLine(a, d);
        Debug.DrawLine(b, c);
        Debug.DrawLine(b, d);

        a = new Vector2(bumpCheck.position.x - 0.1f, bumpCheck.position.y - 0.66f);
        b = new Vector2(bumpCheck.position.x + 0.1f, bumpCheck.position.y + 0.66f);
        
        bumping = Physics2D.OverlapArea(a, b, whatIsBump);

        c = new Vector2(a.x, b.y);
        d = new Vector2(b.x, a.y);
        
        Debug.DrawLine(a, c, Color.red);
        Debug.DrawLine(a, d, Color.red);
        Debug.DrawLine(b, c, Color.yellow);
        Debug.DrawLine(b, d, Color.yellow);
        
        //if (!bumping && !grounded && Mathf.Abs(PlayerController.instance.Rgdb.velocity.y) <= 0.2f)
          //  bumping = true;

        
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (whatHitMe == (1 << col.gameObject.layer) && !PlayerController.instance.loseGame) {
            //PlayerController.instance.Rgdb.velocity = new Vector2(-2, 8);
            PlayerController.instance.hitMe(1);
        }

        if (col.gameObject.tag.Equals(str_coin)) {
            col.GetComponent<ItemController>().collectMe();
            SceneManager.instance.addCoin(1);
            col.GetComponent<CircleCollider2D>().enabled = false;
            PlayerController.instance.playAudio(EnumsGame.AUDIOS.COLLECT_COIN);
        } else if (col.gameObject.tag.Equals(str_crystal)) {
            col.gameObject.SetActive(false);
            SceneManager.instance.addCrystals(1);
            PlayerController.instance.playAudio(EnumsGame.AUDIOS.COLLECT_CRYSTAL);
        } else if (col.gameObject.tag.Equals(str_protect)) {
            PlayerController.instance.addProtect(col.gameObject.GetComponent<ProtectController>());
        }
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotsProtectController : MonoBehaviour {

    public int maxProtect = 4;

    private List<ProtectController> l_protect;
    private int quantProtect = 0;

    public int QuantProtect { get { return quantProtect; }}

	// Use this for initialization
	void Start () {
        l_protect = new List<ProtectController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 120 * Time.deltaTime);
    }

    public void addProtect(ProtectController pc) {
        if(l_protect.Count < maxProtect) {
            pc.initMe(transform.GetChild(l_protect.Count));
            l_protect.Add(pc);
            quantProtect++;
        }
    }

    public void removeProtect() {
        if(l_protect.Count > 0) {
            ProtectController pc_remove = l_protect[0];
            l_protect.Remove(pc_remove);
            pc_remove.removeMe();
            pc_remove.transform.parent = pc_remove.transform.parent.parent;
            pc_remove.GetComponent<SpriteRenderer>().sortingOrder = 200;
            quantProtect--;

            //Ajustar posições trazendo os protects pra o inicio do circulo.
            for (int i = 0; i < l_protect.Count; i++)
                l_protect[i].initMe(transform.GetChild(i));

        }
    }

}

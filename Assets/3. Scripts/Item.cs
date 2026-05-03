using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager m;

    new TMP_Text name;
    TMP_Text much;
    bool isSword;

    // Start is called before the first frame update
    void Start()
    {
        m = GameObject.Find("GameManager").GetComponent<GameManager>();
        name = gameObject.transform.Find("name").GetComponentInChildren<TMP_Text>();
        much = gameObject.transform.Find("much").GetComponentInChildren<TMP_Text>();

        if(gameObject.transform.parent.gameObject.name == "Content")
        {
            isSword = true;
        }
        else
        {
            isSword = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isSword)
        {
            much.text = $"x{m.ivswords[name.text]}";
        }
        else
        {
            much.text = $"x{m.ivitems[name.text]}";
        }
    }
}

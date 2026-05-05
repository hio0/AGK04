using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameManager m;

    new TMP_Text name;
    TMP_Text much;
    bool isSword;
    public Button b;

    public int whatLv;

    // Start is called before the first frame update
    void Start()
    {
        m = GameObject.Find("GameManager").GetComponent<GameManager>();
        name = gameObject.transform.Find("name").GetComponentInChildren<TMP_Text>();
        much = gameObject.transform.Find("much").GetComponentInChildren<TMP_Text>();
        b = gameObject.transform.Find("name/use").GetComponentInChildren<Button>();
        b.onClick.AddListener(Use);

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

    public void Use()
    {
        if(isSword)
        {
            m.ivswords[name.text]--;
            if (m.ivswords[name.text] <= 0)
            {
                m.ivswords.Remove(name.text);
                Destroy(gameObject);
            }

            m.plus = whatLv;
            m.SetSword();
        }
        else
        {
            m.ivitems[name.text]--;
            if(m.ivitems[name.text] <= 0)
            {
                m.ivitems.Remove(name.text);
                Destroy(gameObject);
            }

            m.plus = whatLv;
            m.SetSword();
        }
    }
}

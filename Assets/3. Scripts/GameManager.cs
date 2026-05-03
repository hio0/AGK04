using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public GameObject firstP;
    public GameObject secondP;
    public GameObject breakP;

    public Sprite[] swords = new Sprite[15];
    public Image sword;
    public TMP_Text swordname;
    public int swordPlusmoney;
    public int swordmoney;
    public int swordbangji;
    public GameObject button;
    public Transform itemIv;
    public Transform swordIv;
    public GameObject saveitem;
    public GameObject savesword;
    public Dictionary<string, int> ivswords = new Dictionary<string, int>();
    public Dictionary<string, int> ivitems = new Dictionary<string, int>();
    public Transform itemP;
    public int whatLv;

    public TMP_Text sucsessT;
    public TMP_Text moneyT;
    public TMP_Text swordMT;
    public TMP_Text swordPMT;
    public TMP_Text bangjiT;
    public TMP_Text plusT;
    public TMP_Text noticeT;

    public int plus = 0;
    public int sucsess;
    public int money;
    public int bangji;
    public int warp;

    // Start is called before the first frame update
    void Start()
    {
        SetSword();
        money = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        sword.sprite = swords[plus];

        swordMT.text = $"판매비용: {swordmoney.ToString()}원";
        swordPMT.text = $"강화비용: {swordPlusmoney.ToString()}원";
        sucsessT.text = $"성공확률: {sucsess.ToString()}%";
        moneyT.text = "돈: " + money + "원";
        bangjiT.text = "방지권: " + bangji + "개";
    }

    public void GetMouseBD()
    {
        int a = 0;
        if (plus > 0)
        {
            a = Random.Range(1, 101);
        }
        else
        {
            a = 1;
        }
        Debug.Log(a);
        if (a > sucsess)
        {
            breakP.SetActive(true);
            noticeT.text = $"방지권을 {swordbangji}개 소모해 다시 강화를 이어갈 수 있습니다";
            plusT.text = $"현재 단계: +{plus}강";
        }
        else
        {
            money -= swordPlusmoney;
            plus++;
        }

        SetSword();
    }

    void SetSword()
    {
        if (plus == 0)
        {
            firstP.SetActive(true);

            SwordData(100, "평범검", 0, 300, 1);
        }
        else if (plus == 1)
        {
            firstP.SetActive(false);
            secondP.SetActive(true);

            SwordData(99, "강화된 평범검", 400, 300, 1);
        }
        else if (plus == 2)
        {
            SwordData(95, "묶인 평범검", 800, 500, 1);
        }
        else if (plus == 3)
        {
            SwordData(90, "양날검", 1000, 600, 1);
        }
        else if (plus == 4)
        {
            SwordData(85, "앱솔칼리버", 1400, 1200, 1);
        }
        else if (plus == 5)
        {
            SwordData(80, "특검", 3200, 1700, 1);
        }
        else if (plus == 6)
        {
            SwordData(80, "마왕이 10년 쓰다 버린 검", 6500, 2000, 1);
        }
        else if (plus == 7)
        {
            SwordData(75, "마체테?", 7800, 2300, 2);
        }
        else if (plus == 8)
        {
            SwordData(70, "곡곡곡도", 9000, 2800, 2);
        }
        else if (plus == 9)
        {
            SwordData(70, "마왕의 새 검", 13000, 3200, 2);
        }
        else if (plus == 10)
        {
            SwordData(65, "MZ세대 마왕의 검", 26000, 3600, 3);
        }
        else if (plus == 11)
        {
            SwordData(50, "주작의 깃", 48000, 4000, 4);
            button.SetActive(true);
        }
        else if (plus == 12)
        {
            SwordData(40, "뉴비 용사의 검", 70000, 5000, 6);
        }
        else if (plus == 13)
        {
            SwordData(35, "중수 뉴비 용사의 검", 100000, 7000, 8);
        }
        else if (plus == 14)
        {
            SwordData(1, "도금검", 200000, 8000, 10);
            button.SetActive(true);
        }
    }

    void SwordData(int a, string b, int c, int d, int e)
    {
        sucsess = a;
        swordname.text = $"+{plus} {b}";
        swordmoney = c;
        swordPlusmoney = d;
        swordbangji = e;
    }

    void BackToTeCho()
    {
        firstP.SetActive(false);
        secondP.SetActive(false);
        button.SetActive(false);
        plus = 0;
    }

    public void Sell()
    {
        money += swordmoney;
        BackToTeCho();
        SetSword();
    }

    public void SaveSword()
    {
        bool isit = false;

        GameObject now;

        foreach (KeyValuePair<string, int> sword in ivswords)
        {
            if (sword.Key == swordname.text)
            {
                isit = true;
            }
        }

        if (isit)
        {
            ivswords[swordname.text]++;
        }
        else
        {
            ivswords.Add(swordname.text, 1);
            Instantiate(savesword, swordIv);

            now = swordIv.GetChild(ivswords.Count - 1).gameObject; // 프리팹을 직접 통제
            now.transform.Find("name").GetComponentInChildren<TMP_Text>().text = swordname.text;
        }

        BackToTeCho();
        SetSword();
    }

    public void SaveItem(int num)
    {
        bool isit = false;

        GameObject now;
        string a = itemP.GetChild(num).gameObject.transform.Find("Text (TMP)").GetComponentInChildren<TMP_Text>().text;

        foreach (KeyValuePair<string, int> item in ivitems)
        {
            if (item.Key == a)
            {
                isit = true;
            }
        }

        if (isit)
        {
            ivitems[a]++;
        }
        else
        {
            ivitems.Add(a, 1);
            Instantiate(saveitem, itemIv);

            now = itemIv.GetChild(ivitems.Count - 1).gameObject; // 프리팹을 직접 통제
            now.transform.Find("name").GetComponentInChildren<TMP_Text>().text = a;
        }
    }

    public void Break()
    {
        BackToTeCho();
        SetSword();
    }

    public void Bangji()
    {
        if(bangji > 0)
        {
            bangji -= swordbangji;
            breakP.SetActive(false);
        }
    }

    public void Buy(int num)
    {
        int m = 0;

        if (num == 0)
        {
            m = 8000;
        }
        else if (num == 1)
        {
            m = 12000;

            bangji += 3;
        }
        else if (num == 2)
        {
            m = 12000;
        }
        else if (num == 3)
        {
            m = 18000;
        }
        else if (num == 4)
        {
            m = 26000;
        }

        if (money >= m)
        {
            if (num < 2)
            {
                bangji++;
            }
            else
            {
                SaveItem(num);
            }
            money -= m;
        }
    }
}

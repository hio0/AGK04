using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public TMP_Text sucsessT;
    public TMP_Text moneyT;
    public TMP_Text swordMT;
    public TMP_Text swordPMT;

    public int plus = 0;
    public int sucsess;
    public int money;
    public int bangji;

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
    }

    public void GetMouseBD()
    {
        int a = 0;
        if (plus > 0)
        {
            a = Random.Range(0, 101);
        }
        else
        {
            a = 1;
        }
        Debug.Log(a);
        if (a > sucsess)
        {
            breakP.gameObject.SetActive(true);
            if (bangji > 0)
            {
                bangji--;
                Debug.Log($"방지권을 소모해 파괴를 막았다...!!(남은 방지권: {bangji})");
            }
            else
            {
                BackToTeCho();
            }
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
        if(plus == 0)
        {
            firstP.gameObject.SetActive(true);

            SwordData(100, "평범검", 0, 300);
        }
        else if(plus == 1)
        {
            firstP.gameObject.SetActive(false);
            secondP.gameObject.SetActive(true);

            SwordData(99, "강화된 평범검", 150, 300);
        }
        else if (plus == 2)
        {
            SwordData(98, "묶인 평범검", 400, 500);
        }
        else if (plus == 3)
        {
            SwordData(95, "양날검", 500, 600);
        }
        else if (plus == 4)
        {
            SwordData(90, "신의 은검", 700, 1200);
        }
        else if (plus == 5)
        {
            SwordData(85, "본질", 1000, 1700);
        }
        else if (plus == 6)
        {
            SwordData(80, "아머소드", 1300, 2000);
        }
        else if (plus == 7)
        {
            SwordData(80, "재본질", 1600, 2500);
        }
        else if (plus == 8)
        {
            SwordData(75, "곡곡곡도", 1900, 3100);
        }
        else if (plus == 9)
        {
            SwordData(70, "시퍼런", 2200, 3500);
        }
        else if (plus == 10)
        {
            SwordData(70, "이차원의 검", 2600, 4000);
        }
        else if (plus == 11)
        {
            SwordData(60, "불사조 깃", 3200, 4200);
        }
        else if (plus == 12)
        {
            SwordData(60, "싹트는 것", 4200, 5000);
        }
        else if (plus == 13)
        {
            SwordData(60, "PEAK", 6000, 7000);
        }
        else if (plus == 14)
        {
            SwordData(50, "마지막 시련", 10000, 0);
        }
    }

     void SwordData(int a, string b, int c, int d)
    {
        sucsess = a;
        swordname.text = $"+{plus} {b}";
        swordmoney = c;
        swordPlusmoney = d;
    }

    void BackToTeCho()
    {
        firstP.gameObject.SetActive(false);
        secondP.gameObject.SetActive(false);
        plus = 0;
    }

    public void Sell()
    {
        money += swordmoney;
        BackToTeCho();
        SetSword();
    }

    public void Bangji()
    {
        bangji++;
    }
}

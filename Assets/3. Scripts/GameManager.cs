using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public GameObject pasanP;

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
    public Transform dogamP;
    public GameObject dogamsword;
    public GameObject newsword;
    public List<string> dogamlist = new List<string>();
    public GameObject gujiT;

    public TMP_Text sucsessT;
    public TMP_Text moneyT;
    public TMP_Text swordMT;
    public TMP_Text swordPMT;
    public TMP_Text bangjiT;
    public TMP_Text plusT;
    public TMP_Text noticeT;
    public TMP_Text pasanT;
    public TMP_Text onoffT;

    public int plus = 0;
    public int sucsess;
    public int money;
    public int bangji;
    public int warp;
    bool isOn = false;
    bool isItDogam;

    public new AudioSource audio;
    public AudioSource effectS;
    public AudioClip mainost;
    public AudioClip moneyS;
    public AudioClip upgradeS;
    public AudioClip breackS;

    // Start is called before the first frame update
    void Start()
    {
        SetSword();
        money = 100000;
        bangji += 100;

        Sound(true, mainost);
    }

    // Update is called once per frame
    void Update()
    {
        if(plus < swords.Length)
        {
            sword.sprite = swords[plus];
        }
        else
        {
            sword.sprite = null;
        }

            swordMT.text = $"판매비용: {swordmoney.ToString()}원";
        swordPMT.text = $"강화비용: {swordPlusmoney.ToString()}원";
        sucsessT.text = $"성공확률: {sucsess.ToString()}%";
        moneyT.text = "돈: " + money + "원";
        bangjiT.text = "방지권: " + bangji + "개";

        if (money <= 300)
        {
            pasanP.SetActive(true);
        }
        if (pasanP.activeSelf == true)
        {
            pasanT.text = $"앞으로 {10000 - money}원";

            if (money >= 10000)
            {
                pasanP.SetActive(false);
            }
        }

        if (money >= swordPlusmoney)
        {
            gujiT.SetActive(false);
        }
        else
        {
            gujiT.SetActive(true);
        }
    }

    public void GetMouseBD()
    {
        if(money >= swordPlusmoney)
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
                EffectSound(breackS);
                noticeT.text = $"방지권을 {swordbangji}개 소모해 다시 강화를 이어갈 수 있습니다";
                plusT.text = $"현재 단계: +{plus}강";
            }
            else
            {
                money -= swordPlusmoney;
                plus++;

                SetSword();
            }
        }
    }

    public void SetSword()
    {
        if (plus == 0)
        {
            firstP.SetActive(true);

            SwordData(100, "평범검", 0, 300, 1);
        }
        else if (plus == 1)
        {
            SwordData(99, "강화된 평범검", 400, 300, 1);
        }
        else if (plus == 2)
        {
            SwordData(95, "묶인 평범검", 1500, 500, 1);
        }
        else if (plus == 3)
        {
            SwordData(90, "양날검", 3500, 600, 1);
        }
        else if (plus == 4)
        {
            SwordData(85, "앱솔칼리버", 5900, 1200, 1);
        }
        else if (plus == 5)
        {
            SwordData(80, "특검", 8400, 1700, 1);
        }
        else if (plus == 6)
        {
            SwordData(80, "마왕이 10년 쓰다 버린 검", 12000, 2000, 1);
        }
        else if (plus == 7)
        {
            SwordData(75, "마체테?", 17000, 2300, 2);
        }
        else if (plus == 8)
        {
            SwordData(70, "곡곡곡도", 28000, 2600, 2);
        }
        else if (plus == 9)
        {
            SwordData(70, "마왕의 새 검", 48000, 3000, 2);
        }
        else if (plus == 10)
        {
            SwordData(65, "MZ세대 마왕의 검", 62000, 3600, 3);
        }
        else if (plus == 11)
        {
            SwordData(50, "주작의 깃",87000, 4000, 4);
            button.SetActive(true);
        }
        else if (plus == 12)
        {
            SwordData(40, "뉴비 용사의 검", 100000, 5000, 6);
        }
        else if (plus == 13)
        {
            SwordData(35, "중수 뉴비 용사의 검", 150000, 7000, 8);
        }
        else if (plus == 14)
        {
            SwordData(1, "도금검", 500000, 8000, 10);
            button.SetActive(true);
        }
        else if(plus > 14)
        {
            Debug.Log("와1 엔딩!!");
            BackToTeCho();
            SetSword();
        }

        if (plus > 0)
        {
            firstP.SetActive(false);
            secondP.SetActive(true);

            EffectSound(upgradeS);
        }
    }

    void SwordData(int a, string b, int c, int d, int e)
    {
        sucsess = a;
        swordname.text = $"+{plus} {b}";
        swordmoney = c;
        swordPlusmoney = d;
        swordbangji = e;

        isItDogam = false;
        foreach (string sword in dogamlist)
        {
            if (sword == swordname.text)
            {
                isItDogam = true;
                break;
            }
        }

        if (!isItDogam)
        {
            GameObject now;
            newsword.SetActive(true);

            dogamlist.Add(swordname.text);
            Instantiate(dogamsword, dogamP);
            now = dogamP.GetChild(dogamP.childCount - 1).gameObject;
            now.transform.Find("sword").GetComponentInChildren<Image>().sprite = swords[plus];
            now.transform.Find("name").GetComponentInChildren<TMP_Text>().text = b;
            now.transform.Find("sta").GetComponentInChildren<TMP_Text>().text = $"{a}% / 방지권 {e}개 필요\r\n강화 {d}원 / 판매 {c}원";
        }
        else
        {
            newsword.SetActive(false);
        }

        button.SetActive(false);
    }

    public void BackToTeCho()
    {
        firstP.SetActive(false);
        secondP.SetActive(false);
        button.SetActive(false);
        plus = 0;
    }

    public void Sell()
    {
        money += swordmoney;
        EffectSound(moneyS);
        BackToTeCho();
        SetSword();
    }

    public void SaveSword()
    {
        GameObject now;

        bool isIt = false;
        foreach (KeyValuePair<string, int> sword in ivswords)
        {
            if (sword.Key == swordname.text)
            {
                isIt = true;
            }
        }

        if (isIt)
        {
            ivswords[swordname.text]++;
        }
        else
        {
            ivswords.Add(swordname.text, 1);
            Instantiate(savesword, swordIv);

            now = swordIv.GetChild(ivswords.Count - 1).gameObject; // 프리팹을 직접 통제
            now.transform.Find("name").GetComponentInChildren<TMP_Text>().text = swordname.text;
            now.GetComponent<Item>().whatLv = plus;
        }

        BackToTeCho();
        SetSword();
    }

    public void SaveItem(int num, int warp)
    {
        bool isit = false;

        GameObject now;
        string a = itemP.GetChild(num).gameObject.transform.Find("Text (TMP)").GetComponentInChildren<TMP_Text>().text.Replace("\n", " ");

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
            now.GetComponent<Item>().whatLv = warp;
        }
    }

    public void Break()
    {
        BackToTeCho();
        SetSword();
    }

    public void Bangji()
    {
        if (bangji >= swordbangji)
        {
            bangji -= swordbangji;
            breakP.SetActive(false);
        }
    }

    public void Buy(int num)
    {
        int m = 0;
        int w = 0;

        if (num == 0)
        {
            m = 8000;
        }
        else if (num == 1)
        {
            m = 20000;
        }
        else if (num == 2)
        {
            m = 12000;
            w = 6;
        }
        else if (num == 3)
        {
            m = 18000;
            w = 8;
        }
        else if (num == 4)
        {
            m = 26000;
            w = 11;
        }

        if (money >= m)
        {
            if (num == 0)
            {
                bangji++;
            }
            else if(num == 1)
            {
                bangji += 3;
            }
            else
            {
                SaveItem(num, w);
            }
            money -= m;
        }
    }

    public void Hujup()
    {
        money += 200;
        EffectSound(moneyS);
    }

    void Sound(bool isloop, AudioClip clip)
    {
        audio.loop = isloop;
        audio.clip = clip;
        audio.Play();
    }

    void EffectSound(AudioClip clip)
    {
        effectS.clip = clip;
        effectS.Play();
    }

    public void SoundOnOff()
    {
        if(isOn)
        {
            onoffT.text = "배경음악 끄기";
            audio.volume = 0.7f;

            isOn = false;
        }
        else
        {
            onoffT.text = "배경음악 켜기";
            audio.volume = 0;

            isOn = true;
        }
    }
}

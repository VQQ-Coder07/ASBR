using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Chest : MonoBehaviour
{
    public enum type
    {
        wood, gold, magical
    }
    public enum rewardtype
    {
        coins, powerups, gems, card 
    }
    public type _type;
    public rewardtype _rewardtype;
    public int coinsChance;
    public int gemsChance;
    public int cardChance;
    public int powerUpChance;

    public float[] coinsRewards;
    public float[] gemsRewards;
    public float[] cardRewards;
    public float[] powerUpRewards;
    public float[] maxCoinsRewards;
    public float[] maxGemsRewards;
    public float[] maxCardRewards;
    public float[] maxPowerUpRewards;
    private int index;
    public int maxindex;
    public Transform democard;
    public GameObject gems;
    public GameObject coins;
    public GameObject card;
    public GameObject powerup;
    private GameObject previous;
    private rewardtype previousrewardtype;
    private int cardid;
    private float rewardVal;
    private int itype;
    private void OnEnable()
    {
        index = 0;
    }
    private void Start()
    {
        if(_type == type.wood)
        {
            itype = 0;
        }
        else if(_type == type.gold)
        {
            itype = 1;
        }
        else if(_type == type.magical)
        {
            itype = 2;
        }
        this.gameObject.layer = 7;
    }
    public void openchest()
    {
        Reward();
        GetComponent<Animator>().Play("open");
        Chests.instance.left.gameObject.SetActive(true);
        Invoke("next", 0.5f);
    }
    public void next()
    {
        Chests.instance.text.ResetTrigger("fade");
        Chests.instance.text.Play("init");
        Chests.instance.herotext.GetComponent<Animator>().SetTrigger("invisible");
        Chests.instance.herotext.GetComponent<Animator>().ResetTrigger("invisible");
        Chests.instance.herotext.gameObject.SetActive(false);
        Chests.instance.text.gameObject.SetActive(true);
        if(index == 0)
        {
            openchest();
            index++;
            return;
        }
        else if(index < maxindex && index != 0)
        {
            Destroy(previous);
            previous = Instantiate(Reward(), democard.transform.position, democard.transform.rotation, democard.transform.parent);
            if(previousrewardtype == rewardtype.coins)
            {
                GameManager.instance.FAadd("A", Mathf.RoundToInt(rewardVal));
            }
            else if(previousrewardtype == rewardtype.gems)
            {
                GameManager.instance.FAadd("B", Mathf.RoundToInt(rewardVal));
            }
            if(previousrewardtype == rewardtype.card && previousrewardtype != rewardtype.powerups)
            {
                Chests.instance.herotext.gameObject.SetActive(true);
                Chests.instance.herotext.GetComponent<Animator>().ResetTrigger("restore");
                Chests.instance.herotext.GetComponent<Animator>().ResetTrigger("fade");
                previous.GetComponent<CardItem>().preview.sprite = Chests.instance.cards[cardid].cardSprite;
//                Debug.LogError(cardid);
                Chests.instance.cards[cardid].locked = false;
            }
            if(previousrewardtype == rewardtype.powerups && previousrewardtype != rewardtype.card)
            {
                Chests.instance.cards[cardid].currentExperience+=Mathf.RoundToInt(rewardVal);
                Chests.instance.text.gameObject.SetActive(false);
                previous.GetComponent<CardItem>().preview.sprite = Chests.instance.cards[cardid].cardSprite;
                previous.GetComponent<CardItem>().ShowCard();
                previous.transform.GetChild(1).GetChild(6).GetChild(1).GetChild(0).GetComponent<Text>().text = "+ " + Mathf.RoundToInt(rewardVal).ToString();
                //previous.transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = Chests.instance.cards[cardid].currentExperience.ToString();// + "/" + CardManager.Instance.levelNeededeExp[i + 1];//MARKER CORRECT
                previous.transform.GetChild(1).GetChild(8).GetChild(0).GetComponent<Text>().text = (cardid + 2).ToString();//LV
                //previous.transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = (float)CardManager.Instance.cards[cardid].currentExperience;// / CardManager.Instance.levelNeededeExp[i + 1];
            }
            else if(previousrewardtype != rewardtype.powerups && previousrewardtype != rewardtype.card)
            {
            }
            CardManager.Instance.DisplayCharacters();
            CardManager.Instance.DisplayAvailableCharacter();
            CardManager.Instance.DisplayItemCharOpen();
            CardManager.Instance.DisplayLevelBar();
            //CardManager.Instance.UpdateXpBar();
            Chests.instance.text.GetComponent<TextMeshProUGUI>().text = string.Format("+{0}", Mathf.RoundToInt(rewardVal));
            index++;
        }
        else if(index >= maxindex && index != 0)
        {
            closechest();
        }
        int temp = maxindex - index;
        Chests.instance.left.text = temp.ToString() + " Rewards Left";
    }
    private void closechest()
    {
        GetComponent<Animator>().Play("closed");
        Chests.instance.left.text = "";
        if(previous != null)
        {
            Destroy(previous.gameObject);
        }
        Chests.instance.text.gameObject.SetActive(false);
        Chests.instance.end();
    }
    private GameObject Reward()
    {
        GameObject j = coins;
        rewardtype rand = GenerateReward();
//        Debug.LogError(rand);
        if(rand == rewardtype.coins)
        {
            j = coins;
            rewardVal = Random.Range(coinsRewards[itype], maxCoinsRewards[itype]);
        }
        if(rand == rewardtype.powerups)
        {
            j = powerup;
            List<int> available = new List<int>();
            for(int i=0; i < Chests.instance.cards.Length; i++)
            {
                if(!Chests.instance.cards[i].locked)
                {
                    available.Add(i);
                }
            }
            cardid = available[Random.Range(0, available.Count)];
            rewardVal = Random.Range(powerUpRewards[itype], maxPowerUpRewards[itype]);

        }
        if(rand == rewardtype.gems)
        {
            j = gems;
            rewardVal = Random.Range(gemsRewards[itype], maxGemsRewards[itype]);
        }
        if(rand == rewardtype.card)
        {
            j = card;
            GenID();
            Chests.instance.herotext.text = "Unlocked Hero - " + Chests.instance.cards[cardid].cardName;
            rewardVal = Random.Range(cardRewards[itype], maxCardRewards[itype]);
        }
//        Debug.LogError(rewardVal);
        return j;
    }
    private int GenID()
    {
        List<int> availablecards = new List<int>();
        for(int i=0; i < Chests.instance.cards.Length; i++)
        {
            if(Chests.instance.cards[i].locked)
            {
                availablecards.Add(i);
            }
        }
        int rand = availablecards[Random.Range(0, availablecards.Count)];
        int id = rand;
//        Debug.LogError(id);
        cardid = id;
        return id;
    }
    private rewardtype GenerateReward()
    {
        bool decided = false;
        rewardtype reward = rewardtype.card;
        int random = Random.Range(0, 100);
        if(random <= cardChance && !decided)
        {
            bool full = true;
            for(int i=0; i < Chests.instance.cards.Length; i++)
            {
                if(Chests.instance.cards[i].locked)
                {
                    full = false;
                    //Debug.LogError("unlocked");
                }
            }
            if(full)
            {
                //Debug.LogError("full");
                reward = rewardtype.powerups;
                previousrewardtype = reward;
                decided = true;
            }
            else
            {
                reward = rewardtype.card;
                previousrewardtype = reward;
                decided = true;
            }
            return reward;
            //Chests.instance.herotext.text = "Unlocked Hero - " + Chests.instance.cards[cardid].cardName;
        }
        if(random <= gemsChance && !decided)
        {
            reward = rewardtype.gems;
            previousrewardtype = reward;
            decided = true;
            return reward;
        }
        if(random <= powerUpChance && !decided)
        {
            reward = rewardtype.powerups;
            previousrewardtype = reward;
            decided = true;
            return reward;
        }
        if(random <= coinsChance && !decided)
        {
            reward = rewardtype.coins;
            previousrewardtype = reward;
            decided = true;
            return reward;
        }
        return rewardtype.powerups;
    }
}

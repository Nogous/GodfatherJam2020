using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum TwitteType
{
    Normal,
    Insultes,
    Compliments,
    Critiques,
    NonSense,
}

public class DefaultTextBox : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPooledObject
{
    TwitteType twitteType = TwitteType.Normal;

    public string pseudo;
    public string content;
    public float point;
    public int life;

    public Text speudoText;
    public Text contentText;
    public Text dateText;

    [Header("Move")]
    public PersonalityLife target;
    public float speed = 1f;

    private bool isDragging = false;

    [Header("DataPoints")]
    public int ego;
    public int happinesse;
    public int popularity;

    private bool isLiked = false;
    private bool isRT = false;

    [Header("ButtonSprite")]
    public UnityEngine.UI.Image ban;
    public UnityEngine.UI.Image rT;
    public UnityEngine.UI.Image del;
    public UnityEngine.UI.Image like;

    public Sprite banGris;
    public Sprite rtGris;
    public Sprite delGris;
    public Sprite likeGris;

    public Sprite banColor;
    public Sprite rtColor;
    public Sprite delColor;
    public Sprite likeColor;

    #region SetUp
    public void SetUpTextBox(PersonalityLife _target,string _pseudo, string _content, int _ego, int _happinesse)
    {
        target = _target;
        pseudo = _pseudo;
        content = _content;
        ego = _ego;
        happinesse = _happinesse;

        isLiked = false;
        isRT = false;

        isSlow = false;
        isDeliver = false;

        ban.sprite = banGris;
        rT.sprite = rtGris;
        del.sprite = delGris;
        like.sprite = likeGris;

        speudoText.text = pseudo + "\n"+"@"+pseudo;
        contentText.text = content;

        System.DateTime theTime = System.DateTime.Now;
        string month = "";

        switch (theTime.Month)
        {
            case 1:
                month = "Jan";
                break;
            case 2:
                month = "Feb";
                break;
            case 3:
                month = "Mar";
                break;
            case 4:
                month = "Apr";
                break;
            case 5:
                month = "May";
                break;
            case 6:
                month = "June";
                break;
            case 7:
                month = "July";
                break;
            case 8:
                month = "August";
                break;
            case 9:
                month = "Sep";
                break;
            case 10:
                month = "Oct";
                break;
            case 11:
                month = "Nov";
                break;
            case 12:
                month = "Dec";
                break;
        }

        if (theTime.Day<10)
        {
            dateText.text = "0" + theTime.Day + " " + month + " " + theTime.Year;
        }
        else
        {
            dateText.text = theTime.Day + " " + month + " " + theTime.Year;
        }
    }

    public void SetUpTextBox()
    {
        SetUpTextBox(target, pseudo, content, ego, happinesse);
    }



    public void OnObjectSpawn(PersonalityLife _target, string _pseudo, string _content, int _ego, int _happinesse)
    {
        SetUpTextBox(_target ,_pseudo, _content, _ego, _happinesse);
    }
    #endregion

    #region OnClick
    public void OnClickBan()
    {
        // ha ha ha sa vas etre la merde
    }

    public void OnClickRT()
    {
        // test si peut rt
        if (GameManager.Instance.currentRTCount > 0) return;
        GameManager.Instance.RestartRTCountdawn();

        // up les message du genre
        isRT = !isRT;
        if (isRT)
        {
            rT.sprite = rtColor;
        }
        else
        {
            rT.sprite = rtGris;
        }

        GameManager.Instance.SpawnMultiplicator(twitteType);
    }

    public void OnClickDel()
    {
        gameObject.SetActive(false);
    }

    public void OnClickLike()
    {
        if (GameManager.Instance.currentLikeCount > 0) return;
        GameManager.Instance.RestartLikeCountdawn();

        isLiked = !isLiked;
        if (isLiked)
        {
            like.sprite = likeColor;
        }
        else
        {
            like.sprite = likeGris;
        }
    }

    #endregion

    private void Update()
    {
        if (!isDragging)
        Move(target.gameObject.transform.position);
    }

    private bool isDeliver = false;

    public void Move(Vector2 _target)
    {
        if (isSlow)
        {
            transform.position += ((Vector3)_target - transform.position).normalized * speed/10;
        }
        else
        {
            transform.position += ((Vector3)_target - transform.position).normalized * speed;
        }

        if (isDeliver) return;

        if (Vector2.Distance(transform.position, _target)<10f)
        {
            isDeliver = true;
            target.ReceivingTwitte(happinesse, ego, twitteType, popularity, isLiked);
            gameObject.SetActive(false);
        }
    }

    private bool isSlow = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSlow = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isSlow = false;
    }

    #region DragAndDrop

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        isDeliver = false;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.position = Input.mousePosition;
    }
    #endregion
}
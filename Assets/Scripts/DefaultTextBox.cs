using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultTextBox : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public string pseudo;
    public string content;
    public float point;
    public int life;

    public Text speudoText;
    public Text contentText;
    public Text dateText;

    [Header("Move")]
    public Transform target;

    private bool isDragging = false;

    #region SetUp
    public void SetUpTextBox(string _pseudo, string _content)
    {
        pseudo = _pseudo;
        content = _content;

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
        SetUpTextBox(pseudo, content);
    }
    #endregion

    #region OnClick
    public void OnClickRT()
    {
        // test si peut rt

        // up les message du genre
    }

    public void DoDel()
    {
        Destroy(gameObject);
    }

    public void DoLike()
    {

    }

    #endregion

    private void Update()
    {
        if (!isDragging)
        Move(target.position);
    }

    public void Move(Vector2 Objectif)
    {
        transform.position = Vector2.MoveTowards(transform.position, Objectif, 10f);
    }

    #region DragAndDrop

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.position = Input.mousePosition;
    }
    #endregion
}
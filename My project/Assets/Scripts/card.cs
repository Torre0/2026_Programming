using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    Image img;
    Sprite frontSprite;
    public float rotateY;
    public TextMeshProUGUI text;
    public bool isFront = true;
    private Quaternion flipRortation = Quaternion.Euler(0, 180f, 0);
    private Quaternion originRortation = Quaternion.Euler(0, 0, 0);
    public int number;
    public Cardgame cardGame;
    public bool ismatched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        img = GetComponent<Image>();
        frontSprite = img.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        float currenY = transform.eulerAngles.y;


        if (isFront)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRortation, rotateY * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRortation, rotateY * Time.deltaTime);
        }
        if (currenY > 90 && currenY < 270)
        {
            text.gameObject.SetActive(false);

            img.sprite = null;
        }
        else
        {
            text.gameObject.SetActive(true);

            img.sprite = frontSprite;
        }
    }
    public void ClickCard()
    {
        if (ismatched)
        {

        }
        else
        {
            cardGame.OnClickCard(this);

        }

    }
    public void Flip(bool isFront)
    {
        this.isFront = isFront;
    }
    public void SetCardNumber(int newnumber)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        number = newnumber;
        text.text = number.ToString();
    }
    public void ChangeColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }
    public void Setimage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}

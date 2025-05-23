using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HeroHeathDisplay : MonoBehaviour
{
    [Header("Hearts Setup")]
    public Hero _hero;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Vector3 heartsSpacedX = new Vector3(50,0,0);
    public Vector3 heartsSpacedY = new Vector3(0, 40, 0);
    public GameObject heartContainer;
    public Transform initialHeartTransform;
    public Transform latestHeartTransform;
    public GameObject emptyHeartReference;
    public GameObject fullHeartReference;
    private int columnDown = 0;
    private Transform[] heartContainerAmount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // we do it on start because you only only want to do this one time
        InitializeDisplayHeath();
        InitializeCurrentHealth();
    }

    // Update is called once per frame
    void Update()
    {
        /*        GameObject testInst = Instantiate (emptyHeart, initialHeartTransform.position, initialHeartTransform.rotation);
                testInst.transform.SetParent(heartContainer.transform, true);*/
        heartContainerAmount = heartContainer.GetComponentsInChildren<Transform>();
        UpdateHeath();

    }

    void InitializeDisplayHeath()
    {
        // sets the lastestHeartTransform to ther initialHeartTransform, so it starts at the start.
        latestHeartTransform = initialHeartTransform;

        // creates initial placement for the heart and uses this first one as a reference for all the others
        GameObject latestHeart = Instantiate(emptyHeartReference, initialHeartTransform.position, initialHeartTransform.rotation); 
        latestHeart.SetActive(true);
        latestHeart.transform.SetParent(heartContainer.transform, true);
        latestHeart.name = "this is the culprit: ";
        // set i to 1 as there is already the original gameobject
        for (int i = 1; i < _hero.maxHealth; i++)
        {
            // adds heartsSpacedX to the latestHeartTransform so that it moves to the right
            latestHeart.transform.position += heartsSpacedX;

            // if the hearts are divisible by five, it will go down to a new colum.
            if ((i) % 5 == 0)
            {
                columnDown++;
                latestHeart.transform.position = initialHeartTransform.position - heartsSpacedY * columnDown;
            }

            // instantiate the latestHeartTransform and places it in the heartContainer as a child
            GameObject heart = Instantiate(emptyHeartReference, latestHeart.transform.position, latestHeart.transform.rotation);
            heart.SetActive(true);
            heart.name = "spawn number: " + i;
            heart.transform.SetParent(heartContainer.transform, true);
        }
        // destroys the reference position
        Destroy(latestHeart);
    }

    void InitializeCurrentHealth()
    {
        // gets the transform of ammount of hearts their are.
        Transform[] heartContainerAmount = heartContainer.GetComponentsInChildren<Transform>();
        int addedCurrentHealth = 0;
        //Debug.Log(heartContainerAmount.Length);
        // for each child in the heartcontainer 
        for (int i = 0; i < heartContainerAmount.Length; i++)
        {
            //Debug.Log(heartContainerAmount[i].name);
            if (addedCurrentHealth != _hero.currentHealth)
            {
                Image heartImage = heartContainerAmount[i].GetComponent<Image>();
                if (heartImage == null || heartContainerAmount[i].name == "this is the culprit: ")
                {
                    //Debug.LogError("This heart GameObject was skipped: " + heartContainerAmount[i].name);
                    continue;
                }
                else
                {
                    heartImage.sprite = fullHeart;
                }
                addedCurrentHealth++;
            }
            else
            {
                break;
            }
        }
    }

    void UpdateHeath()
    {
        AddSubtractMaxHealth();
        AddSubtractCurrentHealth();

/*        if(_hero.currentHealth != )
*/
      }

    void AddSubtractMaxHealth()
    {
        // gets the lastest heart position
        int lastHeart = heartContainerAmount.Length - 1;
        Transform lastHeartPosition = heartContainerAmount[lastHeart];

        if (lastHeartPosition == null)
        {
            Debug.Log("this activiated");
            lastHeartPosition = fullHeartReference.transform;
        }
        // we subtract one because for some reason the heartcontainer also recognizes itself as a child
        // if max health is greater, it adds more hearts
        //Debug.Log(heartContainerAmount.Length);
        if (_hero.maxHealth > heartContainerAmount.Length - 1)
        {
            // try referenceHeartNum, if it doesn't work, that means that there is only the original or no gameobjects in the parent
            try
            {
                // references the spawn number of the gameobject to see if it needs to be in a new row or not.
                string lastHeartName = heartContainerAmount[lastHeart].name;
                //Debug.Log(lastHeartName);
                int referenceHeartNum = int.Parse(lastHeartName.ToString().Replace("spawn number: ", ""));

                // if its divisible by 5, the next heart moves to a new row
                if ((referenceHeartNum + 1) % 5 == 0)
                {
                    Debug.Log("this reference is divisible by 5");
                    // it's not going past this
                    columnDown++;
                    Vector3 newHeartPosition = emptyHeartReference.transform.position - heartsSpacedY * columnDown;
                    GameObject addHeart = Instantiate(emptyHeartReference, newHeartPosition, lastHeartPosition.rotation);
                    addHeart.SetActive(true);
                    addHeart.transform.SetParent(heartContainer.transform, true);
                    addHeart.name = ($"spawn number:  {referenceHeartNum + 1}");
                    _hero.currentHealth++;
                }
                // it continues placing hearts right if not.
                else
                {
                    GameObject addHeart = Instantiate(emptyHeartReference, lastHeartPosition.position + heartsSpacedX, lastHeartPosition.rotation);
                    addHeart.SetActive(true);
                    addHeart.transform.SetParent(heartContainer.transform, true);
                    addHeart.name = ($"spawn number:  {referenceHeartNum + 1}");
                    _hero.currentHealth++;
                }
            }
            catch
            {
                // TODO: if max health == 0 make it so that the hero dies.
                if (heartContainerAmount.Length - 1 == 0)
                {
                    Debug.Log("test");
                    // adds first heart
                    GameObject addHeart = Instantiate(emptyHeartReference, emptyHeartReference.transform.position, emptyHeartReference.transform.rotation);
                    addHeart.SetActive(true);
                    addHeart.transform.SetParent(heartContainer.transform, true);
                    _hero.currentHealth++;
                    //addHeart.name = ($"spawn number:  {0}");
                }
                else if (heartContainerAmount.Length - 1 == 1)
                {
                    int timesCalled = 0;
                    Debug.Log($"this is being called: {timesCalled}");
                    GameObject addHeart = Instantiate(emptyHeartReference, emptyHeartReference.transform.position + heartsSpacedX, emptyHeartReference.transform.rotation);
                    addHeart.SetActive(true);
                    addHeart.transform.SetParent(heartContainer.transform, true);
                    _hero.currentHealth++;
                    addHeart.name = ($"spawn number:  {1}");
                }
            }

        }
        else if (_hero.maxHealth < heartContainerAmount.Length - 1)
        {
            int newColumn = _hero.maxHealth / 5;

            columnDown = newColumn;
            // destroy latest gameobject and sets the initial gameobject to false if maxHealth == 0
            Destroy(heartContainerAmount[lastHeart].gameObject);
        }
    }
    
    void AddSubtractCurrentHealth()
    {
        // this goes through each heart in the heart container
        // sets i to 1 because the heart container also considers the parent and child, so by doing this, it skips the parent
        for (int i = 1; i < heartContainerAmount.Length; i++)
        {
            if (heartContainerAmount[i].GetComponent<Image>() == null)
            {
                Debug.LogError($"this was skipped: {heartContainerAmount[i]}");
                continue;
            }

            // if that specific heart number(i) is less than the current health, it turns that heart into a full heart 
            if (i <= _hero.currentHealth)
            {
                heartContainerAmount[i].GetComponent<Image>().sprite = fullHeart;
            }
            // if its greater than, it makes it an empty heart
            else
            {
                heartContainerAmount[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }

        if (_hero.currentHealth > _hero.maxHealth)
        {
            Debug.LogError("can't have the current health larger than the max health");
            _hero.currentHealth = _hero.maxHealth;
        }
    }
}

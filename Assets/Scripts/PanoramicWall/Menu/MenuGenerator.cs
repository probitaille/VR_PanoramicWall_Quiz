using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Menu;

public class MenuGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject _menuItem;

    [SerializeField]
    private GameObject _questionLabel;

    public int nbItemsInWidth = 3;
    public int nbItemsInHeight = 3;

    private GameObject menus_1;
    private GameObject score;

    private int playerScore = 0;

    List<GameObject> buttonsList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        score = GameObject.Find("Score");
        score.SetActive(false);

        createMenu();
    }

    /*void tween()
    {
        iTween.RotateBy(menus_1, iTween.Hash("y", .50, "easeType", "easeInOutBack", "loopType", "none", "delay", 2, "time", 2, "oncomplete", "onTweenCompleted", "oncompletetarget", gameObject));

    }

    void onTweenCompleted()
    {
        Debug.Log("Tween completed");

        tween();
    }*/

    
    void createMenu()
    {
        //This is the menu container
        menus_1 = new GameObject();
        menus_1.name = "Menus";

        QuestionsManager.Question question = QuestionsManager.Instance.getNextQuestion();

        if(question == null)
        {
            showScore();
            return;
        }

        List<QuestionsManager.Choice> questionChoice = question.choices;

        
        GameObject questionLabel = Instantiate(_questionLabel);

        //Set texture to the question label
        Texture2D tmpTexture = (Texture2D)Resources.Load(question.titleTexture , typeof(Texture2D));
        questionLabel.GetComponent<Renderer>().material.mainTexture = tmpTexture;
        questionLabel.transform.position = new Vector3(0, 6, 0);
        questionLabel.transform.parent = menus_1.transform;


        for (int j=0;j< nbItemsInHeight; j++)
        {
            for(int k=0;k< nbItemsInWidth; k++)
            {
            
                GameObject button = Instantiate(_menuItem);
                button.transform.parent = menus_1.transform;

                float yAngle;

                if ( nbItemsInWidth % 2 == 0 )
                {
                    //Calculate y Angle with a pair number of items in width
                    yAngle = 25 * k - (nbItemsInWidth / 2 - 0.5F) * 25;
                }
                else
                {
                    //Calculate y Angle with a impair number of items in width
                    yAngle = 25 * k - Mathf.Ceil(nbItemsInWidth / 2 ) * 25;
                }

                //Set position and rotation
                button.transform.localRotation = Quaternion.Euler(0, yAngle, 0);
                button.transform.position = new Vector3(0, (nbItemsInHeight - 1 - j) * 1.8F , 0);

                //item.GetComponent<MenuButton>().ca    

                //Select a random texture from the array
                int index = Random.Range(0, questionChoice.Count - 1);
                QuestionsManager.Choice choice = questionChoice[index];


                button.GetComponent<MenuButton>().isGoodAnswer = choice.isGoodAnswer;

                //Set texture to the menu item
                Texture2D inputTexture =  (Texture2D)Resources.Load(choice.texturePath, typeof(Texture2D));
                button.GetComponent<Renderer>().material.mainTexture = inputTexture;

                
                //Delete texture from the array to prevent random select it again
                questionChoice.RemoveAt(index);

                button.GetComponent<MenuButton>().OnButtonSelected += selectButton;

                buttonsList.Add(button);
            }
        }
    }


    void showScore()
    {
        score.GetComponent<TextMesh>().text = "Your score is: " + playerScore + "/" + QuestionsManager.Instance.getNumberOfQuestions();
        
        score.SetActive(true);
    }


     public void selectButton(MenuButton selectedButton)
    {

        if (selectedButton.isGoodAnswer)
        {
            Debug.Log("Good answer");

            playerScore++;
        }

        for (int k=0; k < buttonsList.Count; k++)
        {

            GameObject answer = Instantiate(_menuItem);

            answer.transform.localRotation = buttonsList[k].transform.localRotation;
            answer.transform.position = new Vector3(buttonsList[k].transform.position.x, buttonsList[k].transform.position.y, buttonsList[k].transform.position.z - 0.1F);

            answer.transform.parent = menus_1.transform;
            Destroy(answer.GetComponent<MenuItemPopout>());

            answer.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");

            if (buttonsList[k].GetComponent<MenuButton>().isGoodAnswer)
            {
                Texture2D texture = (Texture2D)Resources.Load("PanoramicTextures/Answer/good_answer", typeof(Texture2D));
                answer.GetComponent<Renderer>().material.mainTexture = texture;
            }
            else
            {
                Texture2D texture = (Texture2D)Resources.Load("PanoramicTextures/Answer/bad_answer", typeof(Texture2D));
                answer.GetComponent<Renderer>().material.mainTexture = texture;
            }
            
        }

        StartCoroutine(WaitAndRecreateMenu());
        
    }

    System.Collections.IEnumerator WaitAndRecreateMenu()
    {
        
        yield return new WaitForSeconds(3);

        Destroy(menus_1);

        buttonsList.Clear();

        createMenu();

    }
}

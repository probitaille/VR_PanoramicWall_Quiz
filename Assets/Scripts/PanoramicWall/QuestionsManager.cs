using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionsManager
{

    private static QuestionsManager instance;
    private QuestionsManager() { }

    public List<Question> questions = new List<Question>();

    public int currentQuestionIndex = 0;

    public class Question
    {
        public string titleTexture { get; set; }
        public List<Choice> choices = new List<Choice>();
    }

    public class Choice
    {
        public string texturePath { get; set; }
        public bool isGoodAnswer { get; set; }
    }

    private string[] questionsTitleTexture =
    {
        "PanoramicTextures/Questions/question1_texture",
        "PanoramicTextures/Questions/question2_texture",
        "PanoramicTextures/Questions/question3_texture",
        "PanoramicTextures/Questions/question4_texture"
    };

    #region Choices setter
    List<List<Choice>> allChoices = new List<List<Choice>>()
    {
        new List<Choice>()
        {
            new Choice { texturePath = "PanoramicTextures/texture_01", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_02", isGoodAnswer = true },
            new Choice { texturePath = "PanoramicTextures/texture_13", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_04", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_15", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_06", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_07", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_08", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_09", isGoodAnswer = false }
        },

        new List<Choice>()
        {
            new Choice { texturePath = "PanoramicTextures/texture_10", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_11", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_20", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_07", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_14", isGoodAnswer = true },
            new Choice { texturePath = "PanoramicTextures/texture_05", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_01", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_37", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_39", isGoodAnswer = false }
        },

        new List<Choice>()
        {
            new Choice { texturePath = "PanoramicTextures/texture_20", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_21", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_22", isGoodAnswer = true },
            new Choice { texturePath = "PanoramicTextures/texture_23", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_24", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_25", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_26", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_28", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_12", isGoodAnswer = false }
        },

        new List<Choice>()
        {
            new Choice { texturePath = "PanoramicTextures/texture_02", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_05", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_11", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_19", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_22", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_25", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_28", isGoodAnswer = true },
            new Choice { texturePath = "PanoramicTextures/texture_31", isGoodAnswer = false },
            new Choice { texturePath = "PanoramicTextures/texture_38", isGoodAnswer = false }
        }

    };
    #endregion


    public static QuestionsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestionsManager();
                instance.initialize();

            }
            return instance;
        }
    }

    public int getNumberOfQuestions()
    {
        return questions.Count;
    }

    public Question getNextQuestion()
    {
        if(currentQuestionIndex < questions.Count) { 

            Question nextQuestion = questions[currentQuestionIndex];

            currentQuestionIndex++;

            return nextQuestion;
        }
        else
        {
            return null;
        }
    }


    public void initialize()
    {
        
        int nbChoices = questionsTitleTexture.Length;

        for (int j = 0; j < nbChoices; j++)
        {
            Question question = new Question();
            question.titleTexture = questionsTitleTexture[j];
            question.choices = allChoices[j];
            questions.Add(question);
        }
    }


}

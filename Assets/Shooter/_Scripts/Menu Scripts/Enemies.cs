using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemies : MonoBehaviour {

    Dropdown []color;
    // Use this for initialization
    void Awake() {
        color = new Dropdown[5];
    }
    void Start () {

        color = gameObject.GetComponentsInChildren<Dropdown>();
        color[0].onValueChanged.AddListener(delegate {
            colorChange(color[0]);
        }); color[1].onValueChanged.AddListener(delegate {
            colorChange(color[1]);
        }); color[2].onValueChanged.AddListener(delegate {
            colorChange(color[2]);
        }); color[3].onValueChanged.AddListener(delegate {
            colorChange(color[3]);
        }); color[4].onValueChanged.AddListener(delegate {
            colorChange(color[4]);
        });
        //for loop won't work?!?
        /*for (int i = 0; i < 5; i++) {
            color[i].onValueChanged.AddListener(delegate {
            colorChange(color[i]);
            });
        }*/
       ShowSetValues();
    }

    //Sets the value of each enemy bsaed on the index of the dropdown menu
    private void colorChange(Dropdown target)
    {

        switch (target.value)
        {
            //RED
            case 0:
                if (target.name.Equals("Colour Dropdown 1"))
                {
                    Game.current.shooterSettings.colourEnemy1 = "Red";
                }
                else if (target.name.Equals("Colour Dropdown 2"))
                {
                    Game.current.shooterSettings.colourEnemy2 = "Red";

                }
                else if (target.name.Equals("Colour Dropdown 3"))
                {
                    Game.current.shooterSettings.colourEnemy3 = "Red";

                }
                else if (target.name.Equals("Colour Dropdown 4"))
                {
                    Game.current.shooterSettings.colourEnemy4 = "Red";

                }
                else if (target.name.Equals("Colour Dropdown 5")) {
                    Game.current.shooterSettings.colourEnemy5 = "Red";

                }
                break;
            //BLUE
            case 1:
                if (target.name.Equals("Colour Dropdown 1"))
                {
                    Game.current.shooterSettings.colourEnemy1 = "Blue";
                }
                else if (target.name.Equals("Colour Dropdown 2"))
                {
                    Game.current.shooterSettings.colourEnemy2 = "Blue";
                }
                else if (target.name.Equals("Colour Dropdown 3"))
                {
                    Game.current.shooterSettings.colourEnemy3 = "Blue";

                }
                else if (target.name.Equals("Colour Dropdown 4"))
                {
                    Game.current.shooterSettings.colourEnemy4 = "Blue";

                }
                else if (target.name.Equals("Colour Dropdown 5"))
                {
                    Game.current.shooterSettings.colourEnemy5 = "Blue";

                }
                break;

            //YELLOW
            case 2:
                if (target.name.Equals("Colour Dropdown 1"))
                {
                    Game.current.shooterSettings.colourEnemy1  = "Yellow";
                }
                else if (target.name.Equals("Colour Dropdown 2"))
                {
                    Game.current.shooterSettings.colourEnemy2 = "Yellow";

                }
                else if (target.name.Equals("Colour Dropdown 3"))
                {
                    Game.current.shooterSettings.colourEnemy3 = "Yellow";

                }
                else if (target.name.Equals("Colour Dropdown 4"))
                {
                    Game.current.shooterSettings.colourEnemy4 = "Yellow";

                }
                else if (target.name.Equals("Colour Dropdown 5"))
                {
                    Game.current.shooterSettings.colourEnemy5 = "Yellow";
                }
                break;

            //GREEN
            case 3:
                if (target.name.Equals("Colour Dropdown 1"))
                {
                    Game.current.shooterSettings.colourEnemy1 = "Green";
                }
                else if (target.name.Equals("Colour Dropdown 2"))
                {
                    Game.current.shooterSettings.colourEnemy2 = "Green";

                }
                else if (target.name.Equals("Colour Dropdown 3"))
                {
                    Game.current.shooterSettings.colourEnemy3 = "Green";

                }
                else if (target.name.Equals("Colour Dropdown 4"))
                {
                    Game.current.shooterSettings.colourEnemy4 = "Green";

                }
                else if (target.name.Equals("Colour Dropdown 5"))
                {
                    Game.current.shooterSettings.colourEnemy5 = "Green";

                }

                break;

             //PURPLE
            case 4:
                if (target.name.Equals("Colour Dropdown 1"))
                {
                    Game.current.shooterSettings.colourEnemy1 = "Purple";
                }
                else if (target.name.Equals("Colour Dropdown 2"))
                {
                    Game.current.shooterSettings.colourEnemy2 = "Purple";
                }
                else if (target.name.Equals("Colour Dropdown 3"))
                {
                    Game.current.shooterSettings.colourEnemy3 = "Purple";
                }
                else if (target.name.Equals("Colour Dropdown 4"))
                {
                    Game.current.shooterSettings.colourEnemy4 = "Purple";
                }
                else if (target.name.Equals("Colour Dropdown 5"))
                {
                    Game.current.shooterSettings.colourEnemy5 = "Purple";
                }

                break;
        }

    }

    //Function shows the value associated with each enemy
    public void  ShowSetValues()
    {
        SetValues(0, Game.current.shooterSettings.colourEnemy1);
        SetValues(1, Game.current.shooterSettings.colourEnemy2);
        SetValues(2, Game.current.shooterSettings.colourEnemy3);
        SetValues(3, Game.current.shooterSettings.colourEnemy4);
        SetValues(4, Game.current.shooterSettings.colourEnemy5);
        GameObject.Find("Colour Dropdown 1").GetComponent<Dropdown>().GetComponentInChildren<Text>().text = Game.current.shooterSettings.colourEnemy1;
        GameObject.Find("Colour Dropdown 2").GetComponent<Dropdown>().GetComponentInChildren<Text>().text = Game.current.shooterSettings.colourEnemy2;
        GameObject.Find("Colour Dropdown 3").GetComponent<Dropdown>().GetComponentInChildren<Text>().text = Game.current.shooterSettings.colourEnemy3;
        GameObject.Find("Colour Dropdown 4").GetComponent<Dropdown>().GetComponentInChildren<Text>().text = Game.current.shooterSettings.colourEnemy4;
        GameObject.Find("Colour Dropdown 5").GetComponent<Dropdown>().GetComponentInChildren<Text>().text = Game.current.shooterSettings.colourEnemy5;

        GameObject.Find("Enemy 1 Points").GetComponent<InputField>().text = Game.current.shooterSettings.colourEnemy1.ToString();
        GameObject.Find("Enemy 2 Points").GetComponent<InputField>().text = Game.current.shooterSettings.colourEnemy2.ToString();
        GameObject.Find("Enemy 3 Points").GetComponent<InputField>().text = Game.current.shooterSettings.colourEnemy3.ToString();
        GameObject.Find("Enemy 4 Points").GetComponent<InputField>().text = Game.current.shooterSettings.colourEnemy4.ToString();
        GameObject.Find("Enemy 5 Points").GetComponent<InputField>().text = Game.current.shooterSettings.colourEnemy5.ToString();
        
    }
    //Funtion sets the index of colors based on string values
    private void SetValues(int index, string temp) {
        if (color[index] != null)
        {
            if (temp.Equals("Red"))
            {
                color[index].value = 0;
            }
            else if (temp.Equals("Blue"))
            {
                color[index].value = 1;
            }
            else if (temp.Equals("Yellow"))
            {
                color[index].value = 2;
            }
            else if (temp.Equals("Green"))
            {
                color[index].value = 3;
            }
            else if (temp.Equals("Purple"))
            {
                color[index].value = 4;
            }
        }
    }
}

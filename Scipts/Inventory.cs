using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;
using TMPro;

public class Inventory : MonoBehaviour
{   
    int moneyyy  = GameMenu.money;
    public TMPro.TMP_InputField skinName;
    long id = LoginAndRegister.id;
    public TextMeshProUGUI textMeshProUGUI;
    public Text text;
    private string db = "URI=file:OW3.db";
    // Start is called before the first frame update
    void Start()
    {
        showInventory();
        textMeshProUGUI.text = ""+moneyyy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goback()
    {
        SceneManager.LoadScene(4);
    }
    public void buy()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                int cost=0;
                command.CommandText="SELECT COST FROM Skins Where sNAME LIKE '"+skinName.text+"%';";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cost = (int)reader["COST"];
                        Debug.Log(cost);
                    }
                    reader.Close();
                }
                if (moneyyy - cost < 0)
                {
                    text.text = "you don't have enough money";
                }
                else
                {
                    command.CommandText = "SELECT * FROM UNLOCKED A,Skins B WHERE A.ID="+id+" AND A.cID=B.cID AND B.sNAME LIKE'"+skinName.text+"%' ;";
                    string temp = "";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp = (string)reader["sID"];
                            cost = (int)reader["COST"];
                        }
                        reader.Close();
                    }
                    if(temp.Length==0)
                    {
                        command.CommandText = "SELECT cNAME FROM Skins A,Characters B WHERE A.cID=B.cID AND A.sNAME LIKE '" + skinName.text+ "' ;";
                        using (IDataReader reader = command.ExecuteReader())
                        {
                           
                            while (reader.Read())
                            {
                                temp = (string)reader["cNAME"];
                               

                            }
                            reader.Close();
                        }
                        
                        text.text = "You don't have the necessary character .You have to unlock this character "+temp;
                    }
                    else
                    {
                        command.CommandText = "INSERT INTO INVENTORY VALUES (" + id + int.Parse(temp) + ");";
                        command.ExecuteNonQuery();
                        command.CommandText="UPDATE MONEY A SET A.MONEY ="+(moneyyy-cost)+" WHERE A.ID ="+id;
                        command.ExecuteNonQuery();
                    }

                }

            }
            connection.Clone();
        }
    }
    public void NotshowInventory()

    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Skins A,Inventory B WHERE A.sID=B.sID AND B.ID=" + id + ";";
                string temp="";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         temp += "Skin:" + (string)reader["sNAME"] + " Cost: " + (int)reader["COST"] + "\n";
                    }
                    reader.Close();
                }
                if (temp.Equals(""))
                {
                    command.CommandText = "SELECT * FROM Skins;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp += "Skin: " + (string)reader["sNAME"] + " Cost: " + (int)reader["COST"] + "\n";
                        }
                        text.text = temp;
                        reader.Close();
                    }

                }
                else
                {
                    command.CommandText = "CREATE VIEW notOwned AS\r\nSELECT Skins.sID,Skins.sNAME,Skins.COST\r\nFROM Skins\r\nWHERE Skins.sID IS NOT NULL AND Skins.sID NOT IN (\r\n  SELECT Inventory.sID\r\n  FROM Users\r\n  LEFT JOIN Inventory ON Users.ID = Inventory.ID\r\n  WHERE Users.ID = " + id + "\r\n);";
                    temp = "";
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT * FROM notOwned;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            temp += "Skin: " + (string)reader["sNAME"] + " Cost: " + (int)reader["COST"] + "\n";
                        }
                        text.text = temp;
                        reader.Close();
                    }
                    command.CommandText = "DROP VIEW notOwned;";
                    command.ExecuteNonQuery();

                }

            }
            connection.Close();

        }

    }
    public void showInventory()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText="SELECT * FROM Skins A,Inventory B WHERE A.sID=B.sID AND B.ID="+id+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    string temp = "";
                   while (reader.Read())
                    {
                       
                       // Debug.Log(temp);
                      
                        
                            temp +="Skin: " + (string)reader["sNAME"] + " Cost: " + (int)reader["COST"]+"\n";
                        
                    }
                    if (temp.Equals(""))
                    {
                        text.text = "You don't have any skin.";
                    }
                    else
                    {
                        text.text = temp;
                    }
                   
                    reader.Close();
                }
            }
            connection.Close();

        }
        

    }
}

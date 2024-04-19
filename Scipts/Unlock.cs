using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;
using System.Data;
using UnityEngine.SceneManagement;

public class Unlock : MonoBehaviour
{ long id1 = LoginAndRegister.id;
    public Text text;
    public TMPro.TMP_InputField cName;
    private string db = "URI=file:OW3.db";
    // Start is called before the first frame update
    void Start()
    {
        showUnLocked();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void goback()
    {
        SceneManager.LoadScene(4);
    }

    public void unclock()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                long cid = 0;
                command.CommandText="Select cID FROM Characters WHERE cNAME LIKE '"+cName.text+"%';";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cid = (long)reader["cID"];
                    }
                    reader.Close();
                }
                command.CommandText="INSERT INTO UNLOCKED VALUES ("+id1+","+cid+");";
                command.ExecuteNonQuery();
                text.text = "Unlocked " + cName.text;
            }
            connection.Close();
        }
    }
    public void showLocked()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                string temp = "";
                command.CommandText = "SELECT CNAME FROM Characters EXCEPT SELECT CNAME FROM Characters A WHERE A.cID IN(SELECT cID FROM UNLOCKED WHERE ID =" + id1 + ");";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp += "Character: " + reader["CNAME"] + "\n";
                    }
                    reader.Close();
                }
                
                    text.text = temp;
                
            }
            connection.Close();

        }
    }
    public void showUnLocked()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                string temp = "";
                command.CommandText = "SELECT CNAME FROM Characters A WHERE A.cID IN(SELECT cID FROM UNLOCKED WHERE ID =" + id1 + ");";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp += "Character: "+reader["CNAME"]+"\n";
                    }
                    reader.Close();
                }
                if (temp.Equals(""))
                {
                    text.text = "You don't have any character.";
                }
                else
                {
                    text.text = temp;
                }
            }
            connection.Close() ;
        }

    }
}

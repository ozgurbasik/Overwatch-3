using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using TMPro;

public class TOP50 : MonoBehaviour
{
    string db = "URI=file:OW3.db";
    public Text text;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        top50();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void back()
    {
        SceneManager.LoadScene(4);   
    }
    public void top50()

    {
        string line = "TOP50\n";
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "\r\nSELECT DISTINCT ID ,NICKNAME ,RANK ,RANKNAME FROM Users ,Ranks\r\nWHERE Users.RANK >= Ranks.MinELO AND Users.RANK <Ranks.MaxELO\r\nORDER BY Users.RANK DESC LIMIT 50;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        line += "ID: " + reader["ID"] + " Nickname: " + reader["NICKNAME"] + " RANK: " + reader["RANK"]+" RankName: " + reader["RANKNAME"]+"\n";
                   

                    }
                    reader.Close();
                }


            }
            connection.Close();
        }
        text.text = line;   

    }
    

    public void search()
    {
        string line = "TOP50\n";
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM(SELECT DISTINCT ID ,NICKNAME ,RANK ,RANKNAME FROM Users ,Ranks\r\nWHERE Users.RANK >= Ranks.MinELO AND Users.RANK <Ranks.MaxELO\r\nORDER BY Users.RANK DESC LIMIT 50)TOP50 WHERE TOP50.NICKNAME LIKE '"+inputField.text+"%';";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        line += "ID: " + reader["ID"] + " Nickname: " + reader["NICKNAME"] + " RANK: " + reader["RANK"] + " RankName: " + reader["RANKNAME"]+"\n";


                    }
                    reader.Close();
                }


            }
            connection.Close();
        }
        text.text = line;

    }
}

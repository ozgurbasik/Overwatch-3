using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    long id =LoginAndRegister.id;
    string db = "URI=file:OW3.db";
  public  static int money;
    public TextMeshProUGUI nicknname;
    public TextMeshProUGUI money1;
    void Start()
    {
        showInfo();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quit()
    {
        Application.Quit();
       
    }
    public void play()
    {
        SceneManager.LoadScene(3);
    }
    public void charactea()
    {
        SceneManager.LoadScene(8);
    }
    public void hist()
    {
        SceneManager.LoadScene(5);
    }

    public void top50()
    {
        SceneManager.LoadScene(6);
    }
    public void inventory()
    {
        SceneManager.LoadScene(7);
    }
    public void showInfo()
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText="SELECT * FROM Users WHERE ID="+id+";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nicknname.text ="NICKNAME:"+ (string)reader["NICKNAME"];
                        
                    }
                    reader.Close();
                }
                command.CommandText = "SELECT * FROM MONEY WHERE ID=" + id + ";";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        money1.text = "Money: "+((int)reader["MONEY"]);
                        money = (int)reader["MONEY"];

                    }
                    reader.Close();
                }
                connection.Close();
            }

        }
    }
}

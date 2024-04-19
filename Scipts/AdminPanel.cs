using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AdminPanel : MonoBehaviour
{
    ArrayList arlist = new ArrayList();
    public VideoPlayer player;
    public Button button;
    public Button backk;
    public Button delete;
    public TMP_InputField min;
    public TMP_InputField max;

    // Start is called before the first frame update
    void Start()
    {
        player.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            player.Pause();
           player.gameObject.SetActive(false) ; 
            button.gameObject.SetActive(true) ;
            delete.gameObject.SetActive(true);
            backk.gameObject.SetActive(true);
            max.gameObject.SetActive(true);
            min.gameObject.SetActive(true);
        }
        else if (player.isPaused)
        {
            player.gameObject.SetActive(false);
            button.gameObject.SetActive(true);
            delete.gameObject.SetActive(true);
            backk.gameObject.SetActive(true);
            max.gameObject.SetActive(true);
            min.gameObject.SetActive(true); 
        }
    }

    

    public void execute()
    {
        string[] files = Directory.GetFiles("C:\\Users\\Ahmet Engin\\Overwatch3");
        ArrayList tables = new ArrayList();
        foreach (string file in files)
        {
           
            if (Path.GetExtension(file).Equals(".db"))
            {
                arlist.Add(Path.GetFileName(file));

            }
        }
        foreach (var db in arlist)
        {
            string db1= db.ToString();
           
            using (var connection = new SqliteConnection("URI=file:"+db1))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader["name"]);
                        }
                        reader.Close();
                    }
                    foreach(var table in tables)
                    {
                        if (table.Equals("sqlite_sequence"))
                        {
                            continue;
                        }
                        else
                        {
                            command.CommandText = "DROP TABLE " + table;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }
        player.gameObject.SetActive(true);
        player.Play();
        button.gameObject.SetActive(false);
        delete.gameObject.SetActive(false);
        backk.gameObject.SetActive(false);
        max.gameObject.SetActive(false);
        min.gameObject.SetActive(false);
    }
    
    public void deletemethod()
    {
        int MIN = int.Parse(min.text);
        int MAX = int.Parse(max.text);
        using (var connection = new SqliteConnection("URI=file:OW3.db"))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM MatchHistory WHERE mID IN (SELECT mID FROM UsersToMatch WHERE ID BETWEEN "+MIN+" AND "+MAX+");";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM UsersToMatch WHERE ID BETWEEN " + MIN + " AND " + MAX + " ;";
                command.ExecuteNonQuery();
                command.CommandText="DELETE FROM MONEY WHERE ID BETWEEN " + MIN + " AND " + MAX + " ;";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM INVENTORY WHERE ID BETWEEN " + MIN + " AND " + MAX + " ;";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM UNLOCKED WHERE ID BETWEEN " + MIN + " AND " + MAX + " ;";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM USERS WHERE ID BETWEEN " + MIN + " AND " + MAX + " ;";
                command.ExecuteNonQuery();

            }
            connection.Close();
        }

    }

    public void goback()
    {
        SceneManager.LoadScene(1);
    }
    

}
